(function() {
    'use strict';

    angular
        .module('Eduegate.ERP.Admin')
        .controller('TransactionStatusController', controller);

    controller.$inject = ['$scope'];

    function controller($scope) {

        var vm = this;

        vm.getJobStatus = function () {
            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "GET",
                contentType: "application/json;charset=utf-8",
                url: "Mutual/GetLookUpData?lookType=JobStatus",
                success: function (result) {
                    vm.JobStatuses = result;
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }


        vm.getJobOperationStatus = function () {
            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "GET",
                contentType: "application/json;charset=utf-8",
                url: "Mutual/GetLookUpData?lookType=JobOperationStatus",
                success: function (result) {
                    vm.JobOperationStatuses = result;
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }

        vm.getJobOperationStatusByJobStatus = function (transaction) {
            vm.transaction = transaction;

            var jobStatusId = transaction.JobStatus;

            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "GET",
                contentType: "application/json;charset=utf-8",
                url: "Mutual/GetLookUpData?lookType=JobOperationStatus&optionalId=" + jobStatusId,
                success: function (result) {
                    vm.JobOperationStatuses = result;
                    $('.preload-overlay', $(windowContainer)).hide();
                }
            });
        }


        vm.updateJobStatus = function(transaction) {
            $('.preload-overlay', $(windowContainer)).show();

            $.ajax({
                type: "GET",
                url: "JobEntry/UpdateJobStatus?jobHeadId=" + transaction.JobEntryHeadId + "&jobOperationStatusId=" + transaction.JobOperationStatus,
                data: transaction,
                success: function (result) {
                    console.log(result);

                    vm.transaction.JobStatus = result.JobStatusID;
                    vm.transaction.JobStatusName = result.StatusName;

                    $('.preload-overlay', $(windowContainer)).hide();

                }
            });
        }

    }

})();