/// <reference path="../services/updatesService.js" />
'use strict';

dndApp.controller('BriefsController', function BriefsController($scope, $timeout, briefsService) {
    $scope.ChangeId = -1;
    $scope.briefs = [];
    $scope.enemies = [];
    $scope.briefMode = true;
    $scope.showWarning = false;
    $scope.showError = false;
    var queryBriefs = function () {
        $timeout(function() {
            briefsService.getBriefs($scope.briefs, $scope.enemies, function (response) {
                $scope.showError = false;
                if (response.Characters.length > 0 || response.Enemies.length > 0) {
                    angular.forEach(response.Characters, function (value, index) {
                        if (value.TypeOfChange == "Insert") {
                            value.hpLeftPercent = function () {
                                return (this.CurrentHP / this.MaxHP) * 100;
                            };
                            $scope.briefs.push(value);
                        }
                        else if (value.TypeOfChange == "Update") {
                            var toUpdate = getBriefById(value.Id);
                            if (toUpdate) {
                                toUpdate.CurrentHP = value.CurrentHP;
                                toUpdate.MaxHP = value.MaxHP;
                                toUpdate.CurrentAC = value.CurrentAC;
                                toUpdate.Effects = value.Effects;
                                toUpdate.ChangeId = value.ChangeId;
                                $('#' + toUpdate.Id).addClass('lightYellow');
                                $timeout(function() {
                                    $('#' + toUpdate.Id).removeClass('lightYellow');
                                }, 500);
                            }
                        }
                        else {
                            angular.forEach($scope.briefs, function (val, index) {
                                if (val.Id == value.Id) {
                                    $scope.enemies.splice(index, 1);
                                }
                            });
                        }
                        if (value.ChangeId > $scope.ChangeId)
                            $scope.ChangeId = value.ChangeId;
                    });
                    angular.forEach(response.Enemies, function (value, index) {
                        if (value.TypeOfChange == "Insert") {
                            $scope.enemies.push(value);
                        }
                        else if (value.TypeOfChange == "Update") {
                            var enemy = getEnemyByName(value.Name);
                            enemy.Health = value.Health;
                            enemy.ChangeId = value.ChangeId;
                            $("[id='" + enemy.Name + "']").addClass('lightYellow');
                            $timeout(function () {
                                $("[id='" + enemy.Name + "']").removeClass('lightYellow');
                            }, 500);
                        } else {
                            angular.forEach($scope.enemies, function (val, index) {
                                if (val.Name == value.Name) {
                                    $scope.enemies.splice(index, 1);
                                }
                            });
                        }
                        if (value.ChangeId > $scope.ChangeId)
                            $scope.ChangeId = value.ChangeId;
                    });
                }
                queryBriefs();
                if ($scope.briefs.length == 0) {
                    $scope.showWarning = true;
                }
                else {
                    $scope.showWarning = false;
                }
            }, function() {
                $scope.showError = true;
            });
        }, 3000);
    };
    queryBriefs();
    
    var getBriefById = function (id) {
        var ret = null;
        angular.forEach($scope.briefs, function (value, index) {
            if (value.Id == id) {
                ret = value;
            }
        });
        return ret;
    };

    var getEnemyByName = function(name) {
        var ret = null;
        angular.forEach($scope.enemies, function(value, index) {
            if (value.Name == name) {
                ret = value;
            }
        });
        return ret;
    };
    
    $scope.getHpClass = function (brief) {
        var hpLeft = (brief.CurrentHP / brief.MaxHP) * 100;
            if (hpLeft > 70)
                return "green";
            if (hpLeft > 50)
                return "orange";
            if (hpLeft > 30)
                return "lightRed";
            if (hpLeft > 0)
                return "red";
            return "black";
    };

    $scope.getHpClass2 = function (enemy) {
        if (enemy.Health == "Pełnia zdrowia" || enemy.Health == "Lekkie obrażenia")
            return "green";
        if (enemy.Health == "Średnie obrażenia")
            return "orange";
        if (enemy.Health == "Poważne obrażenia")
            return "lightRed";
        if (enemy.Health == "Na skraju śmierci")
            return "red";
        return "black";
    };

});