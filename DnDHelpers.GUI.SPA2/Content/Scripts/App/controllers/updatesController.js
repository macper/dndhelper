/// <reference path="../services/updatesService.js" />
'use strict';

dndApp.controller('UpdatesController',
    function UpdatesController($scope, updatesService) {
        $scope.hideAll = false;
        var messageBox = function (msg, type) {
            $scope.messageVisible = true;
            $scope.messageText = msg;
            $scope.messageType = type;
        };
        $scope.messageText = '';
        $scope.messageVisible = false;
        $scope.messageType = 'alert alert-info';


        var syncInfo = updatesService.getSyncInfo();

        syncInfo.$promise.catch(function (response) {
            if (response.status == 403) {
                messageBox('Brak uprawnień', 'alert alert-danger');
                $scope.hideAll = true;
            }
        });

        $scope.syncInfo = syncInfo;

        $scope.Sync = function () {
            updatesService.sync(function (data) {
                if (data.Success == true) {
                    $scope.syncInfo = updatesService.getSyncInfo();
                    messageBox(data.Message, 'alert alert-info');
                    return;
                }
                messageBox(data.Message, 'alert alert-danger');
            },
            function (status) {
                messageBox('Nie udało się zaktualizować, sprawdź logi', 'alert alert-danger');
            });
        };
    });
