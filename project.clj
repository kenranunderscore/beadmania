(defproject beadmania "0.1.0-SNAPSHOT"
  :description "FIXME: write this!"
  :url "http://example.com/FIXME"
  :license {:name "Eclipse Public License"
            :url "http://www.eclipse.org/legal/epl-v10.html"}

  :min-lein-version "2.7.1"

  :auto-clean false

  :dependencies [[org.clojure/clojure "1.9.0"]
                 [org.clojure/clojurescript "1.10.339"]
                 [ring "1.7.0-RC2"]
                 [hiccup "1.0.5"]
                 [reacl "2.0.5"]
                 [compojure "1.6.1"]
                 [cljs-ajax "0.7.5"]
                 [net.mikera/imagez "0.12.0"]
                 [fogus/ring-edn "0.3.0"]]

  :plugins [[lein-cljsbuild "1.1.7"]]

  :source-paths ["src/clj" "src/cljs"]

  :aliases {"fig"       ["trampoline" "run" "-m" "figwheel.main"]
            "fig:build" ["trampoline" "run" "-m" "figwheel.main" "-b" "dev" "-r"]
            "fig:min"   ["run" "-m" "figwheel.main" "-O" "advanced" "-bo" "dev"]
            "fig:test"  ["run" "-m" "figwheel.main" "-co" "test.cljs.edn" "-m" beadmania.test-runner]
            "prod"      ["do" "clean" ["cljsbuild" "once"] "uberjar"]}

  :main ^:skip-aot beadmania.server

  :cljsbuild {:builds
              [{:source-paths ["src"]
                :compiler {:main beadmania.core
                           :output-to "resources/public/cljs-out/dev-main.js"
                           :optimizations :simple
                           :pretty-print false}}]}

  :profiles {:dev {:dependencies [[com.bhauman/figwheel-main "0.1.9"]
                                  [com.bhauman/rebel-readline-cljs "0.1.4"]]
                   :resource-paths ["target"]
                   ;; need to add the compliled assets to the :clean-targets
                   :clean-targets ^{:protect false} ["target"]}
             :uberjar {:aot :all}})
