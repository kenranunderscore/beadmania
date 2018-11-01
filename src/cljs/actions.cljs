(ns beadmania.actions
  (:require [ajax.core :refer [POST]]
            [reacl2.core :as reacl]
            [cljs.reader :as reader]))

(defrecord TransformImage [target image])
(defrecord TransformImageSuccess [image-edn])
(defrecord Error [error])

(defn handle-action
  [app-state action]
  (cond
    (instance? TransformImage action)
    (let [target (:target action)]
      (POST "/upload" {:body (doto (js/FormData.)
                               (.append "image" (:image action)))
                       :keywords? true
                       :handler (fn [e] (reacl/send-message! target (->TransformImageSuccess (reader/read-string e))))
                       :error-handler (fn [e] (reacl/send-message! target (->Error e)))}))))
