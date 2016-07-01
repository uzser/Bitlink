(function (app) {
    'use strict';
    app.controller('linksCtrl', linksCtrl);

    function linksCtrl($scope, $http) {
        $scope.loadingLinks = true;
        $scope.message = "Loading links...";

        function loadData() {
            $http.get("/api/links", JSON.stringify($scope.url)).success(function (data, status, headers, config) {
                $scope.links = data.data;
                $scope.message = data.statusMessage;
                $scope.loadingLinks = false;
            }).error(function (data, status, headers, config) {
                $scope.message = "Oops... something went wrong";
            });
        };

        loadData();
    };
})(angular.module('bitlink'));