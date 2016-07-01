(function (app) {
    'use strict';
    app.controller('homeCtrl', homeCtrl);

    function bootstrapAlert() { }
    bootstrapAlert.warning = function (message) {
        $('#alert_placeholder').html('<div class="alert"><a class="close" data-dismiss="alert">×</a><span>' + message + '</span></div>')
    }

    function homeCtrl($scope, $http) {
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
                if (data.statusMessage === 'ALREADY_SHORTENED_LINK') {
                    bootstrapAlert.warning('ALREADY SHORTENED LINK');
                }
            }).error(function (data, status, headers, config) {
                var message = data.statusMessage ? data.statusMessage.replace('_', ' ') : "Oops... something went wrong";
                bootstrapAlert.warning();
                $scope.working = false;
            });
        };
    };
})(angular.module('bitlink'));