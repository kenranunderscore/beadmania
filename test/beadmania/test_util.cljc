(ns beadmania.test-util)

(defn close?
  [x y tolerance]
  (<= (Math/abs (float (- x y)))
      tolerance))

(defn vectors-close?
  [v w tolerance]
  (every? true?
          (map #(close? %1 %2 tolerance) v w)))
