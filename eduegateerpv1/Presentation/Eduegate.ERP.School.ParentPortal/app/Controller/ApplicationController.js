app.controller('ApplicationController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService",
    "toaster", '$timeout', '$location', "$q", function ($scope, $http, $compile, $rootScope, $subscription, $toaster,
        $timeout, $location, $q) {
        console.log('ApplicationController controller loaded.');
        $scope.Applications = [];
        $scope.Casts = [];
        $scope.Nationalities = [];
        $scope.Relegions = [];
        $scope.Genders = [];
        $scope.Classes = [];
        $scope.Categories = [];
        $scope.Countries = [];
        $scope.Schools = [];
        $scope.Syllabus = [];
        $scope.PreviousClassNames = [];
        //$scope.SchoolSyllabus = [];
        //$scope.AcademicYears = [];
        //$scope.Students = [];
        $scope.GaurdianTypes = [];
        $scope.Communitys = [];
        $scope.BloodGroups = [];
        $scope.VolunteerTypes = [];
        $scope.Languages = [];
        $scope.StudentApplication = null;
        $scope.IsStudentStudiedBefore = false;

        $scope.DocumentFileName = null;
        //$scope.DocumentFile = null;
        $scope.AttachmentId = null;
        $scope.ContentFileName = null;
        $scope.StudentPassportAttach = null;
        $scope.TCAttach = null;
        $scope.FatherQIDAttach = null;
        $scope.MotherQIDAttach = null;
        $scope.StudentQIDAttach = null;
        $scope.uploadedDocuments = [];
        $scope.uploadedFile = null;

        $scope.ClassKey = {};

        $scope.init = function (model) {
            $scope.StudentApplication = model;
            if (model.Class != null || model.Class != undefined) {
                if ($scope.ClassKey.Key == undefined || $scope.ClassKey.Key == null) {
                    $scope.ClassKey = model.Class
                }
                $scope.onclassDataChange();
            }
            $scope.previousCheckbox();
            $scope.changeNationality();
            $q.all([
                GetCast(),
                GetNationality(),
                GetCommunity(),
                GetVolunteerType(),
                GetBloodGroup(),
                GetPreviousClassNames(),
                GetGender(),
                GetRelegion(),
                //GetClasses(),
                GetStudentCategory(),
                GetCountries(),
                GetSchool(),
                GetSchoolSyllabus(),
                GetSyllabus(),
                //GetAcademicYear(),                
                //GetStudent(),
                GetGuardianType(),
                GetSecondLanguage(),
                GetThirdLanguage(),
                GetStreamGroup(),
            ]).then(function () {
                // completed;
                $('.preload-overlay').hide();
            });


        };

        $scope.previousCheckbox = function () {

            var checkBox = document.getElementById("IsStudentStudiedBeforeForPortal");

            if (checkBox.checked === true) {
                $scope.IsStudentStudiedBeforeForPortal = true;
            }
            else {
                $scope.IsStudentStudiedBeforeForPortal = false;
            }

        };

        function GetCast() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Cast&defaultBlank=false",
                    success: function (result) {
                        $scope.Casts = result;
                        $timeout(function () {
                            $('#Cast').val($('#CastID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }
        function GetNationality() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=LeadNationality&defaultBlank=false",
                    success: function (result) {
                        $scope.Nationalities = result;
                        $timeout(function () {
                            $('#Nationality').val($('#NationalityID').val());
                            $('#FatherCountry').val($scope.StudentApplication.FatherMotherDetails.FatherCountryID);
                            $('#MotherCountry').val($scope.StudentApplication.FatherMotherDetails.MotherCountryID);
                            $('#GuardianNationality').val($scope.StudentApplication.GuardianDetails.GuardianNationalityID);
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetSecondLanguage() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=SecondLanguage&defaultBlank=false",
                    success: function (result) {
                        $scope.SecondLanguages = result;
                        $timeout(function () {
                            $('#SecoundLanguageString').val($('#SecoundLanguageID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetThirdLanguage() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=ThirdLanguage&defaultBlank=false",
                    success: function (result) {
                        $scope.ThirdLanguages = result;
                        $timeout(function () {
                            $('#ThridLanguageString').val($('#ThridLanguageID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetCommunity() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Community&defaultBlank=false",
                    success: function (result) {
                        $scope.Communitys = result;
                        $timeout(function () {
                            $('#Community').val($('#CommunityID').val());
                            //$('select').formSelect();
                            selectElement("Community", $scope.StudentApplication.CommunityID);
                            resolve();
                        }, 1000);
                    }
                });
            });
        }


        function GetVolunteerType() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=VolunteerType&defaultBlank=false",
                    success: function (result) {
                        $scope.VolunteerTypes = result;
                        $timeout(function () {
                            $('#CanYouVolunteerToHelpOneString').val($scope.StudentApplication.FatherMotherDetails.CanYouVolunteerToHelpOneID);
                            $('#CanYouVolunteerToHelpTwoString').val($scope.StudentApplication.FatherMotherDetails.CanYouVolunteerToHelpTwoID);
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }


        function GetBloodGroup() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=BloodGroup&defaultBlank=false",
                    success: function (result) {
                        $scope.BloodGroups = result;
                        $timeout(function () {
                            $('#BloodGroup').val($('#BloodGroupID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetPreviousClassNames() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=PreviousClassNames&defaultBlank=true ",
                    success: function (result) {
                        $scope.PreviousClassNames = result;
                        $timeout(function () {
                            $('#PreviousSchoolClassClassKey').val($scope.StudentApplication.PreviousSchoolDetails.PreviousSchoolClassCompletedID);
                            resolve();
                        }, 1000);
                    }
                });
            });
        }


        function GetGender() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Gender&defaultBlank=false",
                    success: function (result) {
                        $scope.Genders = result;
                        $timeout(function () {
                            $('#Gender').val($('#GenderID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetRelegion() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Relegion&defaultBlank=false",
                    success: function (result) {
                        $scope.Relegions = result;
                        $timeout(function () {
                            $('#Relegion').val($('#RelegionID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

       

        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            if (valueToSelect != null && valueToSelect != "") {
                element.value = valueToSelect;
            }
        }

        function GetStudentCategory() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=StudentCategory&defaultBlank=false",
                    success: function (result) {
                        $scope.Categories = result;
                        $timeout(function () {
                            $('#Category').val($('#CategoryID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetCountries() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetCountries",
                    success: function (result) {
                        $scope.$apply(function () {
                            $scope.Countries = result;
                        });

                        $timeout(function () {
                            $('#StudentCountryofIssue').val($('#CountryofIssueID').val());
                            $('#FatherCountryofIssue').val($scope.StudentApplication.FatherMotherDetails.FatherCountryofIssueID);
                            $('#MotherCountryofIssue').val($scope.StudentApplication.FatherMotherDetails.MotherCountryofIssueID);
                            $('#GuardianCountryofIssue').val($scope.StudentApplication.GuardianDetails.CountryofIssueID);
                            $('#Country').val($scope.StudentApplication.Address.CountryID);
                            $('#StudentCoutryOfBrith').val($('#StudentCoutryOfBrithID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }


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
                            $('#School').val($('#SchoolID').val());
                            GetAcademicYearBySchool($('#SchoolID').val());
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        function GetSyllabus() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Syllabus&defaultBlank=true",
                    success: function (result) {
                        $scope.Syllabus = result;
                        $timeout(function () {

                            $('#PreviousSchoolSyllabus').val($scope.StudentApplication.PreviousSchoolDetails.PreviousSchoolSyllabusID);
                            //selectElement("PreviousSchoolSyllabus", $scope.StudentApplication.PreviousSchoolDetails.PreviousSchoolSyllabusID);
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }      

        function GetSchoolSyllabus() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=SchoolSyllabus&defaultBlank=false",
                    success: function (result) {
                        $scope.SchoolSyllabus = result;
                       
                    },
                    complete: function (result) {
                        $timeout(function () {
                            $('#CurriculamString').val($('#CurriculamID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        //function GetAcademicYear() {
        //    return $q(function (resolve, reject) {
        //        $.ajax({
        //            type: 'GET',
        //            url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=AcademicYear&defaultBlank=false",
        //            success: function (result) {
        //                $scope.AcademicYears = result;
        //                /*Sibling*/
        //            },
        //            complete: function (result) {
        //                $timeout(function () {

        //                    $('#SchoolAcademicyear').val($('#AcademicyearID').val());
        //                    selectElement("SchoolAcademicyear", $scope.StudentApplication.AcademicyearID);
        //                    //$('select').formSelect();
        //                    resolve();
        //                }, 1000);
        //            }
        //        });
        //    });
        //}

        //function GetStudent() {
        //    return $q(function (resolve, reject) {
        //        $.ajax({
        //            type: 'GET',
        //            url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=Student&defaultBlank=false",
        //            success: function (result) {
        //                $scope.Student = result;
        //                $timeout(function () {
        //                    $('#Sibling').val($('#Sibling').val());
        //                    //$('select').formSelect();
        //                    resolve();
        //                }, 1000);
        //            }
        //        });
        //    });
        //}

        function GetGuardianType() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=GuardianType&defaultBlank=false",
                    success: function (result) {
                        $scope.GaurdianTypes = result;
                        $timeout(function () {
                            $('#GuardianStudentRelationShip').val($scope.StudentApplication.GuardianDetails.GuardianStudentRelationShipID);
                            $('#PrimaryContact').val($('#PrimaryContactID').val());
                            //$('select').formSelect();
                            resolve();
                        }, 1000);
                    }
                });
            });
        }


        $scope.RelegionChanges = function () {
            $('.preload-overlay').show();
            var relegionID = $scope.Relegion?.Key;
            $.ajax({
                type: "GET",
                data: { relegionID: relegionID },
                url: utility.myHost + "/Home/GetCastByRelegion?relegionID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.Casts = result.Response;

                            $timeout(function () {
                                $('#Cast').val($('#CastID').val());
                                //$('select').formSelect();
                                selectElement("Cast", $scope.StudentApplication.CastID);
                            }, 1000);

                        });
                    }

                    $('.preload-overlay').hide();

                },
                error: function () {
                    $('.preload-overlay').hide();
                },
                complete: function (result) {
                    $('.preload-overlay').hide();
                }
            });
        }

        function GetClassesBySchool(schoolID) {
            return $q(function (resolve, reject) {
               
                $.ajax({
                    type: "GET",
                    data: { schoolID: schoolID },
                    url: utility.myHost + "/Home/GetClassesBySchool?schoolID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {

                        $scope.Classes = result.Response;

                    },
                    complete: function (result) {
                        $scope.$apply(function () {
                            $timeout(function () {
                                $('#ClassKey').val($('#ClassID').val());
                                //$('select').formSelect();
                                selectElement("ClassKey", $scope.StudentApplication.ClassID);
                                //$('#PreviousSchoolClassClassKey').val($scope.StudentApplication.PreviousSchoolDetails.PreviousSchoolClassCompletedID);
                            }, 1000);
                        });
                    }

                });
            });

        }

        function GetAcademicYearBySchool(schoolID) {
            if (schoolID == "" || schoolID == null) {
                return false;
            }
            return $q(function (resolve, reject) {
               
                $.ajax({
                    type: "GET",
                    data: { schoolID: schoolID },
                    url: utility.myHost + "/Home/GetAcademicYearBySchool?schoolID",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.AcademicYears = result.Response;                      
                    },
                    complete: function (result) {
                        $scope.$apply(function () {
                        $timeout(function () {
                            $('#SchoolAcademicyear').val($('#AcademicyearID').val());
                            //$('select').formSelect();
                            selectElement("SchoolAcademicyear", $scope.StudentApplication.AcademicyearID);
                            GetClassesBySchool($('#SchoolID').val());
                        }, 1000);
                        });
                    }
                });
            });
        }

        //$scope.AcademicYearChanges = function () {
        //    //$('.preload-overlay').show();
        //    var academicyearID = $scope.SchoolAcademicyear?.Key;
        //    $.ajax({
        //        type: "GET",
        //        data: { academicyearID: academicyearID },
        //        url: utility.myHost + "/Home/GetClasseByAcademicyear?academicyearID",
        //        contentType: "application/json;charset=utf-8",
        //        success: function (result) {
        //            if (!result.IsError && result !== null) {
        //                $scope.$apply(function () {
        //                    $scope.Classes = result.Response;

        //                    $timeout(function () {
        //                        $('#ClassKey').val($('#ClassID').val());
        //                        //$('select').formSelect();
        //                        selectElement("ClassKey", $scope.StudentApplication.ClassID);
        //                    }, 1000);

        //                });
        //            }

        //            $('.preload-overlay').hide();

        //        },
        //        error: function () {
        //            $('.preload-overlay').hide();
        //        },
        //        complete: function (result) {
        //            $scope.$apply(function () {
                      
        //                $timeout(function () {
        //                    $('#ClassKey').val($('#ClassID').val());
        //                    //$('select').formSelect();
        //                    selectElement("ClassKey", $scope.StudentApplication.ClassID);
        //                }, 1000);

        //            });
        //            $('.preload-overlay').hide();
        //        }
        //    });
        //}

        function GetStreamGroup() {
            return $q(function (resolve, reject) {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "/Home/GetDynamicLookUpData?lookType=StreamGroups&defaultBlank=false",
                    success: function (result) {
                        $scope.StreamGroups = result;
                        $timeout(function () {
                            $('#StreamGroup').val($scope.StudentApplication.StreamGroupID);
                            resolve();
                        }, 1000);
                    }
                });
            });
        }

        $scope.StreamGroupChanges = function () {
            $('.preload-overlay').show();
            var getID = document.getElementById("StreamGroup");
            var streamGroupID = getID.value;
            $.ajax({
                type: "GET",
                data: { streamGroupID: streamGroupID },
                url: utility.myHost + "/Home/GetStreamByStreamGroup?streamGroupID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.Streams = result.Response;

                            $timeout(function () {
                                $('#Stream').val($scope.StudentApplication.StreamID);
                            }, 10);

                        });
                    }

                    $('.preload-overlay').hide();
                },
                error: function () {
                    $('.preload-overlay').hide();

                },
                complete: function (result) {
                    $('.preload-overlay').hide();
                }
            });
        }

        $scope.onclassDataChange = function () {
            var id = $scope.ClassKey.Key;
            var id = $scope.ClassKey.Key;
            var filterClass = $scope.ClassKey;
            var trimdClassName = filterClass.Value.substring(0, filterClass.Value.indexOf(' -'));
                //filterClass.Value
                //.replace(" - Meshaf", "")
                //.replace(" - Westbay", "")
                //.replace(" - Thumama.", "");

            if (id == ("7") || id == ("38")) {
                $("#Stream").val("");
                $scope.onStreams = true;
            }
            else {
                $scope.onStreams = false;
            }

            if (trimdClassName.match(/Class KG*/)) {
                $scope.HideSecondLanguage = false;
            }
            else {
                $scope.HideSecondLanguage = true;
            }
            if (trimdClassName == 'Class 1' || trimdClassName == 'Class 2' || trimdClassName == 'Class 3' || trimdClassName == 'Class 4' || trimdClassName == 'Class 9' || trimdClassName == 'Class 10' || trimdClassName.match(/Class KG*/)) {
                $scope.HideThirdLanguage = false;
            }
            else {
                $scope.HideThirdLanguage = true;
            }
        };


        $scope.SchoolChanges = function () {
            $('.preload-overlay').show();
            var schoolID = $scope.School?.Key;
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

                    $('.preload-overlay').hide();

                },
                error: function () {
                    $('.preload-overlay').hide();
                },
                complete: function (result) {
                    $scope.$apply(function () {
                       
                        $timeout(function () {
                            $('#SchoolAcademicYear').val($('#AcademicYearID').val());
                            //$('select').formSelect();
                            selectElement("SchoolAcademicYear", $scope.StudentApplication.AcademicYearID);
                            GetClassesBySchool(schoolID);
                        }, 1000);

                    });
                    $('.preload-overlay').hide();
                }
            });
        }

        $scope.changeNationality = function () {

            var nationalityID = $scope.StudentApplication.NationalityID;

            if (nationalityID == 82) {
                $("#AdhaarCardNo").removeAttr("disabled");
            }
            else {
                $("#AdhaarCardNo").val('');
                $("#AdhaarCardNo").attr("disabled", "disabled");
            }

        };
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
                position: 'right top'
            })
        };
        $scope.DownloadURL = function (url) {
            var link = document.createElement("a");
            link.href = url;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            delete link;
        };

        $scope.submitNewApplicationDocument = function (AttachmentID) {
            showOverlay();

            $scope.AttachmentID = AttachmentID;
            var xhr = new XMLHttpRequest();
            var fd = new FormData();
                //if (filepik.value)
                //{
            $scope.uploadedFile = document.getElementById($scope.AttachmentID).files[0];
                //$scope.DocumentFile = $scope.uploadedFile.files[0]
                //$scope.StudentApplication.DocumentsUpload[0].ContentFileName = $scope.DocumentFileName = filepik.value
                //}
                /* for (i = 0; i < $scope.DocumentFile.files.length; i++) {*/
            fd.append($scope.uploadedFile.name, $scope.uploadedFile);
                //}
                //"Content/UploadContents"
            xhr.open("POST", utility.myHost + "/Content/UploadStudentDocuments", true);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    if (xhr.response) {
                        $scope.$apply(function () {

                            switch ($scope.AttachmentID) {
                                case "DocumentsUpload_0__BirthCertificateAttach":
                                    $("#DocumentsUpload_0__BirthCertificateReferenceID").val(xhr.response);
                                    $("#ContentFileDownload").show();
                                    $("#ContentFileDownload_Trash").show();
                                    callToasterPlugin('success', 'Birth Certificate Uploaded successfully');
                                    break;
                                case "DocumentsUpload_0__StudentPassportAttach":
                                    $("#DocumentsUpload_0__StudentPassportReferenceID").val(xhr.response);
                                    $("#StudentPassportDownload").show();
                                    $("#StudentPassportDownload_Trash").show();
                                    callToasterPlugin('success', 'Student Passport Uploaded successfully');
                                    break;
                                case "DocumentsUpload_0__TCAttach":
                                    $("#DocumentsUpload_0__TCReferenceID").val(xhr.response);
                                    $("#TCReferenceDownload").show();
                                    $("#TCReferenceDownload_Trash").show();
                                    callToasterPlugin('success', 'TC Reference Uploaded successfully');
                                    break;
                                case "DocumentsUpload_0__FatherQIDAttach":
                                    $("#DocumentsUpload_0__FatherQIDReferenceID").val(xhr.response);
                                    $("#FatherQIDReferenceDownload").show();
                                    $("#FatherQIDReferenceDownload_Trash").show();
                                    callToasterPlugin('success', 'Father QID Uploaded successfully');
                                    break;
                                case "DocumentsUpload_0__MotherQIDAttach":
                                    $("#DocumentsUpload_0__MotherQIDReferenceID").val(xhr.response);
                                    $("#MotherQIDDownload").show();
                                    $("#MotherQIDDownload_Trash").show();
                                    callToasterPlugin('success', 'Mother QID Uploaded successfully');
                                    break;
                                case "DocumentsUpload_0__StudentQIDAttach":
                                    $("#DocumentsUpload_0__StudentQIDReferenceID").val(xhr.response);
                                    $("#StudentQIDDownload").show();
                                    $("#StudentQIDDownload_Trash").show();
                                    callToasterPlugin('success', 'Student QID Uploaded successfully');
                                    break;
                                default:
                            }
                            $scope.uploadedFile = null;
                            $scope.AttachmentID = null;
                            hideOverlay();
                        });
                    }
                }
            };
            xhr.send(fd);
        }

        $scope.submitNewApplicationDocument1 = function (AttachmentName) {

            $scope.AttachmentID = AttachmentName;

            switch (AttachmentName) {
                case "BirthCertificate":
                    var filepik = document.getElementById("ContentFileNameDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].ContentFileName = $scope.DocumentFileName = filepik.value
                    }

                    break;
                case "StudentPassport":
                    var filepik = document.getElementById("StudentPassportAttachDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].StudentPassportAttach = $scope.DocumentFileName = filepik.value
                    }

                    break;
                case "TC":
                    var filepik = document.getElementById("TCAttachDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].TCAttach = $scope.DocumentFileName = filepik.value
                    }

                    break;
                case "FatherQID":
                    var filepik = document.getElementById("FatherQIDAttachDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].FatherQIDAttach = $scope.DocumentFileName = filepik.value
                    }

                    break;
                case "MotherQID":
                    var filepik = document.getElementById("MotherQIDAttachDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].MotherQIDAttach = $scope.DocumentFileName = filepik.value
                    }

                    break;
                case "StudentQID":
                    var filepik = document.getElementById("StudentQIDAttachDocPik");
                    if (filepik.value) {
                        $scope.DocumentFile = filepik.files[0]
                        $scope.StudentApplication.DocumentsUpload[0].StudentQIDAttach = $scope.DocumentFileName = filepik.value
                    }

                    break;
                default:
                // code block
            }

            $.ajax({
                type: "POST",
                data: { "UploadedFile": $scope.DocumentFile, "AttachmentName": $scope.AttachmentName },
                url: utility.myHost + "/Content/UploadStudentDocuments",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {

                        switch ($scope.AttachmentName) {
                            case "BirthCertificate":
                                $scope.StudentApplication.DocumentsUpload[0].ContentFileIID = result.Response;
                                callToasterPlugin('success', 'Birth Certificate Uploaded successfully');
                                break;
                            case "StudentPassport":
                                $scope.StudentApplication.DocumentsUpload[0].StudentPassportReferenceID = result.Response;
                                callToasterPlugin('success', 'Student Passport Uploaded successfully');
                                break;
                            case "TC":
                                $scope.StudentApplication.DocumentsUpload[0].TCReferenceID = result.Response;
                                callToasterPlugin('success', 'TC Uploaded successfully');
                                break;
                            case "FatherQID":
                                $scope.StudentApplication.DocumentsUpload[0].FatherQIDReferenceID = result.Response;
                                callToasterPlugin('success', 'Father QID Uploaded successfully');
                                break;
                            case "MotherQID":
                                $scope.StudentApplication.DocumentsUpload[0].MotherQIDReferenceID = result.Response;
                                callToasterPlugin('success', 'Mother QID Uploaded successfully');
                                break;
                            case "StudentQID":
                                $scope.StudentApplication.DocumentsUpload[0].StudentQIDReferenceID = result.Response;
                                callToasterPlugin('success', 'Student QID Uploaded successfully');
                                break;
                            default:
                            // code block
                        }
                    }
                },
                error: function () {},
                complete: function (result) {return result;}
            });
        };

        $scope.SchoolChanges = function () {
            $('.preload-overlay').show();
            var schoolID = $scope.School?.Key;
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

                    $('.preload-overlay').hide();

                },
                error: function () {
                    $('.preload-overlay').hide();
                },
                complete: function (result) {
                    $scope.$apply(function () {

                        $timeout(function () {
                            $('#SchoolAcademicYear').val($('#AcademicYearID').val());
                            //$('select').formSelect();
                            selectElement("SchoolAcademicYear", $scope.StudentApplication.AcademicYearID);
                            GetClassesBySchool(schoolID);
                        }, 1000);

                    });
                    $('.preload-overlay').hide();
                }
            });
        }

        $scope.AutoFillFatherDetails = function () {
            if (document.getElementById(
                "GuardianSameAsFatherDetails").checked) {
                document.getElementById("GuardianFirstName").value = document.getElementById("FatherFirstName").value;
                document.getElementById("GuardianMiddleName").value = document.getElementById("FatherMiddleName").value;
                document.getElementById("GuardianLastName").value = document.getElementById("FatherLastName").value;
                document.getElementById("GuardianNationality").value = document.getElementById("FatherCountry").value;
                document.getElementById("GuardianOccupation").value = document.getElementById("FatherOccupation").value;
                document.getElementById("GuardianCompanyName").value = document.getElementById("FatherCompanyName").value;
                document.getElementById("GuardianPassportNumber").value = document.getElementById("FatherPassportNumber").value;
                document.getElementById("GuardianCountryofIssue").value = document.getElementById("FatherCountryofIssue").value;
                document.getElementById("GuardianPassportNoIssueString").value = document.getElementById("FatherPassportNoIssueString").value;
                document.getElementById("GuardianPassportNoExpiryString").value = document.getElementById("FatherPassportNoExpiryString").value;
                document.getElementById("GuardianNationalID").value = document.getElementById("FatherNationalID").value;
                document.getElementById("GuardianNationalIDNoIssueDateString").value = document.getElementById("FatherNationalDNoIssueDateString").value;
                document.getElementById("GuardianNationalIDNoExpiryDateString").value = document.getElementById("FatherNationalDNoExpiryDateString").value;
                document.getElementById("GuardianMobileNumber").value = document.getElementById("MobileNumber").value;
                document.getElementById("GuardianEmailID").value = document.getElementById("EmailID").value;
                var filterdata = $scope.GaurdianTypes.filter(x => x.Value == "Father");
                var fatherTypeID = filterdata[0].Key;
                document.getElementById("GuardianStudentRelationShip").value = fatherTypeID;
                document.getElementById(
                    "GuardianSameAsMotherDetails").checked = false;
            }
            else {
                document.getElementById("GuardianFirstName").value = null;
                document.getElementById("GuardianMiddleName").value = null;
                document.getElementById("GuardianLastName").value = null;
                document.getElementById("GuardianNationality").value = null;
                document.getElementById("GuardianOccupation").value = null;
                document.getElementById("GuardianCompanyName").value = null;
                document.getElementById("GuardianPassportNumber").value = null;
                document.getElementById("GuardianCountryofIssue").value = null;
                document.getElementById("GuardianPassportNoIssueString").value = null;
                document.getElementById("GuardianPassportNoExpiryString").value = null;
                document.getElementById("GuardianNationalID").value = null;
                document.getElementById("GuardianNationalIDNoIssueDateString").value = null;
                document.getElementById("GuardianNationalIDNoExpiryDateString").value = null;
                document.getElementById("GuardianMobileNumber").value = null;
                document.getElementById("GuardianEmailID").value = null;
                document.getElementById("GuardianStudentRelationShip").value = null;
            }
        };

        $scope.AutoFillMotherDetails = function () {
            if (document.getElementById(
                "GuardianSameAsMotherDetails").checked) {
                document.getElementById("GuardianFirstName").value = document.getElementById("MotherFirstName").value;
                document.getElementById("GuardianMiddleName").value = document.getElementById("MotherMiddleName").value;
                document.getElementById("GuardianLastName").value = document.getElementById("MotherLastName").value;
                document.getElementById("GuardianNationality").value = document.getElementById("MotherCountry").value;
                document.getElementById("GuardianOccupation").value = document.getElementById("MotherOccupation").value;
                document.getElementById("GuardianCompanyName").value = document.getElementById("MotherCompanyName").value;
                document.getElementById("GuardianPassportNumber").value = document.getElementById("MotherPassportNumber").value;
                document.getElementById("GuardianCountryofIssue").value = document.getElementById("MotherCountryofIssue").value;
                document.getElementById("GuardianPassportNoIssueString").value = document.getElementById("MotherPassportNoIssueString").value;
                document.getElementById("GuardianPassportNoExpiryString").value = document.getElementById("MotherPassportNoExpiryString").value;
                document.getElementById("GuardianNationalID").value = document.getElementById("MotherNationalID").value;
                document.getElementById("GuardianNationalIDNoIssueDateString").value = document.getElementById("MotherNationalDNoIssueDateString").value;
                document.getElementById("GuardianNationalIDNoExpiryDateString").value = document.getElementById("MotherNationaIDNoExpiryDateString").value;
                document.getElementById("GuardianMobileNumber").value = document.getElementById("MotherMobileNumber").value;
                document.getElementById("GuardianEmailID").value = document.getElementById("MotherEmailID").value;
                var filterdata = $scope.GaurdianTypes.filter(x => x.Value == "Mother");
                var motherTypeID = filterdata[0].Key;
                document.getElementById("GuardianStudentRelationShip").value = motherTypeID;
                document.getElementById(
                    "GuardianSameAsFatherDetails").checked = false;
            }
            else {
                document.getElementById("GuardianFirstName").value = null;
                document.getElementById("GuardianMiddleName").value = null;
                document.getElementById("GuardianLastName").value = null;
                document.getElementById("GuardianNationality").value = null;
                document.getElementById("GuardianOccupation").value = null;
                document.getElementById("GuardianCompanyName").value = null;
                document.getElementById("GuardianPassportNumber").value = null;
                document.getElementById("GuardianCountryofIssue").value = null;
                document.getElementById("GuardianPassportNoIssueString").value = null;
                document.getElementById("GuardianPassportNoExpiryString").value = null;
                document.getElementById("GuardianNationalID").value = null;
                document.getElementById("GuardianNationalIDNoIssueDateString").value = null;
                document.getElementById("GuardianNationalIDNoExpiryDateString").value = null;
                document.getElementById("GuardianMobileNumber").value = null;
                document.getElementById("GuardianEmailID").value = null;
                document.getElementById("GuardianStudentRelationShip").value = null;
            }
        };

        $scope.AutoFillFatherWhatsAppNo = function () {
            if (document.getElementById(
                "FatherWhatsAppNoSameAsMobileNo").checked) {
                document.getElementById("FatherWhatsappMobileNo").value = document.getElementById("MobileNumber").value;
            }
            else {
                document.getElementById("FatherWhatsappMobileNo").value = null;
            }
        };

        $scope.AutoFillMotherWhatsAppNo = function () {
            if (document.getElementById(
                "MotherWhatsAppNoSameAsMobileNo").checked) {
                document.getElementById("MotherWhatsappMobileNo").value = document.getElementById("MotherMobileNumber").value;
            }
            else {
                document.getElementById("MotherWhatsappMobileNo").value = null;
            }
        };

        $scope.AutoFillGuardianWhatsAppNo = function () {
            if (document.getElementById(
                "GuardianWhatsAppNoSameAsMobileNo").checked) {
                document.getElementById("GuardianWhatsappMobileNo").value = document.getElementById("GuardianMobileNumber").value;
            }
            else {
                document.getElementById("GuardianWhatsappMobileNo").value = null;
            }
        };


        function showOverlay() {
            $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
        };

        function hideOverlay() {
            $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
        };

        $scope.ResetFields1 = function () {
            $scope.ResetBirthCertificate = 0;
            $("#ContentFileDownload").hide();
            $("#ContentFileDownload_Trash").hide();
            callToasterPlugin('success', 'Birth Certificate Removed successfully');
        }

        $scope.ResetFields2 = function () {
            $scope.ResetStudentPassportReference = 0;
            $("#StudentPassportDownload").hide();
            $("#StudentPassportDownload_Trash").hide();
            callToasterPlugin('success', 'Student Passport Removed successfully');
        }

        $scope.ResetFields3 = function () {
            $scope.ResetTC = 0;
            $("#TCReferenceDownload").hide();
            $("#TCReferenceDownload_Trash").hide();
            callToasterPlugin('success', 'TC Removed successfully');
        }

        $scope.ResetFields4 = function () {
            $scope.ResetFatherQID = 0;
            $("#FatherQIDReferenceDownload").hide();
            $("#FatherQIDReferenceDownload_Trash").hide();
            callToasterPlugin('success', 'FatherQID Removed successfully');

        }

        $scope.ResetFields5 = function () {
            $scope.ResetMotherQID = 0;
            $("#MotherQIDDownload").hide();
            $("#MotherQIDDownload_Trash").hide();
            callToasterPlugin('success', 'MotherQID Removed successfully');
        }

        $scope.ResetFields6 = function () {
            $scope.ResetStudentQID = 0;
            $("#StudentQIDDownload").hide();
            $("#StudentQIDDownload_Trash").hide();
            callToasterPlugin('success', 'StudentQID Removed successfully');
        }

        $scope.UploadImageFiles = function (uploadfiles, modelName, imageWidth, imageHeight, element) {
            if (!imageWidth) {
                imageWidth = 552;
            }

            if (!imageHeight) {
                imageHeight = 708;
            }

            var file = uploadfiles.files[0];

            if (file) {
                if ($rootScope.cropper) {
                    $rootScope.cropper.destroy();
                }
                $(".dynamicPopoverOverlay").show();
                $("#dynamicPopover").addClass('cropPopup');
                $("#dynamicPopover").show();
                var parentControl = $(element).closest('.controls');
                $rootScope.ShowPopup($(parentControl).find('.croppingImagePopover'), $scope.CrudWindowContainer);

                var newImage = new Image();

                if (/^image\/\w+/.test(file.type)) {
                    newImage.src = URL.createObjectURL(file);
                    $rootScope.cropper = new Cropper(newImage, {
                        dragMode: 'move',
                        aspectRatio: 1 / (imageHeight / imageWidth),
                        viewMode: 1,
                        movable: false,
                        scalable: false,
                        zoomable: true,
                        rounded: true,
                        restore: false,
                        guides: true,
                        center: false,
                        highlight: false,
                        cropBoxMovable: true,
                        cropBoxResizable: false,
                        toggleDragModeOnDblclick: false,
                        fillColor: '#ffffff'
                    });
                }

                /*var content = '<div>Recommended cropping size';*/
                var content = '<div>';
                /*content = content + '<select ng-model="SelectedCropSize" ng-change="ChangeCroppingSize($event,SelectedCropSize)"><option ng-repeat="data in LookUps.CroppingSize" value={{data.Key}}>{{data.Value}}</option></select>';*/
                content = content + '<div class="imageHolder"></div>';
                content = content + "<div class='cropBtn'><button type='button' class='btn btn-primary' onclick='angular.element(this).scope().SaveCroppedImage(\""
                    + file.name + "\",\"" + modelName + "\",\"UserProfile\")'>Upload</button></div>";
                content = content + '</div>';
                $('.dynamicPopoverContainer').html($compile(content)($scope));

                $('.imageHolder', $('.dynamicPopoverContainer')).append(newImage);
            }

            $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
        }

        $rootScope.cropper = null;

        $scope.SaveCroppedImage = function (fileName, modelName, imageType) {
            if ($rootScope.cropper) {
                var xhr = new XMLHttpRequest()
                var fd = new FormData()
                $rootScope.cropper.getCroppedCanvas({
                    fillColor: '#ffffff',
                    width: 276,
                    height: 354
                }).toBlob(function (blob) {
                    fd.append('imageType', imageType);
                    fd.append('png', blob, fileName);
                    xhr.open('POST', utility.myHost + "/Content/UploadImages", true)
                    xhr.onreadystatechange = function (url) {
                        if (xhr.readyState === 4 && xhr.status === 200) {
                            var result = JSON.parse(xhr.response)
                            if (result.Success === true && result.FileInfo.length > 0) {
                                $scope.$apply(function () {
                                    //$scope[modelName] = result.FileInfo[0].FilePath;
                                    $scope.StudentApplication.ProfileUrl = result.FileInfo[0].ContentFileIID;
                                    callToasterPlugin('success', 'Student Profle Image Uploaded successfully');
                                })
                            }
                        }


                    }
                    xhr.send(fd)
                });
            }

            $('#dynamicPopover .close').trigger('click');
        }

        $scope.ClosePopup = function (event) {
            $(event.target).closest('.popupwindow').fadeOut().removeClass('show');
            $('.overlaydiv').removeClass('whitebg').fadeOut();
            $('.popupwindow').removeClass('moveleft');
        }
    }]);
