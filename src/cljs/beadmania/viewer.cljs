(ns beadmania.viewer
  (:require [reacl2.core :as reacl :include-macros true]
            [reacl2.dom :as dom :include-macros true]))

(reacl/defclass viewer this []
  render
  (dom/canvas))
