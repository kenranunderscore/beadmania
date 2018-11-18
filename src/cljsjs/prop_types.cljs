(ns cljsjs.prop-types
  (:require ["prop-types" :as prop-types]))

(.exportSymbol js/goog "PropTypes" prop-types)
