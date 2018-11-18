# beadmania

The purpose of this program is to convert (and possibly resize) images to a
fixed set of colors. This will be done using color spaces and respective
algorithms. Since these are made to emulate the perception of the human eye,
the results are much better than when, for instance, just taking the Euclidean
distance between two RGB values - which most other programs seem to do.

The application is specially tailored for people who like to create "bead art",
that is, using beads (Perler, Nabbi, Hama, and so on). I plan to include
default sets of colors to target these manufacturers, but also to implement
a possibility to create custom color sets.

### Building and running the code

This project now uses the [Clojure CLI tools](https://clojure.org/guides/deps_and_cli)
for the Clojure part, and [shadow-cljs](shadow-cljs.org) for ClojureScript
compilation, hot reloading and live testing.

#### Backend

To start the server, use:

```
clj -A:run
```

#### Frontend

Compile and watch the ClojureScript code for changes:

```
shadow-cljs watch beadmania
```

##### Testing the frontend

If you want live-testing in the browser, you can execute

```
shadow-cljs watch beadmania test
```

or, for testing against node:

```
shadow-cljs watch beadmania node-test
```

#### Production build

Doing a production build is kind of a hassle still. First, make a release
build of the ClojureScript code via:

```
shadow-cljs release beadmania
```

Then create an uberjar using the Clojure CLI tools:

```
clj -A:uberjar
```

This uberjar can the be run with:

```
java -jar beadmania.jar -m beadmania.server
```
