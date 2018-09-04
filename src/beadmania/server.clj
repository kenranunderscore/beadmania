(ns beadmania.server)

(defn ring-handler
  [request]
  {:status 200
   :headers {"Content-Type" "text/html"}
   :body "beadmania"})

