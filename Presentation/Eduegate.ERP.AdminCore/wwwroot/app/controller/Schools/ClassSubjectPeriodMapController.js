app.controller("ClassSubjectPeriodMapController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("ClassSubjectPeriodMapController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.LookUps.Coordinator = null;

    $scope.ClassChanges = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;
        var classID = model.Class.Key;
        var url = "Schools/School/GetSubjectByClass?classID=" + classID;

        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.Subject = result.data;

                $scope.CRUDModel.ViewModel.SubjectMaps = [];

                var serialNo = 1;

                result.data.forEach(x => {
                    var newMap = {
                        PeriodMapDetailIID: null,    
                        SerialNo: serialNo++,        
                        Subject: {
                            Key: x.Key,              
                            Value: x.Value           
                        },
                        SubjectID: x.Key,            
                        WeekPeriods: null,           
                        TotalPeriods: null,          
                        MinimumPeriods: null,        
                        MaximumPeriods: null,        
                        Add: "",                     
                        Remove: ""                   
                    };

                    $scope.CRUDModel.ViewModel.SubjectMaps.push(newMap);
                });

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };



    $scope.FillClassWiseDropDowns = function ($event, $element, crudModel) {
        var model = crudModel;

        $scope.LookUps.Coordinator = [];

        if (crudModel.Section == null || crudModel.Section.Key == 0 || crudModel.Section.Key == "" || crudModel.Section.Key == null) {
            return false;
        }

        if (crudModel.StudentClass == null || crudModel.StudentClass.Key == 0 || crudModel.StudentClass.Key == "" || crudModel.StudentClass.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select class!");
            model.Section = null;
            return false;
        }

        showOverlay();
        var url = "Schools/School/FillClassSectionWiseCoordinators?classID=" + model.StudentClass.Key + "&sectionID=" + model.Section.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                result.data.forEach(x => {
                    $scope.LookUps.Coordinator.push({
                        "Key": x.Coordinator.Key,
                        "Value": x.Coordinator.Value,
                    });
                });
                hideOverlay();
            }, function () {
                hideOverlay();
            });

        $scope.fillSubjectLookUp(model.StudentClass.Key, model.Section.Key);
    };


    $scope.RefreshButtonClicks = function ($event, $element, crudModel) {
        var model = crudModel;

        if (crudModel.Section == null || crudModel.Section.Key == 0 || crudModel.Section.Key == "" || crudModel.Section.Key == null) {
            return false;
        }

        if (crudModel.StudentClass == null || crudModel.StudentClass.Key == 0 || crudModel.StudentClass.Key == "" || crudModel.StudentClass.Key == null) {
            $().showMessage($scope, $timeout, true, "Please select class!");
            model.Section = null;
            return false;
        }

        showOverlay();
        var url = "Schools/School/FillEditDatasAndSubjects?IID=" + model.ClassClassTeacherMapIID + "&classID=" + model.StudentClass.Key + "&sectionID=" + model.Section.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.OtherClassTeachers = result.data;
                $scope.fillSubjectLookUp(model.StudentClass.Key, model.Section.Key, model.ClassClassTeacherMapIID);
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };


    $scope.fillSubjectLookUp = function (classID, sectionID, IID) {
        $scope.LookUps.Subject = [];
        var url = "Schools/School/FillClassWiseSubjects?classID=" + classID + "&sectionID=" + sectionID + "&IID=" + IID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.CRUDModel.ViewModel.OtherClassTeachers = result.data;

                result.data.forEach(x => {
                    $scope.LookUps.Subject.push({
                        "Key": x.Subject.Key,
                        "Value": x.Subject.Value,
                    });
                    $scope.LookUps.DepartmentsTeacher.push({
                        "Key": x.OtherTeacher.Key,
                        "Value": x.OtherTeacher.Value,
                    });
                });
                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }
}]);