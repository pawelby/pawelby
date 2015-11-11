// Creating simple angular js directive for text "Loading..."

(function () {
    "use strict";

    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/wait-cursor.html"
        };
    }
})();