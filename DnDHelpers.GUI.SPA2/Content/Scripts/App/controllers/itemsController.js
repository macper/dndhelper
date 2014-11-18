/// <reference path="../services/updatesService.js" />
'use strict';

dndApp.controller('ItemsController', function ItemsController ($scope, $timeout, $location, itemsService, itemsCache) {
    $scope.itemName = '';
    $scope.selectedItemType = null;
    $scope.itemTypes = itemsService.getItemTypes();
    $scope.items = null;
    $scope.item = null;
    $scope.selectItem = function (item) {
        $scope.item = item;
        itemsCache.put('itemName', $scope.itemName);
        itemsCache.put('itemType', $scope.selectedItemType);
        $location.url('/item/' + item.Id);
    };
    $scope.typeChanged = function () {
        $scope.items = itemsService.getItems($scope.itemName, $scope.selectedItemType.TypeName);
    };
    var currentTimeout = null;
    $scope.nameChanged = function () {
        if (currentTimeout != null) {
            $timeout.cancel(currentTimeout);
        };
        currentTimeout = $timeout(function () {
            getItems();
        }, 300)
    };
    $scope.init = function () {
        
        $scope.itemName = itemsCache.get('itemName');
        $scope.selectedItemType = itemsCache.get('itemType');
        getItems();
    };

    $scope.add = function() {
        $location.url('/item/00000000-0000-0000-0000-000000000000');
    };

    var getItems = function () {
        if ($scope.selectedItemType)
            $scope.items = itemsService.getItems($scope.itemName, $scope.selectedItemType.TypeName);
        else
            $scope.items = itemsService.getItems($scope.itemName, null);
    };
});