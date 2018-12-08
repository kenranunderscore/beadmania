(ns beadmania.color)

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
