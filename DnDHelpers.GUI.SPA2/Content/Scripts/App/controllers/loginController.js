'use strict';

dndApp.controller('LoginController', function LoginController ($scope, authService, $location) {
    $scope.username = '';
    $scope.password = '';
    $scope.showError = false;
    $scope.submit = function () {
        authService.authenticate($scope.username, $scope.password, function () {
            $location.url('/home');
            return;
        },
        function () {
            $scope.showError = true;
        });
    };
});