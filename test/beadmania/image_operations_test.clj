(ns beadmania.image-operations-test
  (:require [clojure.test :refer [deftest is testing]]
            [beadmania.image-operations :as image-op]))

(deftest color-conversions-are-inverse
  (is (= [10 20 30 40]
         (image-op/argb->color (image-op/color->argb [10 20 30 40]))))
  (is (= 4294967173
         (image-op/color->argb (image-op/argb->color 4294967173)))))

(deftest argb->rgb
  (is (= [253 153 153]
         (image-op/argb->rgb [0.4 250 0 0]))))
