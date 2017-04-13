//app-projects.js
(function () {
    "use strict";

    //Creating the modules
    angular.module("app-projects", ["simpleControls", "ngRoute"])
    .config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "projectsController",
            controllerAs: "vm",
            templateUrl:"/views/projectsView.html"
        });

        //"/:tripName" is a route parameter
        $routeProvider.when("/editor/:projectName", { 
            controller: "projectEditorController",
            controllerAs: "vm",
            templateUrl: "/views/projectEditorView.html"
        });

        $routeProvider.otherwise({redirectTo: "/"}); //default call

    }); //must put directives in main angular module as dependency

})();