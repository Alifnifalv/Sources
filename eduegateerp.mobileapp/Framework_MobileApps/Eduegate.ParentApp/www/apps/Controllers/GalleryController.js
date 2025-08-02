app.controller('GalleryController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('GalleryController loaded.');

    var dataService = rootUrl.SchoolServiceUrl;
    var Context = GetContext.Context();



    $scope.GallerySchool = {};
    $scope.SchoolAcademicYear = {};

    $scope.init = function () {


        $scope.GetSchoolsList();

    };




    $scope.GetGalleryDataByAcademic = function () {
        $scope.Gallery = [];
        var academicYearID = $scope.SchoolAcademicYear?.Key;
        $.ajax({
            type: 'GET',
            data: { academicYearID: academicYearID },
            url: dataService + "/GetGalleryView",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.Gallery = result.Response;
                    });
                }

            },
            complete: function (result) {
               
            },
            error: function () {
               
            },
        });

    }
    $scope.GetSchoolsList = function () {
        $scope.Schools = [];

        $.ajax({
            type: "GET",
            data: { loginID: Context.LoginID},
            url:dataService + "/GetSchoolsByParent",
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
            url: dataService + "/GetAcademicYearBySchool?30",
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



}]);