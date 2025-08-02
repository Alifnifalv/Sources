var app = angular.module('Eduegate.ERP.School.Portal',
    []);


app.controller("OnlineEnquiryController", ["$scope", "$http", function ($scope, $http) {

    //var specialKeys = new Array();
    //specialKeys.push(8); //Backspace
    $scope.apiurl = 'http://localhost/Eduegate.PublicAPI/api/CRM/';
    $http.get($scope.apiurl + "GetDeafaultSchool").then(function (res) {
        $scope.school = res.data
    });
    $scope.school = 30;
    $http.get($scope.apiurl + "GetAcademicYear?school=" + $scope.school).then(function (res) {
        $scope.academicYears = res.data
    });

    $http.get($scope.apiurl + "GetClasses?school=" + $scope.school).then(function (res) {
        $scope.grades = res.data
    });
   
    //$http.get($scope.apiurl + "GetAcademicYear?school=30").then(function (res) {
    //    $scope.academicYears = res.data
    //});

    //$http.get($scope.apiurl + "GetClasses?school=30").then(function (res) {
    //    $scope.grades = res.data
    //});
    $http.get($scope.apiurl + "GetLeadSource").then(function (res) {
        $scope.about_lst = res.data

    });

    $scope.init = function (model) {
        windowContainer = '#' + windowName;
        $scope.PasswordResetModel = passwordResetModel;
        $scope.Message = "";
        $scope.MessageType = null;
        $scope.ShowMessage = false;
        $scope.apiurl = 'http://localhost/Eduegate.PublicAPI/api/CRM/';


        $http.get($scope.apiurl + "GetDeafaultSchool").then(function (res) {
            $scope.school = res.data
        });

        $http.get($scope.apiurl + "GetAcademicYear?school=" + 30).then(function (res) {
            $scope.academicYears = res.data
        });

        $http.get($scope.apiurl + "GetClasses?school=" + 30).then(function (res) {
            $scope.grades = res.data
        });




        $http.get($scope.apiurl + "GetLeadSource").then(function (res) {
            $scope.about_lst = res.data

        });


        $scope.schoolUrl = window.location.href;
        var conStr = $scope.schoolUrl.substring($scope.schoolUrl.indexOf('/') + 2, $scope.schoolUrl.indexOf('.'));
        if (conStr == 'www' || conStr == 'WWW') {
            $scope.schoolUrl = window.location.href;

            var conStr = $scope.schoolUrl.split('.')[1];
            tp.defaults.headers.common['schoolId'] = conStr;

        }
        else {
            $http.defaults.headers.common['schoolId'] = 'pearl';
        }
        //   $http.defaults.headers.common['schoolId']='asd'

        $scope.edt = {};
        //jQuery('*[data-datepicker="true"] input[type="text"]').datepicker({
        //    todayBtn: true,
        //    orientation: "top left",
        //    autoclose: true,
        //    todayHighlight: true,

        //});
        //jQuery(document).on('touch click', '*[data-datepicker="true"] .add-on', function (e) {
        //    jQuery('input[type="text"]', jQuery(this).parent()).focus();
        //});

    }

    //$scope.IsNumeric = function (e) {
    //    var keyCode = e.which ? e.which : e.keyCode
    //    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);

    //    return ret;
    //}

    $scope.RegisterUser = function (edt) {
        //  $scope.getPassword()

        $scope.sendData = [];
        console.log(edt);
        console.log($scope.password);;

        var day = edt.dob1.split('-')[0];
        var month = edt.dob1.split('-')[1];
        var year = edt.dob1.split('-')[2];

        edt.dob = year + '-' + month + '-' + day;
        // edt.password = $scope.password;

        var data = {
            'Dob1': edt.dob, 'Sname': edt.sname, 'Referal_code': edt.referal_code.Key,
            'Email': edt.email, 'Mobile': edt.mobile, 'Pname': edt.pname, 'Grade': edt.grade.Key,
            'Academic_year': edt.academic_year.Key, 'Gender': edt.gender, "SchoolID": $scope.school, 'Nationality': edt.nationality.Key,
        };
        //var data = {
        //     'Sname': edt.sname
        //};
        $http.post($scope.apiurl + "AddLead", data)
            .then(function (data) {
                $scope.p_data = data;

                if ($scope.p_data) {
                    // swal({ text: " Sucessfully" });
                    $scope.edt = {}
                    swal({ title: "Alert", text: " Registered Successfully", imageUrl: "assets/img/check.png", });

                } else {
                    // swal({ text: "Not Registered" });
                    swal({ title: "Alert", text: " Not Registered", imageUrl: "assets/img/notification-alert.png", });

                }

            });
    }

    //$scope.datechange = function (str) {
    //    var day = str.split('/')[1];
    //    var month = str.split('/')[0];
    //    var year = str.split('/')[2];
    //    $scope.edt['dob1'] = day + '-' + month + '-' + year;

    //}



}]);