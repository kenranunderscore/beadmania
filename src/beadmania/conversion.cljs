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

(let [ref-x 95.047
      ref-y 100.0
      ref-z 108.883]
  (defn xyz->lab
    [[x y z]]
    (let [nx (normalize-xyz-value (/ x ref-x))
          ny (normalize-xyz-value (/ y ref-y))
          nz (normalize-xyz-value (/ z ref-z))]
      [(- (* 116.0 ny) 16)
       (* 500.0 (- nx ny))
       (* 200.0 (- ny nz))])))

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
        d-a (- a1 a2)
        d-c (- c1 c2)
        d-b (- b1 b2)
        d-l (- (first v) (first w))
        d-h (Math/sqrt (- (+ (square d-a) (square d-b))
                          (square d-c)))
        s-c (inc (* 0.045 c1))
        s-h (inc (* 0.015 c1))
        radicand (+ (square d-l) (square (/ d-c s-c)) (square (/ d-h s-h)))]
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
