(ns beadmania.image-operations
  (:require [beadmania.color :as color]
            [mikera.image.core :as imagez]))

(defn transform-pixels
  [pixels width]
  (->> pixels
       (map (comp color/color->rgb-color color/argb->color))
       (partition width)))

(defn transform
  [tempfile-path filename]
  (let [image (imagez/load-image tempfile-path)
        pixels (vec (imagez/get-pixels image))]
    (transform-pixels pixels (.getWidth image))))
