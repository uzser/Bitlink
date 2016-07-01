(function (app) {
    'use strict';

    app.controller('homeCtrl', function ($scope, $http) {
            $scope.message = "";
            $scope.url = "";
            $scope.working = false;

            $scope.postUrl = function () {
                $scope.working = true;
                $scope.answered = false;
                $scope.message = "shorting...";

                $http.post("/api/links", JSON.stringify($scope.url)).success(function (data, status, headers, config) {
                    $scope.url = data.data.shortUrl;
                    $scope.message = data.statusMessage;
                    $scope.working = false;
                }).error(function (data, status, headers, config) {
                    $scope.message = "Oops... something went wrong";
                    $scope.working = false;
                });
            };
        });
})(angular.module('bitlink'));