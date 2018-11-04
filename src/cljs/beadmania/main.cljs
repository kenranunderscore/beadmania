(ns beadmania.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.actions :as actions]
            [beadmania.files :as files]
            [beadmania.viewer :as viewer]
            [beadmania.sidebar :as sidebar]))

(reacl/defclass beadmania this app-state []
  render
  (dom/div
   (dom/nav
    {:class "navbar navbar-expand-lg navbar-dark bg-dark"}
    (dom/a
     {:class "navbar-brand"
      :href "#"}
     "beadmania"))
   (dom/div
    {:class "row"}
    (dom/div
     {:class "col-3"}
     (sidebar/sidebar-content
      (reacl/opt :reduce-action actions/handle-action
                 :embed-app-state merge)))
    (dom/div
     {:class "col-9"}
     (when-let [pixels (:pixels app-state)]
       (viewer/viewer pixels))))))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 {})
