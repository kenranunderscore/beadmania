(ns beadmania.server
  (:gen-class)
  (:require [compojure.core :as compojure]
            [compojure.route :as route]
            [ring.adapter.jetty :as jetty]
            [ring.middleware.edn :refer [wrap-edn-params]]
            [ring.middleware.reload :refer [wrap-reload]]
            [ring.middleware.params :refer [wrap-params]]
            [ring.middleware.multipart-params :refer [wrap-multipart-params]]
            [ring.util.response :as resp]
            [mikera.image.core :as imagez]
            [beadmania.image-operations :as image-op]
            [beadmania.app :as app]))

(defn edn-response
  [edn]
  (-> edn
      pr-str
      resp/response
      (resp/status 200)
      (resp/content-type "application/edn")
      (resp/charset "utf-8")))

(defn handle-image-upload
  [{:keys [filename size tempfile] :as params}]
  (edn-response (image-op/transform tempfile filename)))

(compojure/defroutes main-routes
  (compojure/POST "/upload"
                  [image]
                  (handle-image-upload image))
  (compojure/GET "/" [] app/index)
  (route/resources "/")
  (route/not-found "Page could not be found"))

(def prod-ring-handler
  (-> main-routes
      wrap-params
      wrap-multipart-params
      wrap-edn-params))

(def dev-ring-handler
  (-> prod-ring-handler
      wrap-reload))

(defn -main
  [& args]
  (let [port (Integer/parseInt (get (System/getenv) "port" "9500"))]
    (jetty/run-jetty prod-ring-handler {:port port})))
