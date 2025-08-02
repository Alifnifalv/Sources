app.controller('StudentPickupRequestController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', "$q", function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout, $q) {
    console.log('Student pickup request controller loaded.');

    $scope.Students = [];
    $scope.StudentPickedBy = [];

    $scope.ShowPreLoader = true;

    $scope.init = function (model) {

        $scope.StudentPickupRequest = model;

        showOverlay();

        $q.all([
            GetStudentsList(),
            GetPickedByList(),
            GetParentDetails(),
        ]).then(function () {
            hideOverlay();
            $scope.ShowPreLoader = false;
        });

    };

    function GetStudentsList() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Home/GetStudentsSiblings?parentId=" + 0,
                success: function (result) {
                    $timeout(function () {
                        if (!result.IsError) {
                            result.Response.forEach(x => {
                                $scope.Students.push({
                                    "Key": x.StudentIID,
                                    "Value": x.AdmissionNumber + " - " + x.FirstName + " " + (x.MiddleName != null ? (x.MiddleName + " ") : "") + x.LastName,
                                });
                            });
                        }
                        resolve();
                    }, 1000);
                }
            });
        });
    };

    function GetPickedByList() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Home/GetDynamicLookUpData?lookType=StudentPickedBy&defaultBlank=false",
                success: function (result) {

                    $timeout(function () {
                        $scope.StudentPickedBy = result;
                        resolve();
                    }, 1000);
                }
            });
        });
    };

    function GetParentDetails() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: "GET",
                data: { studentId: 0 },
                url: utility.myHost + "Home/GetGuardianDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    $timeout(function () {
                        if (!result.IsError && result !== null) {
                            $scope.$apply(function () {
                                $scope.ParentDetails = result.Response;
                            });
                        }
                        resolve();
                    }, 1000);
                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        });
    };

    $scope.PickedByChanges = function (model) {

        $scope.StudentPickupRequest.PickedByID = model.PickedByVM.Key;

        if (model.PickedByVM.Value == "Father") {
            model.FirstName = $scope.ParentDetails.FatherFirstName;
            model.MiddleName = $scope.ParentDetails.FatherMiddleName;
            model.LastName = $scope.ParentDetails.FatherLastName;
        }
        else if (model.PickedByVM.Value == "Mother") {
            model.FirstName = $scope.ParentDetails.MotherFirstName;
            model.MiddleName = $scope.ParentDetails.MotherMiddleName;
            model.LastName = $scope.ParentDetails.MotherLastName;
        }
        else if (model.PickedByVM.Value == "Guardian") {
            model.FirstName = $scope.ParentDetails.GuardianFirstName;
            model.MiddleName = $scope.ParentDetails.GuardianMiddleName;
            model.LastName = $scope.ParentDetails.GuardianLastName;
        }
        else {
            model.FirstName = null;
            model.MiddleName = null;
            model.LastName = null;
        }
    };

    $scope.SubmitStudentPickupRequestClick = function () {

        if (!$("#FirstName").val()) {
            $("#FirstName").focus();
            $(this).prop("disabled", false);
            return false;
        }
        else if (!$("#FromTimeString").val()) {
            $("#FromTimeString").focus();
            $(this).prop("disabled", false);
            return false;
        }
        else if (!$("#Student").val() || $("#Student").val() == '?') {
            $("#Student").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Select any student!");
            return false;
        }
        else if (!$("#PickedByVM").val() || $("#PickedByVM").val() == '?') {
            $("#PickedByVM").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Picked by field is required!");
            return false;
        }
        else {
            $("#SubmitPickupRequestBtn").html("Submitting...");
            $("#SubmitPickupRequestBtn").prop("disabled", true);

            var data = $scope.StudentPickupRequest;

            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                url: utility.myHost + "Home/SubmitStudentPickupRequest",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.IsError == true) {
                        callToasterPlugin('error', result.Response);
                        $("#SubmitPickupRequestBtn").prop("disabled", false);
                        $("#SubmitPickupRequestBtn").html("Submit");
                        return false;
                    }
                    else {
                        window.location.replace(utility.myHost + "Home/StudentPickupRequestList");
                    }
                }
            });

        }
    };

    function showOverlay() {
        $("#StudentPickupRequestOverlay").fadeIn();
        $("#StudentPickupRequestOverlayButtonLoader").fadeIn();
    };

    function hideOverlay() {
        $("#StudentPickupRequestOverlay").fadeOut();
        $("#StudentPickupRequestOverlayButtonLoader").fadeOut();
    };

}]);