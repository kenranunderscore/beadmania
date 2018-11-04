(ns beadmania.sidebar
  (:require [reacl2.core :as reacl]
            [reacl2.dom :as dom]
            [beadmania.files :as files]
            [beadmania.actions :as actions]))

(defrecord TransformImage [image])

(reacl/defclass sidebar-content this app-state []
  render
  (dom/div
   (files/file-chooser
    (reacl/opt :reaction (reacl/reaction this ->TransformImage))
    nil
    "image-upload"
    "image/png, image/bmp, image/jpeg, image/jpg"))

  handle-message
  (fn [msg]
    (cond
      (instance? TransformImage msg)
      (reacl/return :action (actions/->TransformImage this (:image msg)))

      (instance? actions/TransformImageSuccess msg)
      (reacl/return :app-state (assoc app-state :pixels (:pixels msg)))

      (instance? actions/Error msg)
      (.log js/console (:error msg)))))
