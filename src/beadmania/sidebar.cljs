(ns beadmania.sidebar
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.files :as files]
            [beadmania.actions :as actions]
            [beadmania.controls :as controls]
            [beadmania.conversion :as conversion]))

(defrecord TransformImage [image])
(defrecord ChangePixelSize [value])
(defrecord ChangePixelDistance [value])
(defrecord Convert [])

(reacl/defclass sidebar-content this app-state []
  render
  (dom/div
   {:class "col mt-3"}
   (dom/div
    {:class "form-group"}
    (files/file-chooser (reacl/opt :reaction (reacl/reaction this ->TransformImage))
                        nil
                        "image-upload"
                        "image/png, image/bmp, image/jpeg, image/jpg")
    (when (:pixels app-state)
      (dom/div
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
                              "shape-selection"))
       (dom/button {:class "btn btn-primary"
                    :onclick (fn [_]
                               (reacl/send-message! this (->Convert)))}
                   "Convert")))))

  handle-message
  (fn [msg]
    (cond
      (instance? Convert msg)
      (let [converted-image (conversion/convert (:pixels app-state)
                                                #{[0 0 0]
                                                  [50 50 50]
                                                  [100 100 100]
                                                  [150 150 150]
                                                  [200 200 200]
                                                  [255 255 255]})]
        (reacl/return :app-state (assoc app-state
                                        :pixels
                                        converted-image)))

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
