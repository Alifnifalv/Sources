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
        $scope.isSchoolSelected = false;




        $scope.Init = function () {

            $scope.GetSchoolsProfileWithAcademicYear();
        };

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
            const element = document.getElementById('demo-slider');

            $timeout(() => {
                const flkty = new Flickity(element, {
                    setGallerySize: false,
                    pageDots: false,
                    initialIndex: 2
                });

                $scope.$apply(() => {
                    $scope.activeSlide = flkty.selectedIndex;
                    $scope.onSlideChange($scope.activeSlide);
                });

                flkty.on('select', function () {
                    const selectedIndex = flkty.selectedIndex;

                    $scope.$apply(() => {

                        $scope.activeSlide = selectedIndex;
                        $scope.onSlideChange(selectedIndex);
                    });
                });
            });
        };

        $scope.onSlideChange = function (selectedIndex) {
            const selectedSchool = $scope.SchoolsProfileWithAcademicYear[selectedIndex];
            $scope.GetCurrentAcademicYear(selectedSchool);
        };


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

            const urlParams = new URLSearchParams(window.location.search);
            const languageCode = urlParams.get("language");

            $scope.isSchoolSelected = true;

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

                    //var lang = $rootScope.CultureLanguage

                    if (result.IsError === false) {
                        $scope.Message = result.Message;
                        var redirectUrl = utility.myHost + '?1';

                        if (languageCode) {
                            redirectUrl = utility.myHost + '\?language=' + languageCode;
                        }

                        if (result.MessageType != "") {
                            $scope.MessageType = result.MessageType;
                            $scope.ShowMessage = true;
                        }
                        $scope.$apply();
                        utility.redirect(redirectUrl);
                        $scope.isSchoolSelected = false;

                    };
                },
                error: function (err) {
                    $scope.isSchoolSelected = false;

                    $scope.MessageType = "Error";
                    $scope.ShowMessage = true;
                }
            });
        }

        $scope.GetCurrentAcademicYear = function (selectedSchool) {

            $http({
                url: utility.myHost + "Schools/School/GetCurrentAcademicYearBySchoolID?schoolID=" + selectedSchool.SchoolID,
                method: "GET"
            }).then(function (response) {
                //$scope.CurrentAcademicYear = response.data;
                $scope.acy = response.data;

                $scope.SelectedSchool = {
                    "Key": selectedSchool.SchoolID,
                    "Value": selectedSchool.SchoolName
                };

                $scope.SelectedAcademicYear = {
                    "Key": $scope.acy.Key,
                    "Value": $scope.acy.Value
                }
            })
        };

    }]);