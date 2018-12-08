(ns beadmania.color-distance-test
  (:require [beadmania.color-distance :as color-distance]
            [beadmania.test-util :as test-util]
            #?(:clj [clojure.test :refer [deftest testing is]]
               :cljs [cljs.test :refer-macros [deftest testing is]])))

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
                          0.01))))
