app.controller('PickerVerificationHomeController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$timeout', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $timeout, $sce) {
  console.log('PickerVerificationHomeController loaded.');

  var dataService = rootUrl.SchoolServiceUrl;
  var context = GetContext.Context();
  $scope.ParentUrlService = rootUrl.ParentUrl;
  $scope.InspectionColor = null;

  $scope.init = function () {
      GetTodayInspectionColour();
      GetTodayStudentPickLogs();
  }

  Fancybox.bind("[data-fancybox]", {
    // Your custom options
  });

  function GetTodayInspectionColour() {
    $http({
      method: "GET",
      url: dataService + "/GetTodayInspectionColour?",
      headers: {
        Accept: "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        CallContext: JSON.stringify(context),
      },
    }).success(function (result) {
      var bgColour = result;

      $scope.InspectionColor = bgColour;
    });
  }

  function GetTodayStudentPickLogs() {

    $http({
      method: "GET",
      url: dataService + "/GetTodayStudentPickLogs?",
      headers: {
        Accept: "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        CallContext: JSON.stringify(context),
      },
    }).success(function (result) {
      $scope.StudentPickLogs = result;
      
      if ($scope.StudentPickLogs.length <= 0) {
        $scope.ShowResultMsg = true;
      }
      $scope.ProceedCount = $scope.StudentPickLogs.filter(x => x.LogStatus == true).length;
      $scope.CancelCount = $scope.StudentPickLogs.filter(x => x.LogStatus == false).length;

    });
  }

  $scope.ScanQRCode = function () {
    window.location.href = '#/studentpickerverification';
  };

}]);