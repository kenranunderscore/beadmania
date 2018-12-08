(ns beadmania.color)

(def rgb->xyz-transformation-matrix
  [[0.4124 0.3576 0.1805]
   [0.2126 0.7152 0.0722]
   [0.0193 0.1192 0.9505]])

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
  (let [normalized (map normalize-rgb-value rgb)]
    (map (fn [line]
           (apply + (map * line normalized)))
         rgb->xyz-transformation-matrix)))

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

(defn argb->color
  [argb]
  (mapv #(bit-and 0xff (bit-shift-right argb %))
        [24 16 8 0]))

(defn color->argb
  [[a r g b]]
  (bit-or (bit-shift-left a 24)
          (bit-shift-left r 16)
          (bit-shift-left g 8)
          b))

(defn color->rgb-color
  [[a & rgb]]
  (let [x (- 255 a)
        solid (fn [color-value]
                (int (+ x
                        (* (/ a 255.0)
                           color-value)
                        0.5)))]
    (mapv solid rgb)))
