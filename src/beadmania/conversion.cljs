(ns beadmania.conversion
  (:require [beadmania.color :as color]
            [beadmania.color-distance :as color-distance]))

(defn best-match
  [rgb rgb-palette]
  (apply min-key
         #(color-distance/delta-e94-squared (color/rgb->lab rgb)
                                            (color/rgb->lab %))
         rgb-palette))

(defn convert
  [pixels rgb-palette]
  ;; FIXME: create map of rgb/lab values to improve performance
  (mapv (fn [line]
          (mapv #(best-match % rgb-palette)
                line))
        pixels))
