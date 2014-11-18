/// <reference path="../angular.min.js" />
'use strict';

var dndApp = angular.module('dndApp', ['ngRoute', 'ngResource', 'ngCookies'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/home', {
            templateUrl: '/Content/Templates/home.html',
            controller: 'HomeController'
        });
        $routeProvider.when('/updates', {
            templateUrl: '/Content/Templates/updates.html',
            controller: 'UpdatesController'
        });
        $routeProvider.when('/items', {
            templateUrl: '/Content/Templates/items.html',
            controller: 'ItemsController'
        });
        $routeProvider.when('/item/:id', {
            templateUrl: '/Content/Templates/item.html',
            controller: 'ItemController'
        });
        $routeProvider.when('/login', {
            templateUrl: '/Content/Templates/login.html',
            controller: 'LoginController'
        });
        $routeProvider.when('/briefs', {
            templateUrl: '/Content/Templates/briefs.html',
            controller: 'BriefsController'
        });
        $routeProvider.when('/spells', {
            templateUrl: '/Content/Templates/spells.html',
            controller: 'SpellsController'
        } );
        $routeProvider.when( '/spell/:id', {
            templateUrl: '/Content/Templates/spell.html',
            controller: 'SpellController'
        } );
        $routeProvider.otherwise({ redirectTo: '/home' });
        $locationProvider.html5Mode(true);
    })
    .factory('itemsCache', function ($cacheFactory) {
        return $cacheFactory('itemsCache', { capacity: 2 });
    });