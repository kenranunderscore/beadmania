(ns beadmania.image-operations-test
  (:require [beadmania.image-operations :as image-op]
            [clojure.test :refer [deftest is testing]]
            [clojure.test.check :as check]
            [clojure.test.check.generators :as gen]
            [clojure.test.check.properties :as prop]
            [clojure.test.check.clojure-test :refer [defspec]]))

(def rgb-color-gen
  (gen/vector (gen/large-integer* {:min 0 :max 255})
              3))

(deftest color-conversions-are-inverse
  (is (= [10 20 30 40]
         (image-op/argb->color (image-op/color->argb [10 20 30 40]))))
  (is (= 4294967173
         (image-op/color->argb (image-op/argb->color 4294967173)))))

(deftest t-color->rgb-color
  (is (= [253 153 153]
         (image-op/color->rgb-color [102 250 0 0]))))

(defspec argb->color-is-left-inverse-to-color->argb
  100
  (prop/for-all [v rgb-color-gen]
                (= v
                   (->> v
                        image-op/color->argb
                        image-op/argb->color))))

(defspec color->rgb-color-produces-sane-values
  100
  (prop/for-all [v rgb-color-gen]
                (not-any? #(or (neg? %)
                               (> % 255))
                          (image-op/color->rgb-color v))))
