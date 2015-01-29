(function () {

    angular.module('manageRestrictionScopes')
        .controller('managerRestrictionScopesCtrl', ['$scope', 'config','$http',
            function ($scope, config,$http) {
        $scope.defaultScopes = buildObjects(config.defaultScopes);
        $scope.customScopes = buildObjects(config.customScopes);

        $scope.add = function (scopeItem) {
            $http.post(config.clientsUrl + "/" + config.clientId,
                JSON.stringify({ scopeName: scopeItem.name })).then(
                function (success) {
                    scopeItem.canBeAdded = false;
                    scopeItem.canBeRemoved = true;
                },
                function (error) {
                    alert(error);
                    scopeItem.canBeAdded = true;
                    scopeItem.canBeRemoved = false;
                }
            );
        }
        $scope.remove = function (scopeItem) {
            $http.delete(config.clientsUrl + "/" + config.clientId + "/" + scopeItem.name).then(
                function (success) {
                    scopeItem.canBeAdded = true;
                    scopeItem.canBeRemoved = false;
                },
                function (error) {
                    alert(error);
                    scopeItem.canBeAdded = false;
                    scopeItem.canBeRemoved = true;
                }
            );
        }

        function buildObjects(stringlist) {
            var objs = [];

            for (var i = 0; i < stringlist.length; i++) {
                var string = stringlist[i];
                var isInList = IsInList(string, config.currentScopes);
                objs.push(
                    {
                        name: string,
                        canBeAdded: !isInList,
                        canBeRemoved: isInList
                    }
                 );
            }

            return objs;
        }

        function IsInList(string, stringlist) {
            if (stringlist.length > 0) {
                return stringlist.indexOf(string) > -1;
            } else {
                return false;
            }
        }
    }]);

}());