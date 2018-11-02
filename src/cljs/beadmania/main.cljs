(ns beadmania.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.actions :as actions]
            [beadmania.files :as files]))

(defrecord TransformImage [image])

(reacl/defclass beadmania this []
  render
  (dom/div
   (dom/nav
    {:class "navbar navbar-expand-lg navbar-dark bg-dark"}
    (dom/a
     {:class "navbar-brand"
      :href "#"}
     "beadmania"))
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
      (reacl/return :app-state (:image-edn msg))

      (instance? actions/Error msg)
      (.log js/console (:error msg)))))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 (reacl/opt :reduce-action actions/handle-action)
 {})
