/// <reference path="../services/authService.js" />

'use strict';

dndApp.controller('HomeController',
    function HomeController($scope, $location, updatesService, authService) {
        if (!authService.isAuthenticated()) {
            $location.url('/login');
        };
        $scope.updates = function () {
            $location.url('/updates');
        };
        $scope.items = function () {
            $location.url('/items');
        };
        $scope.briefs = function () {
            $location.url('/briefs');
        };
        $scope.spells = function () {
            $location.url('/spells');
        };
    });