(ns beadmania.main
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]
            [beadmania.actions :as actions]
            [beadmania.files :as files]
            [beadmania.viewer :as viewer]
            [beadmania.sidebar :as sidebar]
            [beadmania.styles :as styles]))

(reacl/defclass beadmania this app-state []
  render
  (dom/div
   {:style styles/main-grid}
   (dom/header
    {:style {:grid-column "span 2"}}
    (dom/nav
     {:class "navbar navbar-expand-lg navbar-dark bg-dark"}
     (dom/a
      {:class "navbar-brand"
       :href "#"}
      "beadmania")))
   (dom/aside
    {:style {:background-color "#f3f3f3"}}
    (sidebar/sidebar-content (reacl/opt :reduce-action actions/handle-action
                                        :embed-app-state merge)
                             (select-keys app-state
                                          [:pixel-size :pixel-distance :pixel-shape :pixels])))
   (dom/main
    {:style {:overflow-y "auto"}}
    (when-let [pixels (:pixels app-state)]
      (viewer/viewer pixels
                     (:pixel-size app-state)
                     (:pixel-distance app-state)
                     (:pixel-shape app-state))))))

(reacl/render-component
 (.getElementById js/document "root")
 beadmania
 {:pixel-size 20
  :pixel-distance 2
  :pixel-shape :rect})
