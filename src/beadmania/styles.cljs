(ns beadmania.styles)

(def flex-row
  {:display "flex"
   :flex-direction "row"})

(def flex-column
  {:display "flex"
   :flex-direction "column"})

(def viewport-height
  {:height "100vh"})

(def grid
  {:display "grid"})

(def main-grid
  (merge grid
         viewport-height
         {:grid-template-columns "300px 1fr"
          :grid-template-rows "56px 1fr"}))
