app.controller('StaffTimeTableController', ['$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 'GetContext', '$sce', function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce) {
    console.log('StaffTimeTableController loaded.');
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    $scope.TimeTableDetails = [];
    var loginID = context.LoginID;

    $scope.TimeTableMapping = [];

    $rootScope.ShowLoader = true;

    $scope.DayList = [
        { 'Key':'1' ,'Value': 'Sunday'},
        { 'Key':'2' ,'Value': 'Monday'},
        { 'Key':'3' ,'Value': 'Tuesday'},
        { 'Key':'4' ,'Value': 'Wednesday'},
        { 'Key':'5' ,'Value': 'Thursday'},
    ];

    $scope.init = function() {

        $scope.GetLookUpDatas();
        $scope.loadGlobalTimeTable();
    };

    $scope.GetLookUpDatas = function () {
        $http({
            method: 'Get',
            url: dataService + 'GetDynamicLookUpDataForMobileApp?lookType=TimeTable&defaultBlank=false',
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).then(function (result) {
            $scope.TableMasterData = result.data;
        });

        //Classes
        $http({
            method: 'Get',
            url: dataService + "GetDynamicLookUpDataForMobileApp?lookType=Classes&defaultBlank=false",
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).then(function (result) {
            $scope.Classes = result.data;
        });

        //Sections
        $http({
            method: 'Get',
            url: dataService + "GetDynamicLookUpDataForMobileApp?lookType=AllSectionsFilter&defaultBlank=false",
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).then(function (result) {
            $scope.Sections = result.data;
        });

        //Week Days
        $http({
            method: 'Get',
            url: dataService + "GetDynamicLookUpDataForMobileApp?lookType=WeekDay&defaultBlank=false",
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).then(function (result) {
            $scope.WeekDay = result.data;
        });

        //Class times
        $http({
            method: 'Get',
            url: dataService + "GetDynamicLookUpDataForMobileApp?lookType=ClassTime&defaultBlank=false",
            headers: {
                "Accept": "application/json;charset=UTF-8",
                "Content-type": "application/json; charset=utf-8",
                "CallContext": JSON.stringify(context)
            }
        }).then(function (result) {
            $scope.ClassTime = result.data;
        });
    };

     $scope.GetTimeTableSlot = function (clasTime, day) {
        var slot = null;
        if ($scope.TimeTableMapping.length != 0) {
        var slot = $scope.TimeTableMapping[0].AllocInfoDetails.find(x => x.WeekDayID == day && x.ClassTimingID == clasTime.Key);
        }
        return slot ? slot.Subject.Value : '';
    }

     
    $scope.DayClick = function ($event) {
        $($event.currentTarget).closest('.Week').toggleClass('active');
        // var acc = document.getElementsByClassName("accordion");
        // var i;
        // for (i = 0; i < acc.length; i++) {
        //     acc[i].addEventListener("click", function () {
        //         this.classList.toggle("active");
        //         var panel = this.nextElementSibling;
        //         if (panel.style.maxHeight) {
        //             panel.style.maxHeight = null;
        //         } else {
        //             panel.style.maxHeight = panel.scrollHeight + "px";
        //         }
        //     });
        // }
    }

      //To load class-wise section and saved time tables
      $scope.loadGlobalTimeTable = function () {
          //showOverlay();
          $scope.TimeTableMapping = [];
          $.ajax({
            type: "GET",
            data: { loginID: loginID },
            url: dataService + "/GetTimeTableByStaffLoginID",
            contentType: "application/json;charset=utf-8",
              success: function (result) {
                  if (!result.IsError && result != null) {
    
                      $scope.$apply(function () {
                          $scope.TimeTableMapping = result;
                          //$scope.setDraggable();
                          $rootScope.ShowLoader = false;
    
                      });
                  }
                  $(".gridItemPopup").fadeOut("fast");
                  $(".gridItemOverlay").hide();
              },
              error: function () {
                $rootScope.ShowLoader = false;
              },
              complete: function (result) {
                //   $scope.loadClasswiseSubject();
              }
          });
      };

    $scope.init();
}]);