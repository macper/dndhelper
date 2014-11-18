'use strict';

dndApp.factory('dictionaryService', function ($resource) {
    return {
        getEffects: function () {
            return $resource('/api/dictionary/effects').query();
        },
        getAtutes: function () {
            return $resource('/api/dictionary/atutes').query();
        },
        getDamages: function () {
            return $resource('/api/dictionary/damageTypes').query();
        },
        getMainSkills: function () {
            return $resource('/api/dictionary/mainSkills').query();
        },
        getSkills: function () {
            return $resource('/api/dictionary/skills').query();
        },
        parseDamage: function (damage) {
            return $resource('/api/dictionary/parsedDamage?Value=:value', { value: '@value' }).get({ value: damage.replace('+', '*') });
        }
    }
});
