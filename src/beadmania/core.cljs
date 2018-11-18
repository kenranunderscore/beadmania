(ns beadmania.core
  (:require [beadmania.main :as main]))

(enable-console-print!)

;; define your app data so that it doesn't get over-written on reload

(defn on-js-reload []
  ;; optionally touch your app-state to force rerendering depending on
  ;; your application
  ;; (swap! app-state update-in [:__figwheel_counter] inc)
  )

(defn init!
  []
  (println "Successfully loaded"))

(defn reload!
  []
  (println "Code updated"))
