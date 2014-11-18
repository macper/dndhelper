'use strict';

dndApp.factory('itemsService', function ($resource) {
    return {
        getItemTypes: function () {
            return $resource('/api/itemTypes').query();
        },
        getItems: function (name, type) {
            return $resource('/api/items?Name=:name&Type=:type', { name: '@name', type: '@type' }).query({ name: name, type: type });
        },
        getItem: function (id) {
            return $resource('/api/item/:id', { id: '@id' }).get({ id: id });
        },
        getItemPrototypes: function () {
            return $resource('/api/itemPrototypes').query();
        },
        getBonusTypes: function () {
            return $resource('/api/bonusPrototypes').query();
        },
        getACTypes: function () {
            return $resource('/api/ACTypes').query();
        },
        saveItem: function (item, onSuccess, onFail) {
            $resource('/api/item').save(item, function () {
                onSuccess();
            }, function (response) {
                if (response.status == 403)
                    onFail('Brak uprawnień');
                else
                    onFail(response.ResponseStatus.Message);
            });
        },
        deleteItem: function(item, onSuccess, onFail) {
            $resource('/api/item').delete(item, function() {
                    onSuccess();
                },
                function (response) {
                    if (response.status == 403)
                        onFail('Brak uprawnień');
                    else
                        onFail(response.ResponseStatus.Message);
                });
        },
        copyItem: function (item) {
            return $resource('/api/itemCopy/' + item.Id).get();
        }
    };
});