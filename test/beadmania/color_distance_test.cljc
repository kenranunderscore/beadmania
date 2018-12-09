(ns beadmania.color-distance-test
  (:require [beadmania.color :as color]
            [beadmania.color-test :as color-test]
            [beadmania.color-distance :as color-distance]
            [beadmania.test-util :as test-util]
            [clojure.test.check :as check]
            [clojure.test.check.generators :as gen]
            [clojure.test.check.properties :as prop]
            #?(:clj [clojure.test :refer [deftest testing is]]
               :cljs [cljs.test :refer-macros [deftest testing is]])))

(def lab-vector-gen
  (gen/fmap color/rgb->lab
            color-test/rgb-color-gen))

(deftest t-delta-e94
  (testing "Distance to zero is calculated correctly"
    (is (test-util/close? 146.23
                          (color-distance/delta-e94 [0 0 0]
                                                    [50 -50 128])
                          0.01)))
  (testing "Distance between arbitrary vectors is calculated correctly"
    (is (test-util/close? 108.47
                          (color-distance/delta-e94 [100 128 -128]
                                                    [0 20 40])
                          0.01)))
  (testing "Distance between x and x is zero"
    (is (true?
         (:result
          (check/quick-check
           100
           (prop/for-all [v lab-vector-gen]
                         (test-util/close? 0
                                           (color-distance/delta-e94 v v)
                                           0.01)))))))
  (testing "Distance is non-negative"
    (is (true?
         (:result
          (check/quick-check
           100
           (prop/for-all [v lab-vector-gen
                          w lab-vector-gen]
                         (>= (color-distance/delta-e94 v w)
                             -0.01))))))))
