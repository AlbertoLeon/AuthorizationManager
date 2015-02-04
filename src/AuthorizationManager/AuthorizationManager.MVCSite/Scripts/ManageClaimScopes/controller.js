(function () {

    angular.module('manageClaimScopes')
        .controller('managerClaimScopesCtrl', ['$scope', 'config','$http',
            function ($scope, config,$http) {
        $scope.defaultClaims = buildObjects(config.defaultClaims);

        $scope.add = function (claimItem) {
            $http.post(config.scopesUrl,
                JSON.stringify({ claimName: claimItem.name })).then(
                function (success) {
                    claimItem.canBeAdded = false;
                    claimItem.canBeRemoved = true;
                },
                function (error) {
                    alert(error);
                    claimItem.canBeAdded = true;
                    claimItem.canBeRemoved = false;
                }
            );
        }
        $scope.remove = function (claimItem) {
            $http.delete(config.scopesUrl + "/" + claimItem.id).then(
                function (success) {
                    claimItem.canBeAdded = true;
                    claimItem.canBeRemoved = false;
                },
                function (error) {
                    alert(error);
                    claimItem.canBeAdded = false;
                    claimItem.canBeRemoved = true;
                }
            );
        }

        function buildObjects(stringlist) {
            var objs = [];

            for (var i = 0; i < stringlist.length; i++) {
                var string = stringlist[i];
                var isInList = IsInList(string, config.currentClaims);
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