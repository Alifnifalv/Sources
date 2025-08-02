app.controller("MyLessonPlanController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("My Lesson Plan Controller Loaded");
 



    $scope.Init = function (screenType, LessonPlanID) {
        $scope.getLessonPlan(LessonPlanID);
    };

  

    $scope.getLessonPlan = function (LessonPlanIID) {
        //showOverlay();
        $.ajax({
            type: "GET",
            data: { LessonPlanID: LessonPlanIID },
            url: utility.myHost + "Lms/GetLessonPlanByLessonID",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.LessonPlan = result.Response;
                    }
                });
            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    }
    $scope.Init();

}]);