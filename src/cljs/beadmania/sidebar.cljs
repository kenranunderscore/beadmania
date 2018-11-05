(ns beadmania.sidebar
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.files :as files]
            [beadmania.actions :as actions]
            [beadmania.controls :as controls]))

(defrecord TransformImage [image])
(defrecord ChangePixelSize [value])
(defrecord ChangePixelDistance [value])

(reacl/defclass sidebar-content this app-state []
  render
  (dom/div
   {:class "col mt-3"}
   (dom/div
    {:class "form-group"}
    (files/file-chooser
     (reacl/opt :reaction (reacl/reaction this ->TransformImage))
     nil
     "image-upload"
     "image/png, image/bmp, image/jpeg, image/jpg")
    (dom/div
     {:class "mt-3"}
     (controls/range-slider (reacl/opt :reaction (reacl/reaction this ->ChangePixelSize))
                            (:pixel-size app-state)
                            1 50
                            "pixel-size-slider"
                            "Ajust pixel size"))
    (dom/div
     {:class "mt-3"}
     (controls/range-slider (reacl/opt :reaction (reacl/reaction this ->ChangePixelDistance))
                            (:pixel-distance app-state)
                            0 5
                            "pixel-distance-slider"
                            "Adjust pixel distance"))
    (dom/div
     {:class "mt-3"}
     (controls/radio-group (reacl/opt :embed-app-state
                                      (fn [state shape]
                                        (assoc state :pixel-shape shape)))
                           (or (:pixel-shape app-state) :rect)
                           [[:rect "Rectangle"] [:circle "Circle"]]
                           "shape-selection"))))

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
