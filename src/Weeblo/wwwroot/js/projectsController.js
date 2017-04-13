// projectsController.js
(function () {
    "use strict";

    //Getting the existing module
    angular.module("app-projects")
    .controller("projectsController", projectsController);

    //Can include dependencies in function
    function projectsController($http) {

        var vm = this;

        vm.projects = [];

        vm.newProject = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/project")
        .then(function (response) {
            //Success
            angular.copy(response.data, vm.projects);
        }, function () {
            //Failure
            vm.errorMessage = "Failed to load data.";
        })
        .finally(function(){
            vm.isBusy = false});  //Promise object first is what happens when succeeds, second is when fails

        vm.addProject = function () {
            vm.isBusy = true;
            vm.errorMessage = ""; //Need to clear error message every time

            $http.post("/api/project", vm.newProject)//Posts body to api
            .then(function (response) {
                //success
                vm.projects.push(response.data);
                vm.newProject = {}; //clears form
            }, function () {
                //failure
                vm.errorMessage = "Failed to save new project";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };
    }

})();
