app.controller('StudentTransportRequestController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', "$q", function ($scope, $http, $compile, $rootScope, $subscription, $toaster, $timeout,$q) {
    console.log('StudentTransportRequestController controller loaded.');

    $scope.Applications = [];
    $scope.Casts = [];
    $scope.Relegions = [];
    $scope.Genders = [];
    $scope.Classes = [];
    $scope.Categories = [];

    $scope.init = function (model) {
        $scope.Applications = model;
        //$timeout(function () {
        //    showOverlay();
        //    $.ajax({
        //        type: 'GET',
        //        url: utility.myHost + "/Home/GetStudentDetailsForTransportApplication?id=" + 0 + "&parameter=" + null,
        //        success: function (result) {
        //            $timeout(function () {
        //                $scope.Applications = result.Response;
        //                console.log($scope.Applications);
        //            });

        //            hideOverlay();
        //        }
        //    });
        //});
        $q.all([
            GetSchool(),
        ]).then(function () {
            // completed;
            $('.preload-overlay').hide();
        });
        $('.preload-overlay').hide();
    };

    $scope.NewTransportApplicationClick = function () {
        window.location = "StudentTransportRequestApplication";
    };

    $scope.SubmitTransportApplicationClick = function () {

        //if ($("#FatherEmailID").val() == null || $("#FatherEmailID").val() == "") {
        //    $("#FatherEmailID").focus();
        //    $(this).prop("disabled", false);
        //    return false;
        //}
        //else if ($("#FatherContactNumber").val() == null || $("#FatherContactNumber").val() == "") {
        //    $("#FatherContactNumber").focus();
        //    $(this).prop("disabled", false);
        //    return false;
        //}
        //else if ($("#MotherEmailID").val() == null || $("#MotherEmailID").val() == "") {
        //    $("#MotherEmailID").focus();
        //    $(this).prop("disabled", false);
        //    return false;
        //}
        //else if ($("#MotherContactNumber").val() == null || $("#MotherContactNumber").val() == "") {
        //    $("#MotherContactNumber").focus();
        //    $(this).prop("disabled", false);
        //    return false;
        //}
        if ($("#EmergencyContactNumber").val() == null || $("#EmergencyContactNumber").val() == "") {
            $("#EmergencyContactNumber").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Please fill emergency contact number");
            return false;
        }
        else if ($("#LocationName").val() == null || $("#LocationName").val() == "") {
            $("#LocationName").focus();
            $(this).prop("disabled", false);
            return false;
        }
        else if ($("#Building_FlatNo").val() == null || $("#Building_FlatNo").val() == "") {
            $("#Building_FlatNo").focus();
            $(this).prop("disabled", false);
            return false;
        }
        else if ($("#StreetNo").val() == null || $("#StreetNo").val() == "") {
            $("#StreetNo").focus();
            $(this).prop("disabled", false);
            return false;
        }
        else if ($("#ZoneNo").val() == null || $("#ZoneNo").val() == "") {
            $("#ZoneNo").focus();
            $(this).prop("disabled", false);
            return false;
        }

        //else if (document.getElementById("IsMedicalCondition").checked && $("#Remarks").val() == null || document.getElementById("IsMedicalCondition").checked && $("#Remarks").val() == "") {
        //    $("#Remarks").focus();
        //    $(this).prop("disabled", false);
        //    callToasterPlugin('error', "Please Explian about Medical Condition");
        //    return false;
        //}

        else if (document.getElementById("TransportTerms").checked == false) {
            $("#TransportTerms").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Transport once allocated, minimum one month fee will be payable even if it is not used at all, please tick the check box to continue");
            return false;
        }
        else if (document.getElementById("TermsAndConditions").checked == false) {
            $("#TermsAndConditions").focus();
            $(this).prop("disabled", false);
            callToasterPlugin('error', "Please Read School Transportation Rules and Regulations");
            return false;
        }

        else {
            $("#SubmitapplicationBtn").html("Submitting...");
            $("#SubmitapplicationBtn").prop("disabled", true);

            var data = $scope.Applications;

            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                url: utility.myHost + "/Home/SubmitTransportApplication",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.IsError) {
                        callToasterPlugin('error', result.Response);
                        $("#SubmitapplicationBtn").prop("disabled", false);
                        $("#SubmitapplicationBtn").html("Submit");
                        return false;
                    }
                    else {
                        window.location = "StudentTransportRequestList";
                    }
                }
            });
        }

    };

    //$scope.ChangeMedicalCondition = function (data) {

    //    if ($('#Yes').prop("checked") == true) {
    //        data.IsMedicalCondition = true;
    //    }

    //    else if ($('#No').prop("checked") == true) {
    //        data.IsMedicalCondition = false;
    //    }
    //};

    function GetSchool() {
        return $q(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=School&defaultBlank=false",
                success: function (result) {
                    $scope.Schools = result;
                },
                complete: function (result) {
                    $timeout(function () {
                        $('#School').val($('#School').val());
                        resolve();
                    }, 1000);
                }
            });
        });
    }

    //$scope.LocationChange = function () {
    //    if (document.getElementById("LocationChange").checked) {
    //        document.getElementById("IsNewRider").checked = false;
    //    }
    //    else {
    //        document.getElementById("IsNewRider").checked = true;
    //    }
    //}

    function showOverlay() {
        $("#StudentTransportRequestListOverlay").fadeIn();
        $("#StudentTransportRequestListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#StudentTransportRequestListOverlay").fadeOut();
        $("#StudentTransportRequestListOverlayButtonLoader").fadeOut();
    }

    function callToasterPlugin(status, title) {
        new Notify({
            status: status,
            title: title,
            effect: 'fade',
            speed: 300,
            customClass: null,
            customIcon: null,
            showIcon: true,
            showCloseButton: true,
            autoclose: true,
            autotimeout: 3000,
            gap: 20,
            distance: 20,
            type: 1,
            position: 'center'
        })
    };

}]);