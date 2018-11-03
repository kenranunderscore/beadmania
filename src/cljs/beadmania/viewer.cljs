(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn draw-pixels!
  [ctx pixels]
  (doall
   (map-indexed (fn [j line]
                  (doall
                   (map-indexed (fn [i [a r g b]]
                                  (let [color (str "rgba(" r "," g "," b "," a ")")]
                                    (set! (.-fillStyle ctx) color)
                                    (.fillRect ctx i j (inc i) (inc j))))
                                line)))
                pixels)))

(reacl/defclass viewer this [pixels]
  component-did-mount
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          _ (draw-pixels! ctx pixels)]
      (reacl/return)))

  component-did-update
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          _ (draw-pixels! ctx pixels)]))

  render
  (dom/canvas
   {:id "image"
    :width (* 1 (count (first pixels)))
    :height (* 1 (count pixels))}))
