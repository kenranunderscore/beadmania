(ns beadmania.conversion
  (:require [clojure.core.matrix :as matrix]))

(def rgb->xyz-transformation
  (matrix/matrix [[0.4124 0.3576 0.1805]
                  [0.2126 0.7152 0.0722]
                  [0.0193 0.1192 0.9505]]))

(defn normalize-rgb-value
  [n]
  (let [val (/ n 255.0)]
    (* 100
       (if (> val 0.04045)
         (Math/pow (/ (+ val 0.055)
                      1.055)
                   2.4)
         (/ val 12.92)))))

(defn rgb->xyz
  [rgb]
  (matrix/mmul rgb->xyz-transformation
               (map normalize-rgb-value
                    rgb)))

(defn normalize-xyz-value
  [v]
  (if (> v 0.008856)
    (Math/pow v
              (/ 1.0 3))
    (+ (* 7.787 v)
       (/ 16.0 116))))

(def reference-x
  95.047)

(def reference-y
  100.0)

(def reference-z
  108.883)

(defn xyz->lab
  [[x y z]]
  (let [nx (normalize-xyz-value (/ x reference-x))
        ny (normalize-xyz-value (/ y reference-y))
        nz (normalize-xyz-value (/ z reference-z))]
    [(- (* 116.0 ny) 16)
     (* 500.0 (- nx ny))
     (* 200.0 (- ny nz))]))

(def rgb->lab (comp xyz->lab rgb->xyz))

(defn square
  "Squares a number `x`."
  [x]
  (* x x))

(defn delta-e94
  [v w]
  (let [a1 (second v)
        a2 (second w)
        b1 (last v)
        b2 (last w)
        c1 (Math/sqrt (+ (square a1) (square b1)))
        c2 (Math/sqrt (+ (square a2) (square b2)))
        da (- a1 a2)
        dc (- c1 c2)
        db (- b1 b2)
        dl (- (first v) (first w))
        dh (Math/sqrt (- (+ (square da) (square db))
                          (square dc)))
        sc (inc (* 0.045 c1))
        sh (inc (* 0.015 c1))
        radicand (+ (square dl) (square (/ dc sc)) (square (/ dh sh)))]
    (Math/sqrt radicand)))

(defn best-match
  [rgb rgb-palette]
  (apply min-key
         #(delta-e94 (rgb->lab rgb) (rgb->lab %))
         rgb-palette))

(defn convert
  [pixels rgb-palette]
  ;; FIXME: create map of rgb/lab values to improve performance
  (mapv (fn [line]
          (mapv #(best-match % rgb-palette)
                line))
        pixels))
