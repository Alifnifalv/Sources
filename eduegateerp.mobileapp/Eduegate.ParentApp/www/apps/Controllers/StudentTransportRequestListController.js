app.controller("StudentTransportRequestListController", [
  "$scope",
  "$http",
  "rootUrl",
  "GetContext",
  "$stateParams",
  "$rootScope",
  "$state",
  "SignalRService",
  "$window",
  function (
    $scope,
    $http,
    rootUrl,
    GetContext,
    $stateParams,
    $rootScope,
    $state,
    SignalRService,
    $window
  ) {
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();

    $scope.init = function () {
      $scope.StudentSubjectList();
    };

    $scope.StudentSubjectList = function (studentID) {
      $scope.Applications = [];

      $http({
        method: "GET",
        url: dataService + "/GetTransportApplication",
        headers: {
          Accept: "application/json;charset=UTF-8",
          "Content-type": "application/json; charset=utf-8",
          CallContext: JSON.stringify(context),
        },
      })
        .success(function (result) {
          // Assign the result to the scope
          $scope.Applications = result;

          $rootScope.ShowLoader = false;
          $rootScope.ShowPreLoader = false;
        })
        .error(function () {
          $rootScope.ShowLoader = false;
        });
    };

    $scope.EditTransportApplicationClick = function (TransportApplctnStudentMapIID) {
      $state.go("studenttransportrequestapplication", {
        TransportApplctnStudentMapIID: TransportApplctnStudentMapIID,
      });
    };
    // Initialize controller
    $scope.init();
  },
]);
