app.controller("LeadController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    //console.log("LeadController Loaded");
    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });


    $scope.FillLeadData = function (LeadIID) {
        var windowName = 'StudentApplication';
        var viewName = 'Student Application';

        if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
        editUrl = 'Schools/School/FillApplicationFromLead?leadID=' + LeadIID;

        $http({ method: 'Get', url: editUrl })
            .then(function (result) {
                $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
            });
    };

    $scope.ChangeEmailTemplate = function ($event, $element, model) {
        var communicationModel = model;
        if (communicationModel.EmailTemplate == null || communicationModel.EmailTemplate == "") {
            return false;
        }

        var url = "Mutual/GetEmailTemplateByID?TemplateID=" + communicationModel.EmailTemplate;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                communicationModel.EmailContent = result.data.EmailTemplate;

            }, function () {

            });
    };

    $scope.SchoolChanges = function ($event, $element, leadModel) {
        if (leadModel.School == null || leadModel.School == "") return false;
        showOverlay();
        var model = leadModel;
        //var academicYear = model.ContactDetails.AcademicYear?.Value;
        //var className = model.ContactDetails.Class?.Value;
        model.SchoolAcademicyear = null;
        model.ContactDetails.ClassName = null;
        var url = "Schools/School/GetAcademicYearBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.AcademicYear = result.data;
                //model.ContactDetails.AcademicYear = $scope.LookUps.AcademicYear.find(x => x.Value == academicYear).Key;
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        var url = "Schools/School/GetClassesBySchool?schoolID=" + model.School;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.LookUps.Classes = result.data;
                //model.ContactDetails.class = $scope.LookUps.Classes.find(x => x.Value == className);
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.ClassChanges = function ($event, $element, leadModel) {
        if (leadModel.School == null || leadModel.School == "") return false;
        showOverlay();
        var model = leadModel;

        var classId = model.ContactDetails.ClassName;
        var academicYearID = model.ContactDetails.AcademicYear;
        model.AgeCriteriaWarningMsg = null;

        if (academicYearID == undefined || academicYearID == null || academicYearID == "") {
            $().showMessage($scope, $timeout, true, "Please select Academic Year!");
            model.ContactDetails.ClassName = null;
        }
        if (model.ContactDetails.DateOfBirthString == undefined || model.ContactDetails.DateOfBirthString == null || model.ContactDetails.DateOfBirthString == "") {
            $().showMessage($scope, $timeout, true, "Please fill Date of Birth!");
            model.ContactDetails.ClassName = null;
        }
        var url = "Schools/School/GetAgeCriteriaByClassID?classId=" + classId + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result.data != null && result.data.length > 0) {

                    var D1 = result.data[0].BirthFromString;
                    var D2 = result.data[0].BirthToString;
                    var D3 = model.ContactDetails.DateOfBirthString;

                    D1 = Date.parse(D1);
                    D2 = Date.parse(D2);
                    D3 = Date.parse(D3);

                    if (D3 <= D2 && D3 >= D1) {
                        //callToasterPlugin('success', 'DOB is OK for this Class ');
                    }
                    else {
                        model.AgeCriteriaWarningMsg = 'DOB Range for the selected class' /*+ result.data[0].Class.Value*/ + ' is from ' + moment(result.data[0].BirthFrom).format(_dateFormat.toUpperCase()) + ' to ' + moment(result.data[0].BirthTo).format(_dateFormat.toUpperCase());
                       // alert("DOB Range for the class is not in between" + " " + (result.data[0].BirthFromString) + " " + "and" + " " + (result.data[0].BirthToString));
                    }
                    hideOverlay();
                }
                hideOverlay();
            }, function () {
                hideOverlay();
            });


    };

    //$scope.PostApplication = function (row, event, ID, screenID) {
    //        showOverlay();

    //    $.ajax({
    //        type: "POST",
    //        //data: JSON.stringify(communication),
    //        url: utility.myHost + "Schools/School/MoveToApplication?leadID=" + (ID ? ID : row.LeadIID).toString() + "&screenID=" + screenID,
    //        contentType: "application/json;charset=utf-8",
    //        success: function (result) {
    //            if (!result.IsError && result !== null) {
    //                hideOverlay();
    //            }

    //        },
    //        error: function () {

    //        },
    //        complete: function (result) {
    //            hideOverlay();
    //        }
    //    });
    //};

    $scope.LoadCommunication = function (row, event, ID, screenID, container) {
        event.preventDefault();
        $("[data-original-title]").popover('dispose');
        $(".popover", container).hide();

        $(event.currentTarget).popover({
            placement: 'left',
            html: true
        }).on('show.bs.popover', function () {
            $(".overlaydiv").show();
        }).on('hide.bs.popover', function () {
            $(".overlaydiv").hide();
        });

        $(event.currentTarget).popover('show');
        $('#' + $(event.currentTarget).attr('aria-describedby'))
            .find('.popover-body').html('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center>');

        $.ajax({
            url: 'Mutual/Communication?referenceID=' + (ID ? ID : row.LeadIID).toString() + '&screenID=' + screenID,
            type: 'GET',
            success: function (content) {
                $('#' + $(event.currentTarget).attr('aria-describedby')).find('.popover-body').html($compile(content)($scope));
                $timeout(function () {
                    window.dispatchEvent(new Event('resize'));
                });
            }
        });

    };

    $scope.MoveToApplication = function (event) {
        if ($scope.CRUDModel.ViewModel.LeadIID == 0) {
            $().showMessage($scope, $timeout, true, "Move to Admission only applicable after once saved!");
            return false;
        }
        showOverlay();
        $.ajax({
            url: utility.myHost + "Schools/School/MoveToApplicationFromLead",
            type: "POST",
            data: {
                "LeadCode": $scope.CRUDModel.ViewModel.LeadCode,
                "SchoolID": $scope.CRUDModel.ViewModel.SchoolID,
                "ContactDetails.EmailAddress": $scope.CRUDModel.ViewModel.ContactDetails.EmailAddress,
                "ContactDetails.StudentName": $scope.CRUDModel.ViewModel.ContactDetails.StudentName,
                "ContactDetails.MobileNumber": $scope.CRUDModel.ViewModel.ContactDetails.MobileNumber,
                "ContactDetails.ParentName": $scope.CRUDModel.ViewModel.ContactDetails.ParentName,
                "ContactDetails.GenderID": $scope.CRUDModel.ViewModel.ContactDetails.GenderID,
                "ContactDetails.DateOfBirthString": $scope.CRUDModel.ViewModel.ContactDetails.DateOfBirthString,
                "ContactDetails.ClassID": $scope.CRUDModel.ViewModel.ContactDetails.ClassID,
                "ContactDetails.AcademicYearID": $scope.CRUDModel.ViewModel.ContactDetails.AcademicYearID,
                "ContactDetails.CurriculamID": $scope.CRUDModel.ViewModel.ContactDetails.CurriculamID
            },
            success: function (result) {
                if (!result.IsFailed && result != null) {
                    $().showMessage($scope, $timeout, false, result);
                    $scope.CRUDModel.ViewModel.LeadStatusID == 3;
                } 
                else {

                    $().showMessage($scope, $timeout, true, result);

                }
                hideOverlay();
            },
            complete: function (result) {
                //$().showMessage($scope, $timeout, result.IsFailed, result.Message);
            }
        });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);