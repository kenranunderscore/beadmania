(ns beadmania.client.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn foo
  [& args]
  (dom/div
   "Foo"))

(reacl/defclass beadmania
  this app-state []
  render
  (dom/div
   (foo this app-state))
  handle-message
  (fn [msg]
    (reacl/return :app-state app-state)))

(defn ^:export run
  [dom]
  (reacl/render-component
   dom
   beadmania))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 {})
