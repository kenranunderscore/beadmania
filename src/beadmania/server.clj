(ns beadmania.server
  (:require [compojure.core :as compojure]
            [compojure.route :as route]
            [compojure.handler :as handler]))

(compojure/defroutes main-routes
  (compojure/GET "/" [] "<script src=\"js/compiled/beadmania.js\" type=\"text/javascript\"></script>beadmania")
  (route/not-found "Page could not be found"))

(def ring-handler
  (handler/site main-routes))
