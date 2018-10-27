(ns beadmania.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defrecord UploadFile [file])

(reacl/defclass file-chooser this file []
  render
  (dom/form
   (dom/input
    {:type "file"
     :class "form-control-file"
     :accept "image/png, image/bmp, image/jpeg"
     :onchange (fn [e]
                 (let [files (.. e -target -files)]
                   (when (pos? (alength files))
                     (reacl/send-message! this (->UploadFile (aget files 0))))))}))
  handle-message
  (fn [msg]
    (cond
      (instance? UploadFile msg)
      (.log js/console (:file msg)))))

(reacl/defclass beadmania this []
  render
  (dom/div
   (dom/nav
    {:class "navbar navbar-expand-lg navbar-dark bg-dark"}
    (dom/a
     {:class "navbar-brand"
      :href "#"}
     "beadmania"))
   (file-chooser nil))

  handle-message
  #(reacl/return))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 {})
