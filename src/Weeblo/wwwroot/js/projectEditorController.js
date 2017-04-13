//projectEditorController.js
(function () {
    "use strict";

    angular.module("app-projects")
    .controller("projectEditorController", projectEditorController);

    function projectEditorController($routeParams, $http) {
        var vm = this;

        vm.projectName = $routeParams.projectName;
        vm.stories = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStory = {};

        var url = "/api/project/" + vm.projectName + "/stories"

        $http.get(url)
        .then(function (response) {
            //success
            angular.copy(response.data, vm.stories);
        }, function (err) {
            //failure
            vm.errorMessage = "Failed to load stories";
        })
        .finally(function () {
            vm.isBusy = false;
        });

        vm.addStory = function () {

            vm.isBusy = true;

            $http.post(url, vm.newStory)
            .then(function (response) {
                //success
                vm.stories.push(response.data);
                vm.newStory = {};
            }, function (err) {
                //failure
                vm.errorMessage = "Failed to add new story";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };

    }

    //Use underscore for private functions

})();