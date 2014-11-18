dndApp.factory('authService', function (Base64, $cookieStore, $http, $resource) {
    // initialize to whatever is in the cookie, if anything

    var _setCredentials = function (username, password) {
        var encoded = Base64.encode(username + ':' + password);
        $http.defaults.headers.common.Authorization = 'Basic ' + encoded;
        $cookieStore.put('authdata', encoded);
        $cookieStore.put('identity', username);
    };

    var _clearCredentials = function () {
        document.execCommand("ClearAuthenticationCache");
        $cookieStore.remove('authdata');
        $http.defaults.headers.common.Authorization = 'Basic ';
    };

    return {
        setCredentials: _setCredentials,
        clearCredentials: _clearCredentials,
        isAuthenticated: function () {
            if ($cookieStore.get('authdata')) {
                $http.defaults.headers.common['Authorization'] = 'Basic ' + $cookieStore.get('authdata');
                return true;
            }
            return false;
        },
        authenticate: function (username, password, onSuccess, onFail) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + Base64.encode(username + ':' + password);
            $http({ method: 'GET', url: '/auth/basic' }).
              success(function (data, status, headers, config) {
                  if (data.UserName == username) {
                      _setCredentials(username, password);
                      onSuccess();
                      return;
                  }
              }).
              error(function (data, status, headers, config) {
                  _clearCredentials();
                  onFail();
              });
        },
        getCurrentUser: function () {
            return $cookieStore.get('identity');
        },
        logout: function () {
            _clearCredentials();
        }
    };
});