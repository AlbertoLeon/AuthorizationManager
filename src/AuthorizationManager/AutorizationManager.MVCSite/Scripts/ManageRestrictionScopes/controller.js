(function () {

    var injectParams = ['$scope','config'];

    var managerRestrictionScopesCtrl = function ($scope, config) {
        $scope.defaultScopes = config.defaultScopes;
        $scope.customScopes = config.customScopes;
        $scope.currentScopes = config.restrictionScopes;
    };

    managerRestrictionScopesCtrl.$inject = injectParams;

    angular.module('manageRestrictionScopes').controller('managerRestrictionScopesCtrl', managerRestrictionScopesCtrl);

}());