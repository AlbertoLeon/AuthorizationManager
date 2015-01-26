(function () {

    var injectParams = ['config','$http'];

    var dataService = function (config,$http) {
        return {
            addScope : function(clientId, name) {
                return $http.post(config.addScopeUrl, { clientId: clientId, name: name });
            },
            removeScope : function (clientId, name) {
                return $http.post(config.removeScopeUrl, { clientId: clientId, name: name });
            },
        };
    };

    dataService.$inject = injectParams;

    angular.module('manageRestrictionScopes').factory('dataService', dataService);

}());