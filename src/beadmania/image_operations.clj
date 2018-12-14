(ns beadmania.image-operations
  (:require [beadmania.color :as color]
            [mikera.image.core :as imagez]))

(defn transform-pixels
  [pixels width]
  (->> pixels
       (map (comp color/color->rgb-color color/argb->color))
       (partition width)))

(defn shrink
  [image]
  (let [max-size 64]
    (if (> (.getWidth image)
           max-size)
      (imagez/resize image max-size)
      image)))

(defn transform
  [tempfile-path filename]
  (let [image (-> tempfile-path
                  imagez/load-image
                  shrink)
        pixels (vec (imagez/get-pixels image))]
    (transform-pixels pixels (.getWidth image))))
