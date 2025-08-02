app.controller('SuperAdminDashboardController', ['$scope', '$http', 'loggedIn', 'rootUrl', 
    '$location', 'GetContext', '$state', '$stateParams', '$sce', '$rootScope', '$q', 'clientSettings',
    function ($scope, $http, loggedIn, rootUrl, $location, GetContext, $state, $stateParams, $sce, $rootScope,
        $q , clientSettings) {
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    $rootScope.ClientSettings = clientSettings;


    $rootScope.ShowLoader = true;
    $rootScope.UserName = context.EmailID;
    $scope.MyWardsCount = 0;
    $scope.NewAssignmentCount = 0;
    $scope.NewAgendaCount= 0;
    $scope.FeeDueAmount = 0;
    $scope.NewCircularCount = 0
    $scope.TotalFee = 0;
    $scope.ExamCount = 0;
    $scope.ApplicationCount = 0;
    $scope.PickupRequestCount = 0;
    $rootScope.ShowFilter = true;
    $rootScope.ShowPreLoader = true;
    $scope.NotificationCount = 0;

    $scope.init = function () {
        $rootScope.ShowLoader = false;
        $q.all([
            // GetMyAssignmentsCount(),
            GetPickupRequestCount(),
            GetPickupRegisterCount(),
            GetMyStudentsSiblingsCount(),
            GetFeeDueAmount(),
            GetLatestCircularCount(),
            StudentApplications(),
            ExamCount(),
            GetMyAgendaCount(),
            GetNotificationCount(),
        ]).then(function () {
            $rootScope.ShowPreLoader = false;
        });
        $('.js-flickity').flickity({
            wrapAround: true,
            contain: true
        });
    };

    // function GetMyAssignmentsCount() {
    //     return $q(function (resolve, reject) {
    //         $http({
    //             method: 'GET',
    //             url: dataService + '/GetMyAssignmentsCount',
    //             data: $scope.user,
    //             headers: { 
    //                 "Accept": "application/json;charset=UTF-8", 
    //                 "Content-type": "application/json; charset=utf-8", 
    //                 "CallContext": JSON.stringify(context) 
    //             }
    //         }).success(function (result) {
    //             $scope.NewAssignmentCount = result;               
    //         });
    //     });
    // }

    function GetPickupRequestCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetPickupRequestsCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.PickupRequestCount = result;               
            });
        });
    }

    function GetPickupRegisterCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetPickupRegisterCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.PickupRegisteredCount = result;               
            });
        });
    }

    function GetMyStudentsSiblingsCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetMyStudentsSiblingsCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.MyWardsCount = result;
                $rootScope.ShowPreLoader = false;
                resolve();
            });
        });
    }

    function GetFeeDueAmount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetFeeDueAmount    ',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.FeeDueAmount = result;               
            });
        });
    }

 
    $scope.today = new Date();
    $scope.todayDay = (new Date()).getDay();

    function GetLatestCircularCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetLatestCircularCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.NewCircularCount = result;
                resolve();
            });
        });
    }
    function StudentApplications() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetStudentApplicationCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.ApplicationCount = result;               
            });
        });
    }

    function GetMyAgendaCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetMyAgendaCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.NewAgendaCount = result;               
            });
        });
    }

    function ExamCount() {
        return $q(function (resolve, reject) {
            $http({
                method: 'GET',
                url: dataService + '/GetExamCount',
                data: $scope.user,
                headers: { 
                    "Accept": "application/json;charset=UTF-8", 
                    "Content-type": "application/json; charset=utf-8", 
                    "CallContext": JSON.stringify(context) 
                }
            }).success(function (result) {
                $scope.ExamCount = result;               
            });
        });
    }

        function GetNotificationCount() {
            return $q(function (resolve, reject) {
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
                })
                    .error(function () {
                        // $rootScope.ShowLoader = false;
                    });
            });
        }

    $scope.init();

}]);