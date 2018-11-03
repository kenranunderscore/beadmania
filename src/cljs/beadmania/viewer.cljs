(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn draw-pixel-as-rect!
  [ctx x y color scaling-factor distance]
  (letfn [(offset [index]
            (* index (+ distance scaling-factor)))]
    (set! (.-fillStyle ctx) color)
    (.fillRect ctx (offset x) (offset y) scaling-factor scaling-factor)))

(defn draw-pixel-as-circle!
  [ctx x y color scaling-factor distance]
  (let [radius (/ scaling-factor 2)]
    (letfn [(offset [index]
              (+ (* index
                    (+ distance scaling-factor))
                 radius))]
      (.beginPath ctx)
      (set! (.-fillStyle ctx) color)
      (.arc ctx (offset x) (offset y) radius 0 6.29 false)
      (.fill ctx))))

(defn draw-pixels!
  [ctx pixels scaling-factor distance]
  (letfn [(offset [index]
            (* index (+ distance scaling-factor)))]
    (doall
     (map-indexed (fn [j line]
                    (doall
                     (map-indexed (fn [i [a r g b]]
                                    (let [color (str "rgba(" r "," g "," b "," a ")")]
                                      (draw-pixel-as-rect! ctx i j color scaling-factor distance)))
                                  line)))
                  pixels))))

(defn canvas-dimensions
  [pixels scaling-factor distance]
  (let [orig-width (count (first pixels))
        orig-height (count pixels)]
    (letfn [(scale [length]
              (+ (* scaling-factor length)
                 (* distance (dec length))))]
      {:width (scale orig-width)
       :height (scale orig-height)})))

(reacl/defclass viewer this [pixels]
  local-state [local-state {:scaling-factor 20
                            :distance 2}]

  component-did-mount
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          _ (draw-pixels! ctx pixels (:scaling-factor local-state) (:distance local-state))]
      (reacl/return :local-state
                    (-> local-state
                        (assoc :context ctx)
                        (assoc :canvas canvas)))))

  component-did-update
  (fn []
    (let [ctx (:context local-state)
          canvas (:canvas local-state)
          _ (.clearRect ctx 0 0 (.-width canvas) (.-height canvas))
          _ (draw-pixels! ctx pixels (:scaling-factor local-state) (:distance local-state))]))

  render
  (let [dimensions (canvas-dimensions pixels (:scaling-factor local-state) (:distance local-state))]
    (dom/canvas
     {:id "image"
      :width (:width dimensions)
      :height (:height dimensions)})))
