(function () {
    'use strict';

    angular.module('bitlink', ['ngRoute'])
        .config([
            '$routeProvider', '$locationProvider',
            function ($routeProvider) {
                $routeProvider
                    .when("/", {
                        templateUrl: "scripts/spa/home/index.html",
                        controller: "homeCtrl"
                    })
                    .when("/links", {
                        templateUrl: "scripts/spa/links/links.html",
                        controller: "linksCtrl"
                    }).otherwise({ redirectTo: "/" });
            }
        ]);
})();