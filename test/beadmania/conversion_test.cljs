(ns beadmania.conversion-test
  (:require [beadmania.conversion :as conversion]
            [cljs.test :refer-macros [deftest testing is]]))

(defn close?
  [x y tolerance]
  (<= (Math/abs (float (- x y)))
      tolerance))

(defn vectors-close?
  [v w tolerance]
  (every? true?
          (map #(close? %1 %2 tolerance) v w)))

(deftest t-rgb->xyz
  (testing "White is converted correctly"
    (is (vectors-close? [95.05 100 108.89]
                        (conversion/rgb->xyz [255 255 255])
                        0.01)))
  (testing "Black is converted correctly"
    (is (vectors-close? [0 0 0]
                        (conversion/rgb->xyz [0 0 0])
                        0.01)))
  (testing "Arbitrary color is converted correctly"
    (is (vectors-close? [29.08 13.23 83.46]
                        (conversion/rgb->xyz [154 3 240])
                        0.01))))

(deftest t-xyz->lab
  (testing "Zero vector is converted to zero"
    (is (vectors-close? [0 0 0]
                        (conversion/xyz->lab [0 0 0])
                        0.01)))
  (testing "Arbitrary vector is converted correctly"
    (is (vectors-close? [83.57 -60.64 74]
                        (conversion/xyz->lab [38.06 63.24 12.68])
                        0.01))))

(deftest t-delta-e94
  (testing "Distance to zero is calculated correctly"
    (is (close? 146.23
                (conversion/delta-e94 [0 0 0]
                                      [50 -50 128])
                0.01)))
  (testing "Distance between arbitrary vectors is calculated correctly"
    (is (close? 108.47
                (conversion/delta-e94 [100 128 -128]
                                      [0 20 40])
                0.01))))
