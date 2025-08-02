app.controller("ClassSectionKanbanController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", function ($scope, $http, $compile, $window, $timeout, $location, $route) {

    $scope.boards = [];

    $scope.ShiftStudentSection = function (studentID, sectionID) {

        $.ajax({

            url: utility.myHost + "FrameWorks/Kanban/ShiftStudentSection",
            type: "POST",
            data: {
                "studentID": studentID,
                "sectionID": sectionID
            },
            success: function (result) {

                if (result.IsFailed || result != null) {

                    $().showMessage($scope, $timeout, true, result.Message);
                }
            },
            error: function () {
                $().showMessage($scope, $timeout, true, result.Message);
            },
            complete: function (result) {

            }
        });


    };
    $scope.GetStudentsForShifting = function (selected) {
        var selectedSecIds = [];
        if (selected.ngModel.$name == "Class") {
            $scope.selectedClass = selected.selected;
        }
        if (selected.ngModel.$name == "Section") {
            $scope.selectedSection = selected.selected;           
        }
        if (selected.ngModel.$name == "ToSection") {
            $scope.selectedToSection = selected.selected;
            $scope.selectedToSection.forEach(function (item, index) {
                if ($scope.selectedSection?.Key == item.Key) {
                    alert('Section From and To should not be same!');
                    //e.preventDefault();
                    return false;
                }
            });
            if ($scope.selectedSection != null) {
                selectedSecIds.push($scope.selectedSection?.Key);
                $scope.selectedToSection.forEach(function (item, index) {
                    selectedSecIds.push(item.Key);
                });
            }
        }
        var classId = $scope.selectedClass?.Key;
        var sectionId = $scope.selectedSection?.Key;
        if (classId != undefined && sectionId != undefined) {
            $.ajax({
                type: "GET",
                data: { classID: classId, sectionID: sectionId },
                url: "FrameWorks/Kanban/GetStudentsForShifting",
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    $scope.$apply(function () {
                        $scope.boards = [];
                        if (selectedSecIds.length == 0) {
                            $scope.boards = result;
                        }
                        else {
                            $scope.boards = result.filter(function (v) {
                                return selectedSecIds.indexOf(v.id) > -1;
                            });
                        }
                    });

                },
                error: function () {
                    alert(result.Message);
                },
                complete: function (result) {
                    $scope.$apply(function () {
                        $scope.InitializeKanaban();
                    });
                }
            });
        }
    }
    $scope.InitializeKanaban = function () {
        $("#myKanban").html("");
        var KanbanTest = new jKanban({
            element: "#myKanban",
            gutter: "10px",
            widthBoard: "450px",
            itemHandleOptions: {
                enabled: true,
            },
            //fetching student & new section IDs
            dropEl: function (el, target, source, sibling) {
                console.log(target.parentElement.getAttribute('data-id'));
                console.log(el, target, source, sibling)
                //passing student ID & New sectionID to shift
                $scope.ShiftStudentSection(el.attributes["data-eid"].value, target.parentElement.getAttribute('data-id'))
            },
          
            boards: $scope.boards

        });
    }
   
    $scope.Init = function (model, windowname, type) {
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Classes&defaultBlank=false",
        }).then(function (result) {
            $scope.Class = result.data;
        });

        //Sections
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=Section&defaultBlank=false",
        }).then(function (result) {
            $scope.Section = result.data;
            $scope.FromSection = result.data;
            $scope.ToSection = result.data;
        });
        $timeout(function () {
            // $scope.GetStudentsForShifting(); 
        });
    };
}]);

