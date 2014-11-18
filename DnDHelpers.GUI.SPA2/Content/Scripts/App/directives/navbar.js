'use strict';

dndApp.directive('navBar', function () {
    return {
        restrict: 'C',
        templateUrl: '/Content/Templates/directives/navbar.html',
        controller: function($scope, authService, $location) {
            $scope.currentUser = authService.getCurrentUser();
            $scope.logout = function () {
                authService.logout();
                $location.url('/login');
            }
        },
        link: function (scope, element, attrs, controller) {
            scope.title = attrs.title;
            $(element[0]).find('button').on('click', function () {
                history.back();
                scope.$apply();
            });
        }    
    };
});