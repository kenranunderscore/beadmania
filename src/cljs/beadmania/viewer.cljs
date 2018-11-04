(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn draw-pixel-as-rect!
  [ctx x y color pixel-size distance]
  (letfn [(offset [index]
            (* index (+ distance pixel-size)))]
    (set! (.-fillStyle ctx) color)
    (.fillRect ctx (offset x) (offset y) pixel-size pixel-size)))

(defn draw-pixel-as-circle!
  [ctx x y color pixel-size distance]
  (let [radius (/ pixel-size 2)]
    (letfn [(offset [index]
              (+ (* index
                    (+ distance pixel-size))
                 radius))]
      (.beginPath ctx)
      (set! (.-fillStyle ctx) color)
      (.arc ctx (offset x) (offset y) radius 0 6.29 false)
      (.fill ctx))))

(defn draw-pixels!
  [ctx pixels pixel-size distance]
  (letfn [(offset [index]
            (* index (+ distance pixel-size)))]
    (doall
     (map-indexed (fn [j line]
                    (doall
                     (map-indexed (fn [i [a r g b]]
                                    (let [color (str "rgba(" r "," g "," b "," a ")")]
                                      (draw-pixel-as-rect! ctx i j color pixel-size distance)))
                                  line)))
                  pixels))))

(defn canvas-dimensions
  [pixels pixel-size distance]
  (let [orig-width (count (first pixels))
        orig-height (count pixels)]
    (letfn [(scale [length]
              (+ (* pixel-size length)
                 (* distance (dec length))))]
      {:width (scale orig-width)
       :height (scale orig-height)})))

;; FIXME: find a way to abstract over "rect"/"circle" calculations
;; to be able to just plug in another calculation
(defn find-index-rect
  [coord pixel-size distance]
  (int (/ coord
          (+ pixel-size distance))))

(reacl/defclass viewer this [pixels]
  local-state [local-state {:pixel-size 20
                            :distance 2}]

  component-did-mount
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          _ (draw-pixels! ctx pixels (:pixel-size local-state) (:distance local-state))]
      (reacl/return :local-state
                    (-> local-state
                        (assoc :context ctx)
                        (assoc :canvas canvas)))))

  component-did-update
  (fn []
    (let [ctx (:context local-state)
          canvas (:canvas local-state)
          _ (.clearRect ctx 0 0 (.-width canvas) (.-height canvas))
          _ (draw-pixels! ctx pixels (:pixel-size local-state) (:distance local-state))]))

  render
  (let [dimensions (canvas-dimensions pixels (:pixel-size local-state) (:distance local-state))]
    (dom/canvas
     {:id "image"
      :onmousemove (fn [e]
                     (let [canvas (:canvas local-state)
                           pixel-size (:pixel-size local-state)
                           distance (:distance local-state)
                           x (- (.-pageX e) (.-offsetLeft canvas))
                           y (- (.-pageY e) (.-offsetTop canvas))
                           i (find-index-rect x pixel-size distance)
                           j (find-index-rect y pixel-size distance)]
                       nil))
      :width (:width dimensions)
      :height (:height dimensions)})))
