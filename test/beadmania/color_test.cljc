(ns beadmania.color-test
  (:require [beadmania.color :as color]
            [beadmania.test-util :as test-util]
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

(deftest t-rgb->xyz
  (testing "White is converted correctly"
    (is (test-util/vectors-close? [95.05 100 108.89]
                                  (color/rgb->xyz [255 255 255])
                                  0.01)))
  (testing "Black is converted correctly"
    (is (test-util/vectors-close? [0 0 0]
                                  (color/rgb->xyz [0 0 0])
                                  0.01)))
  (testing "Arbitrary color is converted correctly"
    (is (test-util/vectors-close? [29.08 13.23 83.46]
                                  (color/rgb->xyz [154 3 240])
                                  0.01))))

(deftest t-xyz->lab
  (testing "Zero vector is converted to zero"
    (is (test-util/vectors-close? [0 0 0]
                                  (color/xyz->lab [0 0 0])
                                  0.01)))
  (testing "Arbitrary vector is converted correctly"
    (is (test-util/vectors-close? [83.57 -60.64 74]
                                  (color/xyz->lab [38.06 63.24 12.68])
                                  0.01))))
