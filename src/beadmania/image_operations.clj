(ns beadmania.image-operations
  (:require [mikera.image.core :as imagez]))

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

(defn transform
  [tempfile-path filename]
  (let [image (imagez/load-image tempfile-path)
        pixels (vec (imagez/get-pixels image))
        colors (map argb->color pixels)]
    (partition (.getWidth image) colors)))