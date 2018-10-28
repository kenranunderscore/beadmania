(ns beadmania.actions
  (:require [ajax.core :refer [POST]]))

(defrecord TransformImage [image])

(defn handle-action
  [app-state action]
  (.log js/console (:image action))
  (cond
    (instance? TransformImage action)
    (POST "/upload" {:body (doto (js/FormData.)
                               (.append "image" (:image action)))
                     :keywords? true
                     :handler (fn [e] (.log js/console e))
                     :error-handler (fn [e] (.log js/console e))})))
