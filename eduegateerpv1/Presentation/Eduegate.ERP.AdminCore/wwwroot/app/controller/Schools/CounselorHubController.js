app.controller("CounselorHubController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    console.log("CounselorHubController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.StudentName = [];


    $scope.ClassSectionChanges = function ($event, $element, crudModel) {
        showOverlay();
        $scope.LookUps.AllStudents = [];
        var model = crudModel;
        var classList = null;
        var sectionList = null;

        if (model.AcademicYear == null || model.AcademicYear == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please select Academic year !");
            hideOverlay();
            return false;
        }

        if (model.Class == null || model.Class == undefined || model.Class.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please select class !");
            model.IsSelected = false;

            $http({
                method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=AllClass&defaultBlank=false'
            }).then(function (result) {
                $scope.LookUps.AllClass = result.data;
            });

            hideOverlay();
            return false;
        }

        if (model.Section == null || model.Section == undefined || model.Section.length == 0) {
            $().showGlobalMessage($root, $timeout, true, "Please select section !");
            model.IsSelected = false;

            $http({
                method: 'Get', url: 'Mutual/GetDynamicLookUpData?lookType=AllSection&defaultBlank=false'
            }).then(function (result) {
                $scope.LookUps.AllSection = result.data;
            });

            hideOverlay();
            return false;
        }

        if (model.Class != null && model.Class != undefined) {
            if (model.Class[0].Value == 'All Classes') {
                classList = model.Class.map(function (item) {
                    $scope.LookUps.AllClass = null;
                    return 0;
                });
            }
            else {
                classList = model.Class.map(function (item) {
                    return item.Key;
                });
            }
        }

        if (model.Section != null && model.Section != undefined) {

            if (!classList) {
                $().showGlobalMessage($root, $timeout, true, "Please select class !");
                hideOverlay();
                return false;
            };

            if (model.Section[0].Value == 'All Section') {
                sectionList = model.Section.map(function (item) {
                    $scope.LookUps.AllSection = null;
                    return 0;
                });
            } else {
                sectionList = model.Section.map(function (item) {
                    return item.Key;
                });
            }
        }

        if (classList[0] != 0 || sectionList[0] != 0) {
            var url = "Schools/School/GetClassStudentsAll?academicYearID=" + model.AcademicYear.Key + "&";
            if (classList && classList.length > 0) {
                url += "classList=" + classList.join('&classList=');
            }
            if (sectionList && sectionList.length > 0) {
                url += "&sectionList=" + sectionList.join('&sectionList=');
            }

            $.ajax({
                url: url,
                type: "GET",
                contentType: "application/json",
                success: function (result) {
                    result.unshift({
                        "Key": "0",
                        "Value": "All Students",
                    });
                    $scope.LookUps.AllStudents = result;

                    model.Fill = false;
                    model.IsSelected = false;

                    hideOverlay();
                },
                error: function () {
                    hideOverlay();
                }
            });
        }
        else {
            $scope.LookUps.AllStudents.push({
                "Key": "0",
                "Value": "All Students",
            });

            model.Fill = false;
            model.IsSelected = false;

            hideOverlay();
        }
    };
    $scope.StudentChanges = function ($event, $element, crudModel) {
        showOverlay();

        var model = crudModel;
        model.StudentList = [];

        if (model.Student != null && model.Student != undefined && model.Student.length > 0) {
            model.IsSelected = true;
            model.Student.forEach(function (stud) {
                model.StudentList.push({
                    "StudentID": stud.Key,
                    "StudentName": stud.Value,
                });
            });
        }
        else {
            model.IsSelected = false;
        }

        hideOverlay();
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);