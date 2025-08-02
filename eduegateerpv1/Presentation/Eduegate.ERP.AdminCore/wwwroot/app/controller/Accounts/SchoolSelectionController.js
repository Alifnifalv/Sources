app.controller('SchoolSelectionController', ['$scope', '$http', "$rootScope", "$window", "$compile", "$timeout", 'FlickityService',
    function ($scope, $http, $root, $window, $compile, $timeout, FlickityService) {
        console.log('SchoolSelectionController controller loaded.');
        $root.IsGlobalError = false;
        $root.GlobalMessage = null;
        $scope.HeaderInfoModel = {};
        $scope.HeaderInfoModel.ProfileFile = "Images/profilepic.png";
        $scope.SelectedTheme = 'gray';
        $scope.SelectedLayout = 'multi';
        $scope.mailData = [];
        $scope.LoginID = null;
        $scope.AcademicYear = [];
        $scope.Schools = [];
        $scope.SelectedAcademicYear = { "Key": null, "Value": null };
        $scope.SelectedSchool = { "Key": null, "Value": null };
        $scope.SelectedAcademicYearDetail = null;
        $scope.SelectedSchoolDetail = null;
        $scope.AcademicYearList = [];
        $scope.SelectedLanguage = 'en';  

    

   

        $scope.Init = function () {

            //$http({
            //    method: 'Get', url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=ActiveAcademicYear&defaultBlank=false",
            //}).then(function (result) {
            //    $scope.AcademicYear = result.data;
            //});

            //$http({
            //    method: 'Get', url: utility.myHost + "Mutual/GetLookUpData?lookType=School&defaultBlank=false",
            //})

            //$scope.GetSchoolList();
            $scope.GetSchoolsProfileWithAcademicYear();
        };

            //$scope.GetSchoolList = function () {
            //    $.ajax({
            //        method: 'Get',
            //        url: utility.myHost + "Mutual/GetLookUpData?lookType=School&defaultBlank=false",
            //        success: function (result) {
            //            $scope.Schools = result;

            //            $scope.GetAcademicYearList();
            //        }
            //    });
            //};

            //$scope.GetAcademicYearList = function () {
            //    $http({
            //        url: utility.myHost + "Mutual/GetActiveAcademicYearListData",
            //        method: "GET"
            //    })
            //        .then(function (response) {
            //            $scope.AcademicYearList = response.data;

            //            if ($scope.AcademicYearList.length > 0) {
            //                $scope.FillSchoolAcademicYears();
            //            }

            //            $scope.LoadCaraousel();
            //        })
            //        .catch(function (error) {
            //            // Handle errors appropriately
            //            console.error("Error getting academic year list:", error);
            //        });
            //};

            $scope.GetSchoolsProfileWithAcademicYear = function () {
                $http({
                    url: utility.myHost + "Mutual/GetSchoolsProfileWithAcademicYear",
                    method: "GET"
                }).then(function (response) {
                    $scope.SchoolsProfileWithAcademicYear = response.data;

                    $scope.LoadCaraousel();
                })
            };

        
        $scope.AcademicYearChanges = function (academicYear, row) {
            $scope.SelectedSchool = {
                "Key": row.SchoolID,
                "Value": row.SchoolName
            };
            $scope.SelectedAcademicYear = {
                "Key": academicYear.Key,
                "Value": academicYear.Value
            }
        };
        
        $scope.LoadCaraousel = function () {

            const element = angular.element(document.getElementById('demo-slider'));

            $timeout(() => {
                $scope.options = {
                    // imagesLoaded: true,
                    // draggable: true,
                    // wrapAround: true,

                    setGallerySize: false,
                    pageDots: false,
                    initialIndex: 2
                };

                // Initialize our Flickity instance
                const flkty = FlickityService.create(element[0], element[0].id, $scope.options);

                //flkty.then(result => {
                //    const flickityInstance = result; // The resolved Flickity instance


                //    const transformer = new FlickityTransformer(result.instance, [
                //        {
                //            name: "scale",
                //            stops: [
                //                [-300, 0.8],
                //                [0, 1],
                //                [300, 0.8]
                //            ]
                //        },
                //        //{
                //        //    name: "translateY",
                //        //    stops: [
                //        //        [-1000, 500],
                //        //        [0, 0],
                //        //        [1000, 500]
                //        //    ]
                //        //},
                //        //{
                //        //    name: "perspective",
                //        //    stops: [
                //        //        [1, 600],
                //        //        [0, 600]
                //        //    ]
                //        //},
                //        //{
                //        //    name: "rotateY",
                //        //    stops: [
                //        //        [-300, -30],
                //        //        [0, 0],
                //        //        [300, 30]
                //        //    ]
                //        //}
                //    ]);
                //});
            });
            }

            //$scope.FillSchoolAcademicYears = function () {
            //    $scope.SchoolsWithAcademics = [];
            //    if ($scope.Schools.length > 0) {

            //        $scope.Schools.forEach(scl => {

            //            var academicYears = [];
            //            var ProfileImgae = ""

            //            $scope.AcademicYearList.forEach(x => {
            //                if (x.SchoolID == scl.Key) {
            //                    academicYears.push({
            //                        "Key": x.AcademicYearID,
            //                        "Value": x.Description + " " + "(" + x.AcademicYearCode + ")",
            //                    });
            //                };
            //            });

            //            $scope.SchoolProfileImage.forEach(x => {
            //                if (x.Key == scl.Key && x.Value) {
            //                    ProfileImgae = x.Value
            //                };
            //            });

            //            $scope.SchoolsWithAcademics.push({
            //                "Key": scl.Key,
            //                "Value": scl.Value,
            //                "AcademicYears": academicYears,
            //                "SchoolProfileImage": ProfileImgae,
            //            });
            //        });
            //    }
            //}
      

        const toastLiveExample = document.getElementById('liveToast')
        $scope.SaveSchool = function () {
            if ($scope.SelectedAcademicYear.Key == null) {
                const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
                    toastBootstrap.show()
                return
            }
            $.ajax({
                url: utility.myHost + "Account/ResetSchoolAcadamicYear",
                type: 'POST',
                data: { academicYearID: $scope.SelectedAcademicYear.Key, schoolID: $scope.SelectedSchool.Key },
                success: function (result) {
                    if (result.IsError === false) {
                        $scope.Message = result.Message;
                        var redirectUrl = utility.myHost + '?1';

                        if (result.MessageType != "") {
                            $scope.MessageType = result.MessageType;
                            $scope.ShowMessage = true;
                        }
                        $scope.$apply();
                        utility.redirect(redirectUrl);
                    };
                },
                error: function (err) {
                    $scope.MessageType = "Error";
                    $scope.ShowMessage = true;
                }
            });
        }

    }]);