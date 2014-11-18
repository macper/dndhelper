dndApp.filter('hpFilter', function () {
    return function (hpLeft) {
        if (hpLeft > 80)
            return "green";
        if (hpLeft > 60)
            return "orange";
        if (hpLeft > 30)
            return "lightRed";
        if (hpLeft > 0)
            return "red";
        return "black";
    };
});