(ns beadmania.sidebar
  (:require [reacl2.core :as reacl]
            [reacl2.dom :as dom]
            [beadmania.files :as files]
            [beadmania.actions :as actions]))

(defrecord TransformImage [image])
(defrecord ChangePixelSize [value])
(defrecord ChangePixelDistance [value])

(reacl/defclass sidebar-content this app-state []
  render
  (dom/div
   {:class "col"
    :style {:margin-top "1em"}}
   (dom/div
    {:class "form-group"}
    (files/file-chooser
     (reacl/opt :reaction (reacl/reaction this ->TransformImage))
     nil
     "image-upload"
     "image/png, image/bmp, image/jpeg, image/jpg")
    (dom/label
     {:htmlFor "pixel-size"
      :style {:margin-top "1em"}}
     "Adjust pixel size")
    (dom/input
     {:id "pixel-size"
      :type "range"
      :class "form-control-range"
      :min 1
      :max 50
      :value (:pixel-size app-state)
      :onchange (fn [e]
                  (reacl/send-message! this
                                       (->ChangePixelSize (int (.. e -target -value)))))})
    (dom/label
     {:htmlFor "pixel-distance"
      :style {:margin-top "1em"}}
     "Adjust pixel distance")
    (dom/input
     {:id "pixel-distance"
      :type "range"
      :class "form-control-range"
      :min 0
      :max 5
      :value (:pixel-distance app-state)
      :onchange (fn [e]
                  (reacl/send-message! this
                                       (->ChangePixelDistance (int (.. e -target -value)))))})))

  handle-message
  (fn [msg]
    (cond
      (instance? TransformImage msg)
      (reacl/return :action (actions/->TransformImage this (:image msg)))

      (instance? actions/TransformImageSuccess msg)
      (reacl/return :app-state (assoc app-state :pixels (:pixels msg)))

      (instance? actions/Error msg)
      (.log js/console (:error msg))

      (instance? ChangePixelSize msg)
      (reacl/return :app-state (assoc app-state :pixel-size (:value msg)))

      (instance? ChangePixelDistance msg)
      (reacl/return :app-state (assoc app-state :pixel-distance (:value msg))))))
