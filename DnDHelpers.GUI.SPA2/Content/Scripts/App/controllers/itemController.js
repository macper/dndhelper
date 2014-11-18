/// <reference path="../services/itemsService.js" />
'use strict';

dndApp.controller('ItemController', function ItemsController($scope, itemsService, dictionaryService, $routeParams) {
    $scope.viewMode = true;
    var itemInfo = itemsService.getItem($routeParams.id);
    itemInfo.$promise.then(function (info) {
        if ($routeParams.id == '00000000-0000-0000-0000-000000000000') {
            $scope.viewMode = false;
        }
        $scope.itemTypes = info.ItemTypes;
        $scope.itemPrototypes = info.ItemPrototypes;
        $scope.item = info.Item;
        $.each($scope.itemTypes, function (index, value) {
            if (value.TypeName == $scope.item.Type.TypeName) {
                $scope.item.Type = $scope.itemTypes[index];
            }
        });
        $.each($scope.itemPrototypes, function (index, value) {
            if (value.Name == $scope.item.Prototype.Name) {
                $scope.item.Prototype = $scope.itemPrototypes[index];
            } 
        });
    });
    $scope.edit = function () {
        $scope.viewMode = false;
    };
    $scope.selectedBonus = null;
    $scope.addBonus = function () {
        setUpBonuses();
    };
    $scope.deleteBonus = function () {
        if ($scope.selectedBonus == null)
            return;
        $.each($scope.item.Bonuses, function (index, value) {
            if (value.Prototype == $scope.selectedBonus.Prototype) {
                $scope.item.Bonuses.splice(index, 1);
                return false;
            }
        });
        $('#bonusModal').modal('hide');
    };
    $scope.editBonus = function (bonus) {
        $scope.selectedBonus = bonus;
        setUpBonuses();
    };
    var setUpBonuses = function () {
        $scope.bonusTypes = itemsService.getBonusTypes();
        $scope.bonusTypes.$promise.then(function (bonuses) {
            $.each(bonuses, function (index, bonus) {
                bonus.template = '/Content/Templates/Bonus/' + bonus.Type + '.html';
                if ($scope.selectedBonus != null && bonus.Type == $scope.selectedBonus.Type) {
                    var prototype = $scope.selectedBonus.Prototype;
                    $scope.selectedBonus = bonus;
                    $scope.selectedBonus.Prototype = prototype;
                    $scope.bonusChanged();
                }
            });
            $('#bonusModal').modal();
        });
    };
    $scope.bonusChanged = function () {
        if ($scope.selectedBonus.AdditionalRequest == 1 && $scope.ACTypes == null) {
            itemsService.getACTypes().$promise.then(function(types) {
                $scope.ACTypes = [];
                $.each(types, function (index, value) {
                    $scope.ACTypes.push(value.Name);
                });
            });
        };
        if ($scope.selectedBonus.AdditionalRequest == 2 && $scope.effects == null) {
            dictionaryService.getEffects().$promise.then(function (effects) {
                $scope.effects = [];
                $.each(effects, function (index, value) {
                    $scope.effects.push(value.Name);
                });
            });
        };
        if ($scope.selectedBonus.AdditionalRequest == 3 && $scope.atutes == null) {
            dictionaryService.getAtutes().$promise.then(function (atutes) {
                $scope.atutes = [];
                $.each(atutes, function (index, value) {
                    $scope.atutes.push(value.Name);
                });
            });
        };
        if ($scope.selectedBonus.AdditionalRequest == 4 && $scope.damages == null) {
            dictionaryService.getDamages().$promise.then(function (damages) {
                $scope.damages = [];
                $.each(damages, function (index, value) {
                    $scope.damages.push(value.Value);
                });
            });
        };
        if ($scope.selectedBonus.AdditionalRequest == 5 && $scope.mainSkills == null) {
            dictionaryService.getMainSkills().$promise.then(function (mainSkills) {
                $scope.mainSkills = mainSkills;
            });
        };
        if ($scope.selectedBonus.AdditionalRequest == 6 && $scope.skills == null) {
            dictionaryService.getSkills().$promise.then(function (skills) {
                $scope.skills = [];
                $.each(skills, function (index, value) {
                    $scope.skills.push(value.Name);
                });
            });
        };
    };
    $scope.effects = null;
    $scope.atutes = null;
    $scope.damages = null;
    $scope.mainSkills = null;
    $scope.skills = null;
    $scope.saveBonus = function () {
        $scope.selectedBonus.Prototype.Source = $scope.item.Name;
        var alreadyExists = false;
        $.each($scope.item.Bonuses, function (index, value) {
            if (value.Prototype == $scope.selectedBonus.Prototype) {
                alreadyExists = true;
                return;
            }
        });
        if (!alreadyExists)
            $scope.item.Bonuses.push($scope.selectedBonus);
        $('#bonusModal').modal('hide');
    };
    $scope.ACTypes = null;
    $scope.submit = function () {
        itemsService.saveItem($scope.item, 
            function onSuccess() {
                showInfo("Edycja przedmiotu zakończona pomyślnie");
            },
            function onFail(response) {
                showError("Wystąpił błąd podczas edycji przedmiotu: " + response);
            });
    };

    $scope.message = null;
    $scope.alertClass = null;
    $scope.showMessage = false;

    var showInfo = function (message) {
        $scope.showMessage = true;
        $scope.alertClass = "alert alert-info";
        $scope.message = message;
    };

    var showError = function (message) {
        $scope.showMessage = true;
        $scope.alertClass = "alert alert-danger";
        $scope.message = message;
    };

    $scope.parseError = false;
    $scope.parseSuccess = false;
    $scope.damageToParse = "1K8+1[Fizyczne]";
    $scope.parseDamage = function(damageToParse) {
        var damage = dictionaryService.parseDamage(damageToParse);
        damage.$promise.then(function(data) {
            $scope.parseError = false;
            $scope.selectedBonus.Prototype.Amount = data;
            $scope.parseSuccess = true;
        }, function() {
            $scope.parseError = true;
            $scope.parseSuccess = false;
        });

    };
    $scope.delete = function() {
        if ($scope.item == null)
            return;
        itemsService.deleteItem({ Id: $scope.item.Id },
            function onSuccess() {
                showInfo("Przedmiot usunięty poprawnie");
            },
            function onError(message) {
                showError("Nie udało się usunąć przedmiotu - " + message);
            });
    };
    $scope.copy = function () {
        if ($scope.item == null)
            return;

        $scope.item = itemsService.copyItem($scope.item);
        $scope.item.$promise.then(function () {
            $.each($scope.itemTypes, function (index, value) {
                if (value.TypeName == $scope.item.Type.TypeName) {
                    $scope.item.Type = $scope.itemTypes[index];
                }
            });
            $.each($scope.itemPrototypes, function (index, value) {
                if (value.Name == $scope.item.Prototype.Name) {
                    $scope.item.Prototype = $scope.itemPrototypes[index];
                }
            });
            showInfo('Przedmiot skopiowany pomyślnie');
        });
    }
});