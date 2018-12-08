(ns beadmania.color-test
  (:require [beadmania.color :as color]
            [clojure.test.check :as check]
            [clojure.test.check.generators :as gen]
            [clojure.test.check.properties :as prop]
            #?(:clj [clojure.test :refer [deftest testing is]]
               :cljs [cljs.test :refer-macros [deftest testing is]])))

(def rgb-color-gen
  (gen/vector (gen/large-integer* {:min 0 :max 255})
              3))

(def argb-color-gen
  (gen/vector (gen/large-integer* {:min 0 :max 255})
              4))

(deftest color-conversions-are-inverse
  (is (= [10 20 30 40]
         (color/argb->color (color/color->argb [10 20 30 40]))))
  (is (= 1684288150
         (color/color->argb (color/argb->color 1684288150)))))

(deftest t-color->rgb-color
  (is (= [253 153 153]
         (color/color->rgb-color [102 250 0 0]))))

(deftest argb->color-is-left-inverse-to-color->argb
  (is (true?
       (:result
        (check/quick-check
         100
         (prop/for-all [v argb-color-gen]
                       (= v
                          (->> v
                               color/color->argb
                               color/argb->color))))))))

(deftest color->rgb-color-produces-sane-values
  (is (true?
       (:result
        (check/quick-check
         100
         (prop/for-all [v rgb-color-gen]
                       (not-any? #(or (neg? %)
                                      (> % 255))
                                 (color/color->rgb-color v))))))))
