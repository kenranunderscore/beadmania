(ns beadmania.server
  (:require [compojure.core :as compojure]
            [compojure.route :as route]
            [compojure.handler :as handler]
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
  (route/not-found "Page could not be found"))

(def ring-handler
  (handler/site main-routes))
