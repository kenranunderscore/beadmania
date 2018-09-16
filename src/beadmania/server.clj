(ns beadmania.server
  (:gen-class)
  (:require [compojure.core :as compojure]
            [compojure.route :as route]
            [compojure.handler :as handler]
            [ring.adapter.jetty :as jetty]
            [hiccup.page :as h]))

(defn script-file
  [filename]
  [:script {:type "text/javascript" :src filename}])

(def index
  (h/html5
   [:html
    [:head {}
     [:title "beadmania"]]
    [:body {}
     [:div {:id "root"}]
     (script-file "cljs-out/dev-main.js")]]))

(compojure/defroutes main-routes
  (compojure/GET "/" [] index)
  (route/resources "/")
  (route/not-found "Page could not be found"))

(def ring-handler
  (handler/site main-routes))

(defn -main
  [& args]
  (let [port (Integer/parseInt (get (System/getenv) "port" "9500"))]
    (jetty/run-jetty ring-handler {:port port})))
