'use strict';

dndApp.factory('briefsService', function ($resource, $filter) {
    var counter = 0;
    return {
        getBriefs: function (briefs, enemies, callback, errorCallback) {
            var chars = [];
            var enems = [];

            angular.forEach(briefs, function (brief) {
                chars.push({ Name: brief.Name, ChangeId : brief.ChangeId });
            });
            angular.forEach(enemies, function (value) {
                enems.push({ Name: value.Name, ChangeId: value.ChangeId });
            });

            var briefs = $resource('/briefInfo').save({ PartyMembers: chars, EnemyMembers: enems }, function () {
                callback(briefs);
            }, function (response) {
                if (response.status != 200) {
                    errorCallback();
                }
            });
            return briefs;
        }
    };
});