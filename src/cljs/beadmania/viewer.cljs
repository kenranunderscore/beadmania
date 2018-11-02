(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn draw-pixels!
  [ctx pixels]
  (set! (.-fillStyle ctx) "#ff0000")
  (.fillRect ctx 0 0 2 3))

(reacl/defclass viewer this [image-edn]
  component-did-mount
  #(let [canvas (.getElementById js/document "image")
         ctx (.getContext canvas "2d")]
     (draw-pixels! ctx image-edn))

  render
  (dom/canvas
   {:id "image"
    :width (count (first image-edn))
    :height (count image-edn)}))
