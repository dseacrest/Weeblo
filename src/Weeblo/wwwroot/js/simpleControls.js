//simpleControls.js
//Directives return an object with well known properties
(function () {
    "use strict";

    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show:"=displayWhen"
            },//Object we are binding wait cursor to
            restrict: "E", //Restricts it to element style only
            templateUrl:"/views/waitCursor.html"
        };
    }
})(); //must put double brackets at end