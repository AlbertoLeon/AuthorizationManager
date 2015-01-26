(function () {

    var injectParams = ['$q', 'dataService'];

    var customScope = function ($q, dataService) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$asyncValidators.unique = function (modelValue, viewValue) {
                    var deferred = $q.defer(),
                        currentValue = modelValue || viewValue,
                        key = attrs.wcUniqueKey,

                        dataService.addScope(attrs.clientId, ngModel.name)
                        .then(function (result) {
                            deferred.resolve();
                        });
                        return deferred.promise;

                };
            }
        };
    };

    wcUniqueDirective.$inject = injectParams;

    angular.module('manageRestrictionScopes').directive('customScope', customScope);

}());