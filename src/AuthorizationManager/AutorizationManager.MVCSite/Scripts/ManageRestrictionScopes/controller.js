(function () {

    var injectParams = ['config'];

    var controller = function (config) {
        $scope.defaultScopes = config.defaultScopes;
        $scope.customScopes = config.customScopes;
        $scope.currentScopes = config.restrictionScopes;
    };

    controller.$inject = injectParams;

    angular.module('manageRestrictionScopes').controller('manageRestrictionScopesCtrl', controller);

}());