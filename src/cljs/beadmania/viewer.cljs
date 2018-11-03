(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn draw-pixels!
  [ctx pixels scale]
  (doall
   (map-indexed (fn [j line]
                  (doall
                   (map-indexed (fn [i [a r g b]]
                                  (let [color (str "rgba(" r "," g "," b "," a ")")]
                                    (set! (.-fillStyle ctx) color)
                                    (.fillRect ctx (* scale i) (* scale j) scale scale)))
                                line)))
                pixels)))

(reacl/defclass viewer this [pixels]
  local-state [local-state {:scale 5}]

  component-did-mount
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          _ (draw-pixels! ctx pixels (:scale local-state))]
      (reacl/return :local-state
                    (-> local-state
                        (assoc :context ctx)
                        (assoc :canvas canvas)))))

  component-did-update
  (fn []
    (let [ctx (:context local-state)
          canvas (:canvas local-state)
          _ (.clearRect ctx 0 0 (.-width canvas) (.-height canvas))
          _ (draw-pixels! ctx pixels (:scale local-state))]))

  render
  (let [scale (:scale local-state)]
    (dom/canvas
     {:id "image"
      :width (* scale (count (first pixels)))
      :height (* scale (count pixels))})))
