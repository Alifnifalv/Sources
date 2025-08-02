app.controller('SuperAdminDashboardController', ['$scope', '$http', 'loggedIn', 'rootUrl', '$location', 'GetContext', '$state', '$stateParams', '$sce', '$rootScope', function ($scope, $http, loggedIn, rootUrl, $location, GetContext, $state, $stateParams, $sce, $rootScope) {
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    // $rootScope.ShowLoader = true;
    $rootScope.UserName = context.EmailID;
    $scope.NewAssignmentCount = 0;
    $scope.MyClassCount = 0;
    $scope.LessonPlanCount = 0;
    $scope.NotificationCount = 0;
    
    var securityService = rootUrl.SecurityServiceUrl;
    $rootScope.UserDetails = null;
    $scope.buttonText = "start";
    $scope.src = "./images/right-to-bracket.svg";
    $scope.color = "red";
    $rootScope.Driver = false
    $scope.Teacher = false
    $rootScope.UserClaimSets = context.UserClaimSets;
    $scope.attendanceCardSpinner = true;
       
    if ($rootScope.UserClaimSets.some(code => code.Value === 'Driver')){
        $rootScope.Driver = true
    }
    if ($rootScope.UserClaimSets.some(code => code.Value === 'Class Teacher')){
      $scope.Teacher = true
    }
    

    $scope.init = function () {
       //checklocationService();
     
      $scope.GetTodaysAttendance();

      //Used for showing ID Card when the App Opens 
      // $('#myModal').modal('show'); 


      //   $http({
      //     method: 'GET',
      //     url: securityService + '/GetUserDetails',
      //     data: $scope.user,
      //     headers: { 
      //         "Accept": "application/json;charset=UTF-8", 
      //         "Content-type": "application/json; charset=utf-8", 
      //         "CallContext": JSON.stringify(context) 
      //     }
      // }).success(function (result) {
      //     $rootScope.UserDetails = result;
      //     $rootScope.ShowLoader = false;
      // });

        $http({
            method: 'GET',
            url: dataService + '/GetMyClassCount',
            data: $scope.user,
            headers: { 
                "Accept": "application/json;charset=UTF-8", 
                "Content-type": "application/json; charset=utf-8", 
                "CallContext": JSON.stringify(context) 
            }
        }).success(function (result) {
            $scope.MyClassCount = result;
        })
        .error(function(){
          // $rootScope.ShowLoader = false;
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
            $scope.NewAssignmentCount = result;

            // $rootScope.ShowLoader = false;
        })
        .error(function(){
          // $rootScope.ShowLoader = false;
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
        })
        .error(function(){
          // $rootScope.ShowLoader = false;
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
        })
        .error(function(){
          // $rootScope.ShowLoader = false;
        });

        if ($rootScope.geoLocationInterval != null) {
            clearTimeout($rootScope.geoLocationInterval);
          };
  
          logGeoLocation();
    };
    $scope.changeText = function() {
      
      $scope.buttonText = "Stop";

    $scope.src = "./images/right-from-bracket.svg";
    $scope.color = $scope.color === "red" ? "green" : "red";
     };


    //Used for showing ID Card when the App Opens by using Modal
    const myModal = new bootstrap.Modal(document.getElementById('myModal'))
    
    function checklocationService() {
      cordova.plugins.diagnostic.getLocationMode(
        function (locationMode) {
          switch (locationMode) {
            case cordova.plugins.diagnostic.locationMode.HIGH_ACCURACY:
              console.log("High accuracy");
              break;
            case cordova.plugins.diagnostic.locationMode.BATTERY_SAVING:
              console.log("Battery saving");
              break;
            case cordova.plugins.diagnostic.locationMode.DEVICE_ONLY:
              console.log("Device only");
              cordova.plugins.diagnostic.requestLocationAuthorization(
                function (status) {
                  console.log(status);
                },
                function (error) {
                  console.error(error);
                },
                cordova.plugins.diagnostic.locationAuthorizationMode.WHEN_IN_USE
              );
              // cordova.plugins.diagnostic.isLocationAvailable(successCallback, errorCallback);
              cordova.plugins.diagnostic.isLocationAvailable(
                function (available) {
                  console.log(
                    "Location is " + (available ? "available" : "not available")
                  );
                },
                function (error) {
                  console.error("The following error occurred: " + error);
                }
              );
              break;
            case cordova.plugins.diagnostic.locationMode.LOCATION_OFF:
              console.log("Location off");
              var myModal = new bootstrap.Modal(document.getElementById('exampleModal'))
              myModal.show()
              cordova.plugins.locationAccuracy.canRequest(function(canRequest){
                if(canRequest){
                    cordova.plugins.locationAccuracy.request(function (success){
                        console.log("Successfully requested accuracy: "+success.message);
                    }, function (error){
                       console.error("Accuracy request failed: error code="+error.code+"; error message="+error.message);
                       if(error.code !== cordova.plugins.locationAccuracy.ERROR_USER_DISAGREED){
                           if(window.confirm("Failed to automatically set Location Mode to 'High Accuracy'. Would you like to switch to the Location Settings page and do this manually?")){
                               cordova.plugins.diagnostic.switchToLocationSettings();
                           }
                       }
                    }, cordova.plugins.locationAccuracy.REQUEST_PRIORITY_HIGH_ACCURACY);
                }else{
                    // request location permission and try again
                }
            });
              // cordova.plugins.diagnostic.isLocationAvailable(successCallback, errorCallback);
              cordova.plugins.diagnostic.isLocationAvailable(
                function (available) {
                  console.log(
                    "Location is " + (available ? "available" : "not available")
                  );
                },
                function (error) {
                  console.error("The following error occurred: " + error);
                }
              );
              break;
          }
        },
        function (error) {
          console.error("The following error occurred: " + error);
        }
      );
  
      cordova.plugins.diagnostic.isLocationEnabled(
        function (enabled) {
          console.log(
            "Location setting is " + (enabled ? "enabled" : "disabled")
          );
        },
        function (error) {
          console.error("The following error occurred: " + error);
        }
      );
    }
    function logGeoLocation() {
        if (navigator.geolocation) {
          navigator.geolocation.getCurrentPosition(function (position) {
            var pos = position.coords.latitude + ',' + position.coords.longitude;
            //save employees current location
            $http({
              url: dataService + '/UpdateUserGeoLocation?geoLocation=' + pos,
              method: 'GET',
              headers: {
                Accept: 'application/json;charset=UTF-8',
                'Content-type':
                  'application/json; charset=utf-8',
                CallContext: JSON.stringify(context)
              }
            }).then((result) => {
              $rootScope.geoLocationInterval = setTimeout(function () {
                logGeoLocation();
              }, 50 * 1000);
            }, () => {
              $rootScope.geoLocationInterval = setTimeout(function () {
                logGeoLocation();
              }, 50 * 1000);
            });
          });
        }
      }


  $scope.CloseAttendanceCard = function () {
    document.getElementById("attendanceCard").style.display = "none";
  }

  $scope.GetTodaysAttendance = function () {

    $scope.TodaysAttendanceData = null;
    $scope.attendanceCardSpinner = true;
    // Array of weekdays
    var weekdays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

    $http({
      method: 'GET',
      url: dataService + '/GetTodayStaffAttendanceByLoginID',
      data: $scope.user,
      headers: {
        "Accept": "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).success(function (result) {
      $scope.TodaysAttendanceData = result;

      if($scope.TodaysAttendanceData?.PresentStatus == null || $scope.TodaysAttendanceData?.PresentStatus == ""){
        document.getElementById("attendanceCard").style.display = "none";
        $scope.attendanceCardSpinner = false;
      }

      const timestamp = $scope.TodaysAttendanceData?.AttendenceDate;
      const date = new Date(parseInt(timestamp?.substr(6)));
      const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
      const formattedDate = date.toLocaleDateString('en-US', options);

      $scope.TodaysAttendanceData.AttendenceDate = formattedDate;

      $scope.attendanceCardSpinner = false;
    })
      .error(function () {
        // $rootScope.ShowLoader = false;
        $scope.attendanceCardSpinner = false;
      });
  }


    $scope.init();

}]);