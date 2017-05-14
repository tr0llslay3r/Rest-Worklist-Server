(function () {
    'use strict';

    angular
        .module('WorklistApp')
        .controller('ConfigurationController', configurationController);

    configurationController.$inject = ['$scope', '$http'];
    

    function configurationController($scope, $http) {
        
        var tagmapUrl = "/api/dicomtagmap";
        var promise = $http.get(tagmapUrl);
        promise.then(function(res) {
            return $scope.tagmap = res.data;
        }, function(err) {
            $scope.errorMsg = 'could not load worklistitems';
            console.log("err: ");
            return console.log(err);
        });
        
        $scope.saveConfig = function () {
            console.log($scope.tagmap);
            $http.post(tagmapUrl, $scope.tagmap).then(function (res) {
                
                console.log("config saved");
            }, function(err) {
                console.log("error saving config");
            });
        }
    }
})();
