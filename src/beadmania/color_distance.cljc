(ns beadmania.color-distance)

(defn square
  "Squares a number `x`."
  [x]
  (* x x))

(defn delta-e94-squared
  "Calculates the squared Delta E94 distance between two L*a*b*
  color vectors."
  [v w]
  (let [a1 (second v)
        a2 (second w)
        b1 (last v)
        b2 (last w)
        c1 (Math/sqrt (+ (square a1) (square b1)))
        c2 (Math/sqrt (+ (square a2) (square b2)))
        da (- a1 a2)
        dc (- c1 c2)
        db (- b1 b2)
        dl (- (first v) (first w))
        rad (- (+ (square da) (square db))
               (square dc))
        dh (if (neg? rad) 0 (Math/sqrt rad)) ;; cljs workaround: no bigdec
        sc (inc (* 0.045 c1))
        sh (inc (* 0.015 c1))
        radicand (+ (square dl) (square (/ dc sc)) (square (/ dh sh)))]
    radicand))

(defn delta-e94
  "Calculates the squared Delta E94 distance between two L*a*b*
  color vectors."
  [v w]
  (Math/sqrt (delta-e94-squared v w)))
