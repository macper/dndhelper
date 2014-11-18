'use strict';

dndApp.factory('updatesService', function ($http, $q, $resource) {
    return {
        getSyncInfo: function () {
            return $resource('/api/syncInfo').get();
        },
        sync: function (onSuccess, onFail) {
            $http({ url: '/api/syncRepositories', method: 'POST' }).
            success(function (data) {
                onSuccess(data);
            }).
            error(function (data, status, headers, config) {
                onFail(status);
            });
        }
    };
});