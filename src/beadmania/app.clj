(ns beadmania.app
  (:require [hiccup.page :as page]))

(def index
  (page/html5
   [:html
    [:head {}
     [:title "beadmania"]
     [:meta {:charset "utf-8"}]
     [:meta {:name "viewport"
             :content "width=device-width, initial-scale=1, shrink-to-fit=no"}]
     (page/include-css "/css/bootstrap.min.css")]
    [:body {}
     [:div {:id "root"}]
     (page/include-js "/vendor/js/jquery-3.3.1.slim.min.js")
     (page/include-js "/vendor/js/bootstrap.bundle.min.js")
     (page/include-js "/js/main.js")]]))
