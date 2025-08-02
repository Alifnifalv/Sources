app.controller('MyWardsController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('My Wards Controller controller loaded.');

    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.DashbaordType = 1;
    $scope.UserName = context.EmailID;
    $scope.WardDetailsHeader = "";
    $scope.WardDetailsTitle = "";
    $scope.showAddBtn = false;

    $scope.TeacherClassDetails = [];
    $scope.SelectedWard = {};

    $scope.MyClassCount = 0;
    $scope.MyAssignmentCount = 0;
    $scope.LessonPlanCount = 0;
    $scope.NotificationCount = 0;
    $scope.NotificationCount = 0;
    $scope.MarkListCount = 0;
    $scope.NewCircularCount = 0;
    $scope.Driver = false
    $scope.Teacher = false


    $rootScope.UserClaimSets = context.UserClaimSets;

       
    if ($rootScope.UserClaimSets.some(code => code.Value === 'Driver')){
        $scope.Driver = true
      }
      if ($rootScope.UserClaimSets.some(code => code.Value === 'Class Teacher')){
        $scope.Teacher = true
      }
      


    $rootScope.ShowLoader = true;

    $scope.Init = function () {

        $http({
            method: 'GET',
            url: dataService + '/GetMyClassCount',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.MyClassCount = result;

            $rootScope.ShowLoader = false;
        }).error(function () {
            $rootScope.ShowLoader = false;
        });

        $http({
            method: 'GET',
            url: dataService + '/GetEmployeeAssignmentsCount',
            data: $scope.user,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.MyAssignmentCount = result;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
        });

        $http({
            method: 'GET',
            url: dataService + '/GetMyLessonPlanCount',
            data: $scope.user,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.LessonPlanCount = result;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
        });

        $http({
            method: 'GET',
            url: dataService + '/GetMyNotificationCount',
            data: $scope.user,
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).success(function (result) {
            $scope.NotificationCount = result;
        }).error(function (err) {
            $rootScope.ShowLoader = false;
        });
    }

    // $scope.LoadTeacherClassInfo = function () {

    // }

    // $scope.SelectWard = function (ward) {
    //     $scope.SelectedWard = ward;
    // }

    // $scope.setMultiColor = function () {
    //     var colorCode = ["#6684fd", "#fc442f", "#ffac15", "#37dc6e", "#6684fd", "#fc442f", "#ffac15"];
    //     var wardListItem = $(".tabScrollItemList ul li span.listWrap");

    //     $.each(wardListItem, function (index) {
    //         var setColor = colorCode[index % wardListItem.length];
    //         $(this).css("color", setColor);
    //     })
    // }

    // $scope.viewDetails = function (event, detailsHeader, title) {
    //     $(".myWardsDetails").fadeIn("fast").addClass('showPanel');
    //     $scope.WardDetailsHeader = detailsHeader;
    //     $scope.WardDetailsTitle = title ?? parameter;
    // }

    // $scope.hideWardDetails = function () {
    //     $(".myWardsDetails").removeClass('showPanel').fadeOut("fast");
    //     $scope.WardDetailsTitle = "";
    //     $scope.WardDetailsHeader = "";
    //     $scope.showAddBtn = false;
    // }
}]);