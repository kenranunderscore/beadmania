(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.sidebar :as sidebar]))

(defn draw-pixel-as-rect!
  [ctx x y color pixel-size pixel-distance]
  (letfn [(offset [index]
            (* index (+ pixel-distance pixel-size)))]
    (set! (.-fillStyle ctx) color)
    (.fillRect ctx (offset x) (offset y) pixel-size pixel-size)))

(defn draw-pixel-as-circle!
  [ctx x y color pixel-size pixel-distance]
  (let [radius (/ pixel-size 2)]
    (letfn [(offset [index]
              (+ (* index
                    (+ pixel-distance pixel-size))
                 radius))]
      (.beginPath ctx)
      (set! (.-fillStyle ctx) color)
      (.arc ctx (offset x) (offset y) radius 0 6.29 false)
      (.fill ctx))))

(defn draw-pixels!
  [ctx draw-fn pixels pixel-size pixel-distance]
  (letfn [(offset [index]
            (* index (+ pixel-distance pixel-size)))]
    (doall
     (map-indexed (fn [j line]
                    (doall
                     (map-indexed (fn [i [a r g b]]
                                    (let [color (str "rgba(" r "," g "," b "," a ")")]
                                      (draw-fn ctx i j color pixel-size pixel-distance)))
                                  line)))
                  pixels))))

(defn canvas-dimensions
  [pixels pixel-size pixel-distance]
  (let [orig-width (count (first pixels))
        orig-height (count pixels)]
    (letfn [(scale [length]
              (+ (* pixel-size length)
                 (* pixel-distance (dec length))))]
      {:width (scale orig-width)
       :height (scale orig-height)})))

;; FIXME: find a way to abstract over "rect"/"circle" calculations
;; to be able to just plug in another calculation
(defn find-index-rect
  [coord pixel-size pixel-distance]
  (int (/ coord
          (+ pixel-size pixel-distance))))

(defn determine-draw-fn
  [pixel-shape]
  (case pixel-shape
    :rect draw-pixel-as-rect!
    :circle draw-pixel-as-circle!))

(reacl/defclass viewer this [pixels pixel-size pixel-distance pixel-shape]
  local-state [local-state {}]

  component-did-mount
  (fn []
    (let [canvas (.getElementById js/document "image")
          ctx (.getContext canvas "2d")
          draw-fn (determine-draw-fn pixel-shape)
          _ (draw-pixels! ctx draw-fn pixels pixel-size pixel-distance)]
      (reacl/return :local-state
                    (-> local-state
                        (assoc :context ctx)))))

  component-did-update
  (fn []
    (let [ctx (:context local-state)
          canvas (.-canvas ctx)
          draw-fn (determine-draw-fn pixel-shape)
          _ (.clearRect ctx 0 0 (.-width canvas) (.-height canvas))
          _ (draw-pixels! ctx draw-fn pixels pixel-size pixel-distance)]))

  render
  (let [dimensions (canvas-dimensions pixels pixel-size pixel-distance)]
    (dom/canvas
     {:id "image"
      :onmousemove (fn [e]
                     (let [canvas (.getElementById js/document "image")
                           pixel-size pixel-size
                           pixel-distance pixel-distance
                           x (- (.-pageX e) (.-offsetLeft canvas))
                           y (- (.-pageY e) (.-offsetTop canvas))
                           i (find-index-rect x pixel-size pixel-distance)
                           j (find-index-rect y pixel-size pixel-distance)]
                       nil))
      :width (:width dimensions)
      :height (:height dimensions)})))
