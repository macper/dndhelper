/// <reference path="../services/spellsService.js" />
'use strict';

dndApp.controller( 'SpellController', function SpellController( $scope, spellsService, $routeParams ) {
    $scope.viewMode = true;

    var spellInfo = spellsService.getSpell( $routeParams.id );

    spellInfo.$promise.then( function ( info ) {
        if ( $routeParams.id == '00000000-0000-0000-0000-000000000000' ) {
            $scope.viewMode = false;
        }
        $scope.schools = info.DictSchools;
        $scope.types = info.DictTypes;
        $scope.ranges = info.DictRanges;
        $scope.spell = info.SpellInstance;
        $scope.levels = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

        $scope.spell.School = info.DictSchools.filter( function ( el ) { return el.Name == $scope.spell.School.Name; } )[0];
        $scope.spell.Range = info.DictRanges.filter( function ( el ) { return el.Name == $scope.spell.Range.Name; } )[0];
        $scope.spell.Level = $scope.levels.filter( function ( el ) { return el == $scope.spell.Level; } )[0];
    } );

    
    $scope.selectTypeLeftList = null;
    $scope.selectTypeRightList = null;

    $scope.selectType = function () {
        if ( $scope.selectTypeLeftList == null ) {
            return;
        }
        $scope.spell.Type.push( $scope.selectTypeLeftList );
    };

    $scope.unselectType = function () {
        if ( $scope.selectTypeRightList == null ) {
            return;
        }

        $scope.spell.Type.splice( $scope.spell.Type.indexOf( $scope.selectTypeRightList ), 1 );
    }

    $scope.edit = function () {
        $scope.viewMode = false;
    };

    $scope.submit = function () {
        spellsService.saveSpell( $scope.spell,
            function onSuccess() {
                showInfo( "Edycja czaru zakończona pomyślnie" );
            },
            function onFail( response ) {
                showError( "Wystąpił błąd podczas edycji czaru: " + response.data.ResponseStatus.Message );
            } );
    };

    $scope.message = null;
    $scope.alertClass = null;
    $scope.showMessage = false;

    var showInfo = function ( message ) {
        $scope.showMessage = true;
        $scope.alertClass = "alert alert-info";
        $scope.message = message;
    };

    var showError = function ( message ) {
        $scope.showMessage = true;
        $scope.alertClass = "alert alert-danger";
        $scope.message = message;
    };

    $scope.delete = function () {
        if ( $scope.spell == null )
            return;
        spellsService.deleteSpell( { Id: $scope.spell.Id },
            function onSuccess() {
                showInfo( "Czar usunięty poprawnie" );
            },
            function onError( message ) {
                showError( "Nie udało się usunąć czaru - " + message.data.ResponseStatus.Message );
            } );
    };

} );