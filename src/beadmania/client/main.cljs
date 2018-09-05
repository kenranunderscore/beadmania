(ns beadmania.client.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defn foo
  [& args]
  (dom/div
   "FOO"))

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
