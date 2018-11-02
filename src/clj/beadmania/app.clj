(ns beadmania.app
  (:require [hiccup.page :as page]))

(defn include-stylesheet
  [href & [integrity crossorigin]]
  [:link {:rel "stylesheet"
          :href href
          :integrity integrity
          :crossorigin crossorigin}])

(defn include-js
  [src & [integrity crossorigin]]
  [:script {:type "text/javascript"
            :src src
            :integrity integrity
            :crossorigin crossorigin}])

(def index
  (page/html5
   [:html
    [:head {}
     [:title "beadmania"]
     [:meta {:charset "utf-8"}]
     [:meta {:name "viewport"
             :content "width=device-width, initial-scale=1, shrink-to-fit=no"}]
     (include-stylesheet "https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"
                         "sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO"
                         "anonymous")]
    [:body {}
     [:div {:id "root"}]
     (include-js "https://code.jquery.com/jquery-3.3.1.slim.min.js"
                 "sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
                 "anonymous")
     (include-js "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"
                 "sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49"
                 "anonymous")
     (include-js "https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"
                 "sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy"
                 "anonymous")
     (include-js "cljs-out/dev-main.js")]]))
