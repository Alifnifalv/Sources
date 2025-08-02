app.controller('GalleryController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Gallery Controller loaded.');


    $scope.GallerySchool = {};
    $scope.SchoolAcademicYear = {};

    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();
        $scope.GetSchoolsList();
    };


    $scope.GetGalleryDataByAcademic = function () {

        //showOverlay();
        $scope.Gallery = [];
        var academicYearID = $scope.SchoolAcademicYear?.Key;
        $.ajax({
            type: "GET",
            data: { academicYearID: academicYearID },
            url: utility.myHost + "/Home/GetGalleryView",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Gallery = result.Response;
                    });
                }
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }

    $scope.GetSchoolsList = function () {
        $scope.Schools = [];

        $.ajax({
            type: "GET",
            data: { loginID: $root.LoginID },
            url: utility.myHost + "/Home/GetSchoolsByParentLoginID?loginID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.Schools = result;
            },
            complete: function (result) {
                if ($scope.Schools.length > 0) {
                    $scope.Schools.forEach(x => {
                        if (x.Key != null) {
                            if (x.Key == 30) {
                                $scope.$apply(function () {
                                    $timeout(function () {
                                        $scope.GallerySchool.Key = x.Key;
                                        $scope.GallerySchool.Value = x.Value;

                                        $scope.SchoolChanges();
                                    }, 1000);
                                });
                            }
                        };
                    });
                }
            },
            error: function () {

            }
        });
    }

    $scope.openImage = function () {
        fsLightbox.open();
    };

    $scope.SchoolChanges = function () {
        $scope.SchoolAcademicYear = {};

        $('.preload-overlay').show();
        var schoolID = $scope.GallerySchool?.Key;
        $.ajax({
            type: "GET",
            data: { schoolID: schoolID },
            url: utility.myHost + "/Home/GetAcademicYearBySchool?schoolID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.AcademicYears = result.Response;
                    });
                }
            },
            complete: function (result) {
                if ($scope.AcademicYears.length == 1) {
                    $scope.AcademicYears.forEach(x => {
                        if (x.Key != null) {
                            $scope.$apply(function () {
                                $timeout(function () {
                                    $scope.SchoolAcademicYear.Key = x.Key;
                                    $scope.SchoolAcademicYear.Value = x.Value;

                                    $scope.GetGalleryDataByAcademic();
                                }, 1000);
                            });
                        };
                    });
                }
            },
            error: function () {

            }
        });
    }


    function showOverlay() {
        $("#CircularListOverlay").fadeIn();
        $("#CircularListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#CircularListOverlay").fadeOut();
        $("#CircularListOverlayButtonLoader").fadeOut();
    }

}]);