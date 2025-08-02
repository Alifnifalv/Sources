app.controller('StudentFeesController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', "$timeout", function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, $timeout) {
    console.log('StudentFeesController loaded.');

    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.FeeTypes = [];

    $rootScope.ShowPreLoader = true;
    $rootScope.ShowLoader = true;

    $scope.StudentID = $stateParams.studentID;

    $scope.init = function () {

        $scope.FillInvoice($scope.StudentID);
    };

    $scope.toggleGrid = function (event) {

        toggleHeader = $(event.currentTarget).closest(".toggleContainer").find(".toggleHeader");
        toggleContent = $(event.currentTarget).closest(".toggleContainer").find(".toggleContent");
        toggleHeader.toggleClass("active");
        if (toggleHeader.hasClass('active')) {
            toggleContent.slideDown("fast");
        }
        else {
            toggleContent.slideUp("fast");
        }
    }
    $scope.GetTotalFeePayAmount = function (data) {

        if (typeof (data) === 'undefined') {
            return 0;
        }
        var sum = 0;
        $.each(data, function (index, objModel) {
            $.each(objModel.FeeTypes, function (index, objModelinner) {

                sum = sum + objModelinner.Amount;

            });
        });
        return sum;
    }

    $scope.fillInvoice = function (studentID) {
        $scope.FeeTypesNew = [];

        $http({
            method: 'GET',
            url: dataService + '/FillFeeDue?studentID=' + $scope.StudentID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.FeeTypesNew = result;

            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        });
    }

    $scope.FillInvoice = function (studentID) {
        $scope.FeeTypes = [];

        $http({
            method: 'GET',
            url: rootUrl.ParentUrl + '/Home/FillFeeDue?studentId=' + $scope.StudentID,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            if (!result.IsError) {
                $scope.FeeTypes = result.Response;
            }

            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
            $rootScope.ShowPreLoader = false;
        });
    }

    // $scope.init();
}]);