(ns beadmania.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(reacl/defclass beadmania this []
  render
  (dom/div
   (dom/nav
    {:class "navbar navbar-expand-lg navbar-dark bg-dark"}
    (dom/a
     {:class "navbar-brand"
            :href "#"}
     "beadmania")))

  handle-message
  #(reacl/return))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 {})
