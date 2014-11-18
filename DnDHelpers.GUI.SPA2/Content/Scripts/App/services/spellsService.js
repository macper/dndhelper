'use strict';

dndApp.factory('spellsService', function ($resource) {
    return {
        getSpellTypes: function () {
            return $resource('/api/spellTypes').query();
        },
        getSpells: function (name, type, level) {
            return $resource('/api/spells?Name=:name&Type=:type&level=:level', { name: '@name', type: '@type', level: '@level' }).query({ name: name, type: type, level: level });
        },
        getSpell: function (id) {
            return $resource('/api/spell/:id', { id: '@id' }).get({ id: id });
        },
        getSchools: function () {
            return $resource( '/api/spellSchools' ).query();
        },
        getRanges: function () {
            return $resource( '/api/spellRanges' ).query();
        },
        saveSpell: function ( spell, onSuccess, onFail ) {
            $resource( '/api/spell' ).save( spell, function () {
                onSuccess();
            }, function ( data ) {
                onFail( data );
            } );
        },
        deleteSpell: function ( spell, onSuccess, onFail ) {
            $resource( '/api/spell' ).delete( spell, function () {
                onSuccess();
            },
            function ( data ) {
                onFail( data );
            } );
        }
    };
});