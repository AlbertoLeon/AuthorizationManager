(function () {

    var injectParams = [];

    var authscope = function () {
        return {
            restrict: 'A',
            require: '^ngModel',
            replace: true,
            scope: {
                canBeAdded: false,
                canBeRemoved: false,
                name: '@',
                clientId: '='
            },
            templateUrl: 'Templates/AuthScope',
            controller: ['$scope', '$http','config', function ($scope, $http, config) {
                $scope.add = function() {
                    $http.post(config.addScopeUrl, { clientId: $scope.clientId, scopeName: $scope.name },
                        function(success) {
                            $scope.canBeAdded = false;
                            $scope.canBeRemoved = true;
                        },
                        function (error) {
                            alert(error);
                            $scope.canBeAdded = true;
                            $scope.canBeRemoved = false;
                        }
                    );
                }
                $scope.remove = function() {
                    $http.delete(config.removeScopeUrl, { clientId: $scope.clientId, scopeName: $scope.name },
                        function (success) {
                            $scope.canBeAdded = true;
                            $scope.canBeRemoved = false;
                        },
                        function (error) {
                            alert(error);
                            $scope.canBeAdded = false;
                            $scope.canBeRemoved = true;
                        }
                    );
                }
            }],
            link: function (scope, element, attrs) {
                // get weather details
            }
        }
    };

    authscope.$inject = injectParams;

    angular.module('manageRestrictionScopes').directive('customScope', authscope);

}());