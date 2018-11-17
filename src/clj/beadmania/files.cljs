(ns beadmania.files
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defrecord UploadFile [file])

(reacl/defclass file-chooser this file [id accept]
  render
  (dom/div
   (dom/button
    {:class "btn btn-primary"
     :type "button"
     :onclick #(.click (.getElementById js/document id))}
    "Choose File")
   (dom/input
    {:type "file"
     :id id
     :style {:display "none"}
     :accept accept
     :onchange (fn [e]
                 (let [files (.. e -target -files)]
                   (when (pos? (alength files))
                     (reacl/send-message! this (->UploadFile (aget files 0))))))}))
  handle-message
  (fn [msg]
    (cond
      (instance? UploadFile msg)
      (reacl/return :app-state (:file msg)))))
