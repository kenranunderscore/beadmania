(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.sidebar :as sidebar]))

(defn rectangular-pixel
  [x y color size distance]
  (letfn [(offset [index]
            (* index (+ distance size)))]
    (dom/rect {:x (offset x)
               :y (offset y)
               :width size
               :height size
               :style {:fill color}})))

(defn circular-pixel
  [x y color size distance]
  (let [r (/ size 2)
        offset (fn [index]
                 (+ (* index
                       (+ distance size))
                    r))]
    (dom/circle {:cx (offset x)
                 :cy (offset y)
                 :r r
                 :fill color})))

(defn drawn-pixels
  [draw-fn pixels pixel-size pixel-distance]
  (apply concat
         (map-indexed (fn [j line]
                        (map-indexed (fn [i [a r g b]]
                                       (let [color (str "rgba(" r "," g "," b "," a ")")]
                                         (draw-fn i j color pixel-size pixel-distance)))
                                     line))
                      pixels)))

(defn canvas-dimensions
  [pixels pixel-size pixel-distance]
  (let [orig-width (count (first pixels))
        orig-height (count pixels)]
    (letfn [(scale [length]
              (+ (* pixel-size length)
                 (* pixel-distance (dec length))))]
      {:width (scale orig-width)
       :height (scale orig-height)})))

(def draw-functions
  {:rect rectangular-pixel
   :circle circular-pixel})

(reacl/defclass viewer this [pixels pixel-size pixel-distance pixel-shape]
  local-state [local-state {}]

  render
  (let [dimensions (canvas-dimensions pixels pixel-size pixel-distance)]
    (apply dom/svg {:id "image"
                    :width (:width dimensions)
                    :height (:height dimensions)}
           (drawn-pixels (draw-functions pixel-shape)
                         pixels
                         pixel-size
                         pixel-distance))))
