(function () {
    'use strict';

    angular
        .module('WorklistApp')
        .controller('WorklistController', controller1);

    controller1.$inject = ['$scope', '$http'];

    function controller1($scope, $http) {
        var promise = $http.get("/api/worklistitems");
        promise.then(function(res) {
            return $scope.items = res.data;
        }, function(err) {
            $scope.errorMsg = 'could not load services';
            console.log("err: ");
            return console.log(err);
        });

        activate();

        function activate() { }
    }
})();
