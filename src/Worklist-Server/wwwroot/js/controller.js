(function () {
    'use strict';

    angular
        .module('WorklistApp')
        .controller('WorklistController', controller1)
        .controller('ModalInstanceCtrl', modalInstanceCtrl);

    controller1.$inject = ['$scope', '$http', '$uibModal', '$log', '$document'];

    function modalInstanceCtrl($uibModalInstance, selectedItem) {
        var $ctrl = this;
        $ctrl.selectedWorklistItem = selectedItem;

        $ctrl.ok = function () {
            $uibModalInstance.close('ok');
        };

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function controller1($scope, $http, $uibModal, $log, $document) {

        var $ctrl = this;
        $ctrl.open = function (size, parentSelector, worklistitem) {
            console.log(worklistitem);
            var parentElem = parentSelector ?
                angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: size,
                appendTo: parentElem,
                resolve: {
                    selectedItem: function() {
                        return worklistitem;
                    }
                }
            });
            modalInstance.result.then(function (item) {
                console.log('Modal dismissed with ' + item + ' at: ' + new Date());
            }, function (item) {
                console.log('Modal dismissed with ' + item + ' at: ' + new Date());
            });
        };

        $scope.allWorklistItemsUrl = "/api/worklistitems";
        var promise = $http.get($scope.allWorklistItemsUrl);
        promise.then(function(res) {
            return $scope.items = res.data;
        }, function(err) {
            $scope.errorMsg = 'could not load worklistitems';
            console.log("err: ");
            return console.log(err);
        });

        activate();

        function activate() { }
    }
})();
