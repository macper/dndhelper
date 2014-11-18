/// <reference path="../services/updatesService.js" />
'use strict';

dndApp.controller('SpellsController', function SpellsController ($scope, $timeout, $location, spellsService, itemsCache) {
    $scope.name = '';
    $scope.type = null;
    $scope.level = null;
    $scope.spellTypes = spellsService.getSpellTypes();
    $scope.levels = ['(wszystkie)', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    $scope.spells = null;
    $scope.spell = null;

    $scope.selectSpell = function (spell) {
        $scope.spell = spell;
        $location.url('/spell/' + spell.Id);
    };

    $scope.typeChanged = function () {
        getSpells();
    };

    var currentTimeout = null;
    $scope.nameChanged = function () {
        if (currentTimeout != null) {
            $timeout.cancel(currentTimeout);
        };
        currentTimeout = $timeout(function () {
            getSpells();
        }, 300)
    };

    $scope.init = function () {
        getSpells();
    };

    $scope.add = function() {
        $location.url('/spell/00000000-0000-0000-0000-000000000000');
    };

    var getSpells = function () {
        var type = $scope.type == null ? null : $scope.type.Value;
        $scope.spells = spellsService.getSpells($scope.name, type, $scope.level);
    };

    $scope.getSpells = getSpells;
});