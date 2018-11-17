(ns beadmania.controls
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(defrecord Select [value])

(reacl/defclass radio-group this selected-value [options id]
  render
  (dom/div
   (map-indexed (fn [i [value text]]
                  (let [radio-id (str id i)]
                    (dom/keyed
                     i
                     (dom/div
                      {:class "form-check form-check-inline"}
                      (dom/input
                       {:class "form-check-input"
                        :type "radio"
                        :checked (= value selected-value)
                        :onchange #(reacl/send-message! this (->Select value))
                        :id radio-id
                        :name id})
                      (dom/label
                       {:class "form-check-label"
                        :htmlFor radio-id}
                       text)))))
                options))

  handle-message
  (fn [msg]
    (cond
      (instance? Select msg)
      (reacl/return :app-state (:value msg)))))

(reacl/defclass range-slider this value [min max id text]
  render
  (dom/div
   (dom/label {:htmlFor id}
              text)
   (dom/input {:type "range"
               :class "form-control-range"
               :min min
               :max max
               :id id
               :value value
               :onchange (fn [e]
                           (reacl/send-message! this (->Select (int (.. e -target -value)))))}))

  handle-message
  (fn [msg]
    (cond
      (instance? Select msg)
      (reacl/return :app-state (:value msg)))))
