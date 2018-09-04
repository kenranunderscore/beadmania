(ns beadmania.server
  (:require [compojure.core :as compojure]
            [compojure.route :as route]
            [compojure.handler :as handler]
            [hiccup.core :as h]))

(def index
  (h/html [:script {:src "js/compiled/beadmania.js" :type "text/javascript"}]
          [:body [:p "Beadmania"]]))

(compojure/defroutes main-routes
  (compojure/GET "/" [] index)
  (route/not-found "Page could not be found"))

(def ring-handler
  (handler/site main-routes))
