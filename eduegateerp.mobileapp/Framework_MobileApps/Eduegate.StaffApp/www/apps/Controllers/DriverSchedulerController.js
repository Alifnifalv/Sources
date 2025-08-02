app.controller("DriverSchedulerController", [
  "$scope",
  "$http",
  "$state",
  "rootUrl",
  "$location",
  "$rootScope",
  "$stateParams",
  "GetContext",
  "$sce",
  "$timeout",
  "offlineSync",
  function (
    $scope,
    $http,
    $state,
    rootUrl,
    $location,
    $rootScope,
    $stateParams,
    GetContext,
    $sce,
    $timeout,
    $offlineSync
  ) {
    console.log("DriverSchedulerController loaded.");
    var dataService = rootUrl.SchoolServiceUrl;
    var context = GetContext.Context();
    $scope.ParentUrlService = rootUrl.ParentUrl;
    $scope.ScheduleDetails = [];
    $scope.CurrentDate = new Date();
    $scope.CurrentDateMonth = new Date().getMonth();
    $scope.CurrentDateYear = new Date().getFullYear();
    $scope.CurrentDateString = $scope.CurrentDate.toDateString();
    $scope.IsStudentDetails = true;
    $scope.IsStaffDetails = true;
    $scope.StudentPickoutSheduleCount = 0;
    $scope.StaffPickoutSheduleCount = 0;
    $rootScope.ShowLoader = true;
    $scope.SheduleType = "PickupIn";
    $scope.IsAutoInOrOut = true;
    $scope.SearchText = null;
    $scope.OfflineSupport = true;

    $scope.PickupInTotalCount = 0;
    $scope.PickupInCount = 0;
    $scope.PickupNotInCount = 0;

    $scope.PickupOutTotalCount = 0;
    $scope.PickupOutCount = 0;
    $scope.PickupNotOutCount = 0;

    $scope.DropTotalCount = 0;
    $scope.DropInCount = 0;
    $scope.DropNotInCount = 0;

    $scope.DropOutTotalCount = 0;
    $scope.DropOutCount = 0;
    $scope.DropNotOutCount = 0;

    $scope.init = function () {
      $scope.IsStudentDetails = true;
      $scope.IsStaffDetails = true;
      $scope.ClearSearchDataLists();
    };
    $scope.GetStudentImage = function (studentId,fileName) {
      //return rootUrl.ImageUrl + imagePath;
      return rootUrl.ErpUrl+"UploadImages/StudentProfile/" + studentId +"/Thumbnail/"+ fileName;
      
  }
    $scope.LoadScheduleInfo = function () {


      $scope.SyncButtonName = "Offline Data sync";

      if (!$rootScope.IsOnline && $rootScope.ClientSettings.IsOfflineSupport) {
        $offlineSync
          .GetStudentsAndStaffScheduleDetails("ALL")
          .then(function (data) {
            $scope.ScheduleDetails = data;
            if ($scope.ScheduleDetails.length > 0) {
              $scope.GetInOutDetailCount();
            }
            else { $scope.AlertText = "Offline Data is not available!" }
            $(".loader-blk").hide();
          });
        $rootScope.ShowLoader = false;
        return;
      }
      if ($rootScope.IsOnline) {
        // if ($scope.OfflineSupport == true) {
        //   $offlineSync.SyncLiveDB();
        // }
        $http({
          method: "GET",
          url: dataService + "/GetRouteStudentsAndStaffDetailsByEmployeeLoginID",
          headers: {
            Accept: "application/json;charset=UTF-8",
            "Content-type": "application/json; charset=utf-8",
            CallContext: JSON.stringify(context),
          },
        })
          .success(function (result) {
            $scope.ScheduleDetails = result;
            $rootScope.ShowLoader = false;

            // const falseNb = $scope.ScheduleDetails.length;
            // console.log(falseNb);

            if ($scope.ScheduleDetails.length > 0) {
              if ($rootScope.ClientSettings.IsOfflineSupport) {
                $offlineSync.Sync();
              }
              $scope.GetInOutDetailCount();
            }
            else { $scope.AlertText = "Data is not available!" }
          })
          .error(function () {
            $rootScope.ShowLoader = false;
          });
      }
    };

    $scope.showRightPanel = function ($event, tabName, headID, activity) {
      if (activity) {
        $scope.SelectedActivity = activity;
      }

      $(".rightSidebarTabData").hide();
      $('.rightSidebarTabData[data-tab="' + tabName + '"]').show();
      $(".rightsideBarOverlay").show();
      $(".rightsideBar").addClass("active");
    };

    $scope.hideRightPanel = function () {
      $(".rightsideBarOverlay").hide();
      $(".rightsideBar").removeClass("active");
    };

    $scope.GetDetails = function (type, $event) {
      $($event.currentTarget).closest("ul").find("li").removeClass("active");
      $($event.currentTarget).closest("li").toggleClass("active");
    };

    $scope.GetDetailCount = function (model, type) {
      var count = 0;

      if (type == "StudentPickupStop") {
        if (model.StopsStudentDetails.length != 0) {
          count = model.StopsStudentDetails.filter(
            (x) => x.IsPickupStop == true
          ).length;
        }
        return count;
      }

      if (type == "StudentDropStop") {
        if (model.StopsStudentDetails.length != 0) {
          count = model.StopsStudentDetails.filter(
            (x) => x.IsDropStop == true
          ).length;
        }
        return count;
      }

      if (type == "StaffPickupStop") {
        if (model.StopsStaffDetails.length != 0) {
          count = model.StopsStaffDetails.filter(
            (x) => x.IsPickupStop == true
          ).length;
        }
        return count;
      }

      if (type == "StaffDropStop") {
        if (model.StopsStaffDetails.length != 0) {
          count = model.StopsStaffDetails.filter(
            (x) => x.IsDropStop == true
          ).length;
        }
        return count;
      }
    };

    $scope.GetDetailCountByRoute = function (model, type) {
      var count = 0;

      // if (type == 'StudentPickupStop') {
      //     if (model.Stops.length != 0) {
      //         model.Stops.forEach(x =>
      //             count = count + x.StopsStudentDetails.filter(y => y.IsPickupStop == true).length,
      //         )
      //     }
      //     return count;
      // }

      if (type == "StudentDropStop") {
        if (model.Stops != undefined && model.Stops.length != 0) {
          model.Stops.forEach(
            (x) =>
            (count =
              count +
              x.StopsStudentDetails.filter((y) => y.IsDropStop == true)
                .length)
          );
        }
        return count;
      }

      // if (type == 'StaffPickupStop') {
      //     if (model.Stops.length != 0) {
      //         model.Stops.forEach(x =>
      //             count = count + x.StopsStaffDetails.filter(y => y.IsPickupStop == true).length,
      //         )
      //     }
      //     return count;
      // }

      if (type == "StaffDropStop") {
        if (model.Stops.length != 0) {
          model.Stops.forEach(
            (x) =>
            (count =
              count +
              x.StopsStaffDetails.filter((y) => y.IsDropStop == true).length)
          );
        }
        return count;
      }
    };


    $scope.GetStopCount = function (stop) {
      var count = 0;
      var DataList = [];

      var staffOutputArray = [];
      var studentOutputArray = [];

      //Get distinct data of stop's staff data start
      if (stop.StopsStaffDetails.length > 0) {
        var distinctStaffStopList = [];
        var start = false;

        for (j = 0; j < stop.StopsStaffDetails.length; j++) {
          for (k = 0; k < distinctStaffStopList.length; k++) {
            if (
              stop.StopsStaffDetails[j].StaffID ==
              distinctStaffStopList[k].StaffID
            ) {
              start = true;
            }
          }
          count++;
          if (count == 1 && start == false) {
            distinctStaffStopList.push(stop.StopsStaffDetails[j]);
          }
          start = false;
          count = 0;
        }

        distinctStaffStopList.forEach((x) => {
          if (x.StaffID != null && x.IsPickupStop == true) {
            staffOutputArray.push({
              Key: x.StaffID,
              Value: x.StaffName,
            });
          }
        });
      }
      //Get distinct data of stop's staff data end

      //Get distinct data of stop's student data start
      if (stop.StopsStudentDetails.length > 0) {
        var distinctStudentStopList = [];
        var start = false;

        for (j = 0; j < stop.StopsStudentDetails.length; j++) {
          for (k = 0; k < distinctStudentStopList.length; k++) {
            if (
              stop.StopsStudentDetails[j].StudentID ==
              distinctStudentStopList[k].StudentID
            ) {
              start = true;
            }
          }
          count++;
          if (count == 1 && start == false) {
            distinctStudentStopList.push(stop.StopsStudentDetails[j]);
          }
          start = false;
          count = 0;
        }

        distinctStudentStopList.forEach((x) => {
          if (x.StudentID != null && x.IsPickupStop == true) {
            studentOutputArray.push({
              Key: x.StudentID,
              Value: x.StudentName,
            });
          }
        });
      }
      //Get distinct data of stop's student data start

      count = staffOutputArray.length + studentOutputArray.length;

      return count;
    };

    $scope.ChangeStatus = function (
      type,
      scheduleDetails,
      routeDetails,
      stopDetails,
      passengerDetails,
      passengerType,
      status
    ) {
      var scheduleLogData = {};

      if (passengerType == "Student") {
        if (status == true) {
          if (passengerDetails.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: passengerDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 1, //For pickup status
              Status: "I",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
          if (passengerDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: passengerDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "I",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
        } else {
          if (passengerDetails.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: passengerDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "A",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
          if (passengerDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: passengerDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
        }
      }

      if (passengerType == "Employee") {
        if (status == true) {

          // if (passengerDetails.CurrentStatus == "A" && passengerDetails.ScheduleLogType == "DROP-IN")
          // {
          // var statusChangeVal = "I";
          // }
          // else
          // {
          //   var statusChangeVal = "A";
          // }


          if (passengerDetails.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: passengerDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 1, //For pickup status
              Status: "I",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
          if (passengerDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: passengerDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "I",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
        } else {
          if (passengerDetails.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: passengerDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "A",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
          if (passengerDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: passengerDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: passengerDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: scheduleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: passengerDetails.ScheduleLogType,
            };
          }
        }
      }

      var syncStatus = false;
      var IsStdtIn = false;
      var IsStfIn = false;

      if (scheduleLogData != "O") {
        if (passengerType == "Employee") {
          IsStdtIn = false;
          IsStfIn = true;
        }
        else {
          IsStdtIn = true;
          IsStfIn = false;
        }
      }
      else {
        IsStdtIn = false;
        IsStfIn = false;
      }

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, passengerDetails); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStdtIn, IsStfIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline
      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            passengerDetails.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, passengerDetails, passengerDetails.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }
    };

    //call After Save for offline
    AutoInOutAfterSaveOffline = function (scheduleLogData, passengerDetails, result) {
      $rootScope.ShowLoader = false;
      $scope.LoadingSpinner = false;

      //passing status for count change in PICK-OUT (non-filter search part)
      //passengerDetails.Status = result;
      if (passengerDetails.StudentID != null && passengerDetails.StudentRouteStopMap != undefined) {
        passengerDetails.StudentRouteStopMap.Status = result;
      }
      else if (passengerDetails.StaffID != null && passengerDetails.StaffRouteStopMap != undefined) {
        passengerDetails.StaffRouteStopMap.Status = result;
      }


      if ($scope.IsAutoInOrOut == true) {

        if (result == "I") {
          $rootScope.ToastMessage = "Successfully In"
          $('.toast').toast('show');
        }
        else if (result == "O") {
          $rootScope.ToastMessage = "Successfully Out"
          $('.toast').toast('show');
        }

        //passing status to filtersearch data for pickup-IN
        if (passengerDetails.ScheduleLogType == "PICK-IN" && $scope.IsAutoInOrOut == true && result == "I") {
          if (passengerDetails.StudentID != null && $scope.FilterScheduleStudentPickInDetails != undefined && $scope.FilterScheduleStudentPickInDetails.length > 0) {
            $scope.FilterScheduleStudentPickInDetails[0].IsStudentIn = true;
          }
          if (passengerDetails.StudentID == null && $scope.FilterScheduleStaffPickInDetails.length > 0) {
            $scope.FilterScheduleStaffPickInDetails[0].IsStaffIn = true;
          }
        }

        //passing status to filtersearch data for pickup-OUT
        if (passengerDetails.ScheduleLogType == "PICK-OUT" && $scope.IsAutoInOrOut == true && result == "O") {
          if (passengerDetails.StudentID != null && $scope.FilterScheduleStudentPickOutDetails != undefined && $scope.FilterScheduleStudentPickOutDetails.length > 0) {
            $scope.FilterScheduleStudentPickOutDetails[0].IsStudentIn = false;
            var fiteredPickupList = $scope.StudentPickupSheduledList.filter(x => x.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID)
            if (fiteredPickupList != null) {
              if (fiteredPickupList.length == 1) {
                fiteredPickupList[0].StudentRouteStopMap.IsStudentIn = false;
                fiteredPickupList[0].StudentRouteStopMap.Status = result;
              }
            }
          }
          if (passengerDetails.StudentID == null && $scope.FilterScheduleStaffPickOutDetails.length > 0) {
            $scope.FilterScheduleStaffPickOutDetails[0].IsStaffIn = false;
            var fiteredPickupList = $scope.StaffPickupSheduledList.filter(x => x.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID)
            if (fiteredPickupList != null) {
              if (fiteredPickupList.length == 1) {
                fiteredPickupList[0].StaffRouteStopMap.IsStaffIn = false;
                fiteredPickupList[0].StaffRouteStopMap.Status = result;
              }
            }
          }
        }

        //passing status to filtersearch data for DROPIN
        if (passengerDetails.ScheduleLogType == "DROP-IN" && $scope.IsAutoInOrOut == true && result == "I") {
          if (passengerDetails.StudentID != null && $scope.FilterScheduleStudentDropInDetails.length > 0) {
            $scope.FilterScheduleStudentDropInDetails[0].IsStudentIn = true;
          }
          if (passengerDetails.StudentID == null && $scope.FilterScheduleStaffDropInDetails.length > 0) {
            $scope.FilterScheduleStaffDropInDetails[0].IsStaffIn = true;
          }
        }

        //passing status to filtersearch data for DROP-OUT
        if (passengerDetails.ScheduleLogType == "DROP-OUT" && $scope.IsAutoInOrOut == true && result == "O") {
          if (passengerDetails.StudentID != null && $scope.FilterScheduleStudentDropOutDetails.length > 0) {
            $scope.FilterScheduleStudentDropOutDetails[0].IsStudentIn = false;
            $scope.StudentStaffDropOutStopWiseDetails.Stops.forEach(z => {

              if (z.StopsStudentDetails.length > 0) {
                var studentData = z.StopsStudentDetails.filter(s => s.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID);
                if (studentData.length > 0) {
                  studentData[0].IsStudentIn = false;
                  studentData[0].Status = result;
                }
              }
            })
          }
          if (passengerDetails.StudentID == null && $scope.StudentStaffDropOutStopWiseDetails.length > 0) {
            $scope.FilterScheduleStaffDropOutDetails[0].IsStaffIn = false;
            $scope.StudentStaffDropOutStopWiseDetails.Stops.forEach(z => {

              if (z.StopsStaffDetails.length > 0) {
                var staffData = z.StopsStaffDetails.filter(s => s.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID);
                if (staffData.length > 0) {
                  staffData[0].IsStaffIn = false;
                  staffData[0].Status = result;
                }
              }
            })
          }
        }
      }
      $scope.GetInOutDetailCount();
    };

    $scope.SaveScheduleData = function (scheduleLogData, passengerDetails) {
      $http({
        method: "POST",
        url: dataService + "/SaveStudentAndStaffScheduleLogs",
        data: scheduleLogData,
        headers: {
          Accept: "application/json;charset=UTF-8",
          "Content-type": "application/json; charset=utf-8",
          CallContext: JSON.stringify(context),
        },
      })
        .success(function (result) {
          //$timeout(function () {
          //$scope.$apply(function () {
          passengerDetails.Status = result;
          // $scope.GetInOutDetailCount();
          //passengerDetails.DriverScheduleLogIID = result;
          //});
          //});
          $rootScope.ShowLoader = false;
          $scope.LoadingSpinner = false;

          //passing status for count change in PICK-OUT (non-filter search part)
          if (passengerDetails.StudentID != null && passengerDetails.StudentRouteStopMap != undefined) {
            passengerDetails.StudentRouteStopMap.Status = result;
          }
          else if (passengerDetails.StaffID != null && passengerDetails.StaffRouteStopMap != undefined) {
            passengerDetails.StaffRouteStopMap.Status = result;
          }


          if ($scope.IsAutoInOrOut == true) {

            if (result == "I") {
              $rootScope.ToastMessage = "Successfully In"
              $('.toast').toast('show');
            }
            else if (result == "O") {
              $rootScope.ToastMessage = "Successfully Out"
              $('.toast').toast('show');
            }

            //passing status to filtersearch data for pickup-IN
            if (passengerDetails.ScheduleLogType == "PICK-IN" && $scope.IsAutoInOrOut == true && result == "I") {
              if (passengerDetails.StudentID != null) {
                if ($scope.FilterScheduleStudentPickInDetails != undefined && $scope.FilterScheduleStudentPickInDetails.length > 0) {
                  $scope.FilterScheduleStudentPickInDetails[0].IsStudentIn = true;
                }
              }
              else {
                if ($scope.FilterScheduleStaffPickInDetails != undefined && $scope.FilterScheduleStaffPickInDetails.length > 0) {
                  $scope.FilterScheduleStaffPickInDetails[0].IsStaffIn = true;
                }
              }
            }

            //passing status to filtersearch data for pickup-OUT
            if (passengerDetails.ScheduleLogType == "PICK-OUT" && $scope.IsAutoInOrOut == true && result == "O") {
              if (passengerDetails.StudentID != null) {
                if($scope.FilterScheduleStudentPickOutDetails.length > 0)
                {
                  $scope.FilterScheduleStudentPickOutDetails[0].IsStudentIn = false;
                  var fiteredPickupList = $scope.StudentPickupSheduledList.filter(x => x.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID)
                  if (fiteredPickupList != null) {
                    if (fiteredPickupList.length == 1) {
                      fiteredPickupList[0].StudentRouteStopMap.IsStudentIn = false;
                      fiteredPickupList[0].StudentRouteStopMap.Status = result;
                    }
                  }
                }
              }
              else {
                if ($scope.FilterScheduleStaffPickOutDetails.length > 0) {
                  $scope.FilterScheduleStaffPickOutDetails[0].IsStaffIn = false;
                  var fiteredPickupList = $scope.StaffPickupSheduledList.filter(x => x.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID)
                  if (fiteredPickupList != null) {
                    if (fiteredPickupList.length == 1) {
                      fiteredPickupList[0].StaffRouteStopMap.IsStaffIn = false;
                      fiteredPickupList[0].StaffRouteStopMap.Status = result;
                    }
                  }
                }
              }
            }


            //passing status to filtersearch data for DROPIN
            if (passengerDetails.ScheduleLogType == "DROP-IN" && $scope.IsAutoInOrOut == true && result == "I") {
              if (passengerDetails.StudentID != null) {
                if ($scope.FilterScheduleStudentDropInDetails.length > 0)
                {
                  $scope.FilterScheduleStudentDropInDetails[0].IsStudentIn = true;
                }
              }
              else {
                if ($scope.FilterScheduleStaffDropInDetails.length > 0)
                {
                  $scope.FilterScheduleStaffDropInDetails[0].IsStaffIn = true;
                }
              }
            }

            //passing status to filtersearch data for DROP-OUT
            if (passengerDetails.ScheduleLogType == "DROP-OUT" && $scope.IsAutoInOrOut == true && result == "O") {
              if (passengerDetails.StudentID != null) {
                if($scope.FilterScheduleStudentDropOutDetails.length > 0)
                {
                $scope.FilterScheduleStudentDropOutDetails[0].IsStudentIn = false;
                }
                $scope.StudentStaffDropOutStopWiseDetails.Stops.forEach(z => {

                  if (z.StopsStudentDetails.length > 0) {
                    var studentData = z.StopsStudentDetails.filter(s => s.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID);
                    if (studentData.length > 0) {
                      studentData[0].IsStudentIn = false;
                      studentData[0].Status = result;
                    }
                  }
                })
              }
              else {
                if($scope.FilterScheduleStaffDropOutDetails.length > 0)
                {
                  $scope.FilterScheduleStaffDropOutDetails[0].IsStaffIn = false;
                }

                $scope.StudentStaffDropOutStopWiseDetails.Stops.forEach(z => {

                  if (z.StopsStaffDetails.length > 0) {
                    var staffData = z.StopsStaffDetails.filter(s => s.DriverScheduleLogIID == passengerDetails.DriverScheduleLogIID);
                    if (staffData.length > 0) {
                      staffData[0].IsStaffIn = false;
                      staffData[0].Status = result;
                    }
                  }
                })
              }
            }
          }
          $scope.GetInOutDetailCount();
        })
        .error(function (err) {
          $rootScope.ToastMessage = err.ErrorMessage;
          //  "Something went wrong, please try again later!";
          $rootScope.ShowLoader = false;
        });
    };

    // $scope.SyncLiveDB = function () {
    //   var syncScheduleLogData = [];
    //   $offlineSync.GetDataToSync("ALL").then(function (data) {
    //     syncScheduleLogData = data;
    //   });

    //   $http({
    //     method: "POST",
    //     url: dataService + "/SyncDriverScheduleLogs",
    //     data: syncScheduleLogData,
    //     headers: {
    //       Accept: "application/json;charset=UTF-8",
    //       "Content-type": "application/json; charset=utf-8",
    //       CallContext: JSON.stringify(context),
    //     },
    //   })
    //     .success(function (result) {
    //       $timeout(function () {
    //         $scope.$apply(function () {
    //           passengerDetails.DriverScheduleLogIID = result;
    //         });
    //       });

    //       $rootScope.ShowLoader = false;
    //       $scope.LoadingSpinner = false;
    //     })
    //     .error(function (err) {
    //       $rootScope.ErrorMessage =
    //         "Something went wrong, please try again later!";
    //       $rootScope.ShowLoader = false;
    //     });
    // };

    $scope.GetScheduledDatasByRoute = function (
      type,
      scheduleDetails,
      routeDetails
    ) {

      var vehicleID = null;
      var routeID = null;

      vehicleID = scheduleDetails.VehicleIID;
      routeID = routeDetails.RouteID;

      var passengerType = "";

      if ($scope.IsStudentDetails == true) {
        passengerType = "Student";
      }
      if ($scope.IsStaffDetails == true) {
        if (passengerType == "") {
          passengerType = "Staff";
        } else {
          passengerType = passengerType + ",Staff";
        }
      }
      if (type == "IsPickup") {
        $scope.RouteStopTypeChange("PickupOut");
      }

      $scope.StudentPickupSheduledList = [];
      $scope.StaffDropOutSheduledList = [];


      if ($rootScope.IsOnline) {
        //if (passengerType == "Student") {
        if (type == "IsPickup") {
          $scope.RouteStopTypeChange("PickupOut");

          $scope.StudentPickupSheduledList = [];

          $http({
            method: "GET",
            url:
              dataService +
              "/GetScheduleLogsByRoute?scheduleType=" +
              type +
              "&passengerType=" +
              passengerType +
              "&vehicleID=" +
              vehicleID +
              "&routeID=" +
              routeID,
            headers: {
              Accept: "application/json;charset=UTF-8",
              "Content-type": "application/json; charset=utf-8",
              CallContext: JSON.stringify(context),
            },
          })
            .success(function (result) {

              if (type == "IsPickup") {
                $scope.StudentPickupSheduledList = result.filter(x => x.StudentID != null && x.ScheduleLogType == 'PICK-OUT');
                $scope.StaffPickupSheduledList = result.filter(x => x.EmployeeID != null && x.ScheduleLogType == 'PICK-OUT');

                $scope.GetInOutDetailCount();

                if ($scope.StudentPickupSheduledList.length > 0) {
                  $scope.StudentPickoutSheduleCount =
                    $scope.StudentPickupSheduledList.length;
                }

                if ($scope.StaffPickupSheduledList.length > 0) {
                  $scope.StaffPickoutSheduleCount =
                    $scope.StaffPickupSheduledList.length;
                }
              }
            })
            .error(function () { });
        }

        if (type == "IsDrop") {
          $scope.RouteStopTypeChange("DropOut");
          $scope.StudentStaffDropOutStopWiseDetails = null;
          // $scope.StudentDropShedule = null;
          // $scope.StaffDropOutList = null;

          $http({
            method: "GET",
            url:
              dataService +
              "/GetStudentStaffDropScheduleDatasforDropOut?passengerType=" +
              passengerType +
              "&vehicleID=" +
              vehicleID +
              "&routeID=" +
              routeID,
            headers: {
              Accept: "application/json;charset=UTF-8",
              "Content-type": "application/json; charset=utf-8",
              CallContext: JSON.stringify(context),
            },
          })
            .success(function (result) {
              $scope.StudentStaffDropOutStopWiseDetails = result;
              $scope.GetInOutDetailCount();
              // $scope.StudentDropShedule = result;
              // $scope.StaffDropOutList = result;
            })
            .error(function () { });
        }
      }
      else {
        //Offline filling 
        if (type == "IsPickup") {

          $scope.RouteStopTypeChange("PickupOut");

          $scope.StudentPickupSheduledList = [];

          $offlineSync
            .GetScheduleLogsByRouteOffline(
              type,
              passengerType,
              vehicleID,
              routeID
            )
            .then(function (result) {

              $scope.StudentPickupSheduledList = result.filter(x => x.StudentID != null && x.ScheduleLogType == 'PICK-OUT');
              $scope.StaffPickupSheduledList = result.filter(x => x.EmployeeID != null && x.ScheduleLogType == 'PICK-OUT');

              // if ($scope.StudentPickupSheduledList.length > 0) {
              $scope.StudentPickoutSheduleCount = $scope.StudentPickupSheduledList.length;
              // }

              // if ($scope.StaffPickupSheduledList.length > 0) {
              $scope.StaffPickoutSheduleCount = $scope.StaffPickupSheduledList.length;
              // }
              $scope.GetInOutDetailCount();
            });
        }
        else {
          $scope.RouteStopTypeChange("DropOut");
          $scope.StudentStaffDropOutStopWiseDetails = null;


          $offlineSync
            .GetScheduleLogsByRouteOffline(
              type,
              passengerType,
              vehicleID,
              routeID
            )
            .then(function (result) {

              $scope.StudentStaffDropOutStopWiseDetails = result;

              $scope.GetInOutDetailCount();
            });
        }
      }


    };

    $scope.AddNewSheduleDataByRoute = function (
      scheduleDetails,
      passengerType,
      status
    ) {
      var scheduleLogData = {};

      if (passengerType == "Student") {
        if (status == true) {
          if (scheduleDetails.StudentRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: null,
              StopEntryStatusID: 1,
              Status: "O",
            };
          }
          if (scheduleDetails.StudentRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "I",
            };
          }
        } else {
          if (scheduleDetails.StudentRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "O",
            };
          }
          if (scheduleDetails.StudentRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
            };
          }
        }
      }

      if (passengerType == "Staff") {
        if (status == true) {
          if (scheduleDetails.StaffRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 1, //For pickup status
              Status: "I",
            };
          }
          if (scheduleDetails.StaffRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "I",
            };
          }
        } else {
          if (scheduleDetails.StaffRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "O",
            };
          }
          if (scheduleDetails.StaffRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
            };
          }
        }
      }

      var syncStatus = false;
      var IsStudentIn = false;
      var IsStaffIn = true;
      // if (!$rootScope.IsOnline) {

      if (scheduleLogData.SheduleLogStatusID == 1) {
        if (passengerType == "Employee") {
          IsStudentIn = false;
          IsStaffIn = true;
        }
        else {
          IsStudentIn = true;
          IsStaffIn = false;
        }

      }
      else {
        IsStudentIn = false;
        IsStaffIn = false;
      }

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, scheduleDetails); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStudentIn, IsStaffIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline

      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            scheduleDetails.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, scheduleDetails, scheduleDetails.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }


    };

    $scope.AddNewSheduleData = function (
      scheduleDetails,
      passengerType,
      status
    ) {
      var scheduleLogData = {};

      if (passengerType == "Student") {
        if (status == true) {
          if (scheduleDetails.StudentRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 1, //For pickup status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
          if (scheduleDetails.StudentRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        } else {
          if (scheduleDetails.StudentRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
          if (scheduleDetails.StudentRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentRouteStopMap.StudentID,
              EmployeeID: null,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        }
      }

      if (passengerType == "Staff") {
        if (status == true) {
          if (scheduleDetails.StaffRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 1, //For pickup status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
          if (scheduleDetails.StaffRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        } else {
          if (scheduleDetails.StaffRouteStopMap.IsPickupStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 1, //For pickup status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
          if (scheduleDetails.StaffRouteStopMap.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffRouteStopMap.StaffID,
              RouteID: scheduleDetails.RouteID,
              RouteStopMapID: scheduleDetails.RouteStopMapID,
              VehicleID: scheduleDetails.VehicleID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        }
      }

      var syncStatus = false;
      var IsStudentIn = false;
      var IsStaffIn = true;
      // if (!$rootScope.IsOnline) {

      if (scheduleLogData.SheduleLogStatusID == 1) {
        if (passengerType == "Employee") {
          IsStudentIn = false;
          IsStaffIn = true;
        }
        else {
          IsStudentIn = true;
          IsStaffIn = false;
        }

      }
      else {
        IsStudentIn = false;
        IsStaffIn = false;
      }

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, scheduleDetails); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStudentIn, IsStaffIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline

      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            scheduleDetails.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, scheduleDetails, scheduleDetails.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }
    };

    $scope.AddNewDropSheduleData = function (
      scheduleDetails,
      routeDetails,
      stopDetails,
      vehicleDetails,
      passengerType,
      status
    ) {
      var scheduleLogData = {};

      if (passengerType == "Student") {
        if (status == true) {
          if (scheduleDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: vehicleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        } else {
          if (scheduleDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: scheduleDetails.StudentID,
              EmployeeID: null,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: vehicleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        }
      }

      if (passengerType == "Staff") {
        if (status == true) {
          if (scheduleDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: vehicleDetails.VehicleIID,
              SheduleLogStatusID: 1, //For IN status
              StopEntryStatusID: 2, //For drop status
              Status: "A",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        } else {
          if (scheduleDetails.IsDropStop == true) {
            scheduleLogData = {
              DriverScheduleLogIID: scheduleDetails.DriverScheduleLogIID,
              StudentID: null,
              EmployeeID: scheduleDetails.StaffID,
              RouteID: routeDetails.RouteID,
              RouteStopMapID: stopDetails.RouteStopMapIID,
              VehicleID: vehicleDetails.VehicleIID,
              SheduleLogStatusID: 2, //For OUT status
              StopEntryStatusID: 2, //For drop status
              Status: "O",
              ScheduleLogType: scheduleDetails.ScheduleLogType,
            };
          }
        }
      }
      var syncStatus = false;
      var IsStudentIn = false;
      var IsStaffIn = true;
      // if (!$rootScope.IsOnline) {

      if (scheduleLogData.SheduleLogStatusID == 1) {
        if (passengerType == "Employee") {
          IsStudentIn = false;
          IsStaffIn = true;
        }
        else {
          IsStudentIn = true;
          IsStaffIn = false;
        }

      }
      else {
        IsStudentIn = false;
        IsStaffIn = false;
      }

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, scheduleDetails); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStudentIn, IsStaffIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline

      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            scheduleDetails.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, scheduleDetails, scheduleDetails.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }

    };

    $scope.RouteStopTypeChange = function (type) {
      if (type == "Pickup") {
        $scope.SheduleType = "PickupIn";
        $scope.GetInOutDetailCount();
      }
      if (type == "Drop") {
        $scope.SheduleType = "DropIn";
        $scope.GetInOutDetailCount();
      }
      if (type == "PickupIn") {
        $scope.SheduleType = type;

      }
      if (type == "PickupOut") {
        $scope.SheduleType = type;

      }
      if (type == "DropIn") {
        $scope.SheduleType = type;
      }
      if (type == "DropOut") {
        $scope.SheduleType = type;
      }
      if (type == "IsDropOut") {
        $scope.SheduleType = "DropOut";
      }

      $timeout(function () {
        $scope.$apply(function () {
          $scope.SearchText = null;
          $scope.ClearSearchDataLists();
        });
      });
    };
    $scope.ScanBarcode = function (routeDetails, vehicleDetail, type) {
      $scope.SearchText = null;
      $scope.BarCodeValue = null;

      $timeout(function () {
        var settings = cordova.plugins.scanner.getDefaultSettings();

        cordova.plugins.scanner.startScanning(
          function (result) {
            var notifySound = "./sounds/beep.wav";
            var audio = new Audio(notifySound);
            audio.play();

            $scope.BarCodeValue = result;

            if ($scope.BarCodeValue) {
              $scope.$apply(function () {
                //$scope.SearchText = $scope.BarCodeValue;
                if (type == "Drop") {
                  document.getElementById("search-input-drop").value = result;
                  document.getElementById("search-input-drop").text = result;
                }
                else {
                  document.getElementById("search-input-pickup").value = result;
                  document.getElementById("search-input-pickup").text = result;
                }
                $scope.SearchText = result;
                $scope.FillSearchText(routeDetails, vehicleDetail, type);
              });
            }
          },
          function (error) {
            alert("Scanning failed: " + error);
          },

          settings
        );
      });
    };

    $scope.FillSearchText = function (routeDetails, vehicleDetail, type) {
      // var autoChekBox = document.getElementById("InorOutChecked"); 
      // if(autoChekBox.checked == true)
      // {
      //   $scope.IsAutoInOrOut = true;
      // }
      // else
      // {
      //   $scope.IsAutoInOrOut = false;
      // }

      if (type == "Drop") {
        var seachTextData = document.getElementById("search-input-drop").value;
      }
      else {
        var seachTextData = document.getElementById("search-input-pickup").value;
      }

      // if ($scope.SheduleType == "PickupIn")
      // {
      //   $scope.isAlertMsg = true;
      //   $scope.SearchDataAlreadyExistMsg = "test message";
      // }

      // if (data) {
      //   seachTextData = data;
      // } else {
      //   seachTextData = null;
      // }
      $timeout(function () {
        $scope.$apply(function () {
          $scope.SearchText = seachTextData;

          if (type == "Pickup") {
            $scope.FilterPickSheduleDataBySearchData(
              $scope.SearchText,
              routeDetails,
              vehicleDetail
            );
          }
          if (type == "Drop") {
            $scope.FilterDropSheduleDataBySearchData(
              $scope.SearchText,
              routeDetails,
              vehicleDetail
            );
          }
        });
      });
    };

    $scope.FilterPickSheduleDataBySearchData = function (
      searchData,
      routeDetails,
      vehicleDetail
    ) {
      var studentStops = [];
      var staffStops = [];

      if (searchData) {
        if ($scope.SheduleType == "PickupIn") {
          if ($scope.IsStudentDetails == true) {
            $scope.FilterScheduleStudentPickInDetails = [];

            studentStops = routeDetails.Stops.filter(
              (x) => x.StopsStudentDetails.length > 0
            );

            studentStops.forEach((x) =>
              x.StopsStudentDetails.forEach((y) => {
                if (
                  y.AdmissionNumber.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.IsPickupStop == true
                ) {
                  $scope.FilterScheduleStudentPickInDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    AdmissionNumber: y.AdmissionNumber,
                    IsActive: y.IsActive,
                    IsDropStop: y.IsDropStop,
                    IsPickupStop: y.IsPickupStop,
                    IsStudentIn: y.IsStudentIn,
                    StudentID: y.StudentID,
                    StudentName: y.StudentName,
                    ClassName: y.ClassName,
                    SectionName: y.SectionName,
                    StudentProfile: y.StudentProfile,
                    RouteID: routeDetails.RouteID,
                    VehicleID: vehicleDetail.VehicleIID,
                    RouteStopMapID: x.RouteStopMapIID,
                    StopName: x.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              })
            );
          }

          if ($scope.IsStaffDetails == true) {
            $scope.FilterScheduleStaffPickInDetails = [];

            staffStops = routeDetails.Stops.filter(
              (x) => x.StopsStaffDetails.length > 0
            );

            staffStops.forEach((x) =>
              x.StopsStaffDetails.forEach((y) => {
                if (
                  y.EmployeeCode.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.IsPickupStop == true
                ) {
                  $scope.FilterScheduleStaffPickInDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    EmployeeCode: y.EmployeeCode,
                    IsActive: y.IsActive,
                    IsDropStop: y.IsDropStop,
                    IsPickupStop: y.IsPickupStop,
                    IsStaffIn: y.IsStaffIn,
                    StaffID: y.StaffID,
                    StaffName: y.StaffName,
                    StaffProfile: y.StaffProfile,
                    RouteID: routeDetails.RouteID,
                    VehicleID: vehicleDetail.VehicleIID,
                    RouteStopMapID: x.RouteStopMapIID,
                    StopName: x.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              })
            );
          }

          if ($scope.IsAutoInOrOut == true && $scope.FilterScheduleStudentPickInDetails.length == 1 && $scope.FilterScheduleStudentPickInDetails[0].AdmissionNumber.toUpperCase() == $scope.SearchText.toUpperCase() ||
            $scope.IsAutoInOrOut == true && $scope.FilterScheduleStaffPickInDetails.length == 1 && $scope.FilterScheduleStaffPickInDetails[0].EmployeeCode.toUpperCase() == $scope.SearchText.toUpperCase()) {
            var statuschangeIN = true;
            if ($scope.FilterScheduleStudentPickInDetails.length > 0) {
              $scope.ChangeFilterPickupDataStatus($scope.FilterScheduleStudentPickInDetails[0], 'Student', statuschangeIN);
            }
            else {
              $scope.ChangeFilterPickupDataStatus($scope.FilterScheduleStaffPickInDetails[0], 'Employee', statuschangeIN);
            }
            $scope.LoadingSpinner = true;
            $scope.LoadScheduleInfo();
          }

        }
        if ($scope.SheduleType == "PickupOut") {
          if ($scope.IsStudentDetails == true) {
            $scope.FilterScheduleStudentPickOutDetails = [];

            if ($scope.StudentPickupSheduledList.length > 0) {
              $scope.StudentPickupSheduledList.forEach((y) => {
                if (
                  y.StudentRouteStopMap.AdmissionNumber.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.StudentRouteStopMap.IsPickupStop == true
                ) {
                  $scope.FilterScheduleStudentPickOutDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    AdmissionNumber: y.StudentRouteStopMap.AdmissionNumber,
                    IsActive: y.StudentRouteStopMap.IsActive,
                    IsDropStop: y.StudentRouteStopMap.IsDropStop,
                    IsPickupStop: y.StudentRouteStopMap.IsPickupStop,
                    IsStudentIn: y.StudentRouteStopMap.IsStudentIn,
                    StudentID: y.StudentRouteStopMap.StudentID,
                    StudentName: y.StudentRouteStopMap.StudentName,
                    ClassName: y.StudentRouteStopMap.ClassName,
                    SectionName: y.StudentRouteStopMap.SectionName,
                    StudentProfile: y.StudentRouteStopMap.StudentProfile,
                    RouteID: y.RouteID,
                    VehicleID: y.VehicleID,
                    RouteStopMapID: y.RouteStopMapID,
                    StopName: y.StudentRouteStopMap.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              });
            }
          }

          if ($scope.IsStaffDetails == true) {
            $scope.FilterScheduleStaffPickOutDetails = [];

            if ($scope.StaffPickupSheduledList.length > 0) {
              $scope.StaffPickupSheduledList.forEach((y) => {
                if (
                  y.StaffRouteStopMap.EmployeeCode.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.StaffRouteStopMap.IsPickupStop == true
                ) {
                  $scope.FilterScheduleStaffPickOutDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    EmployeeCode: y.StaffRouteStopMap.EmployeeCode,
                    IsActive: y.StaffRouteStopMap.IsActive,
                    IsDropStop: y.StaffRouteStopMap.IsDropStop,
                    IsPickupStop: y.StaffRouteStopMap.IsPickupStop,
                    IsStaffIn: y.StaffRouteStopMap.IsStaffIn,
                    StaffID: y.StaffRouteStopMap.StaffID,
                    StaffName: y.StaffRouteStopMap.StaffName,
                    StaffProfile: y.StaffRouteStopMap.StaffProfile,
                    RouteID: y.RouteID,
                    VehicleID: y.VehicleID,
                    RouteStopMapID: y.RouteStopMapID,
                    StopName: y.StaffRouteStopMap.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              });
            }
          }
          if ($scope.IsAutoInOrOut == true && $scope.FilterScheduleStudentPickOutDetails.length == 1 && $scope.FilterScheduleStudentPickOutDetails[0].AdmissionNumber.toUpperCase() == $scope.SearchText.toUpperCase() ||
            $scope.IsAutoInOrOut == true && $scope.FilterScheduleStaffPickOutDetails.length == 1 && $scope.FilterScheduleStaffPickOutDetails[0].EmployeeCode.toUpperCase() == $scope.SearchText.toUpperCase()) {
            var statuschange = false;
            if ($scope.FilterScheduleStudentPickOutDetails.length > 0) {
              $scope.ChangeFilterPickupDataStatus($scope.FilterScheduleStudentPickOutDetails[0], 'Student', statuschange);
            }
            else {
              $scope.ChangeFilterPickupDataStatus($scope.FilterScheduleStaffPickOutDetails[0], 'Employee', statuschange);
            }
            $scope.LoadingSpinner = true;
            //$scope.LoadScheduleInfo();
          }

        }


        //for dropOut data filling
        // if ($scope.SheduleType == "DropOut") {
        //   if ($scope.IsStudentDetails == true) {
        //     $scope.FilterScheduleStudentDropOutDetails = [];

        //     if ($scope.StudentDropOutSheduledList.length > 0) {
        //       $scope.StudentDropOutSheduledList.forEach((y) => {
        //         if (
        //           y.StudentRouteStopMap.AdmissionNumber.toUpperCase().includes(
        //             searchData.toUpperCase()
        //           ) &&
        //           y.StudentRouteStopMap.IsPickupStop == true
        //         ) {
        //           $scope.FilterScheduleStudentDropOutDetails.push({
        //             DriverScheduleLogIID: y.DriverScheduleLogIID,
        //             AdmissionNumber: y.StudentRouteStopMap.AdmissionNumber,
        //             IsActive: y.StudentRouteStopMap.IsActive,
        //             IsDropStop: y.StudentRouteStopMap.IsDropStop,
        //             IsPickupStop: y.StudentRouteStopMap.IsPickupStop,
        //             IsStudentIn: y.StudentRouteStopMap.IsStudentIn,
        //             StudentID: y.StudentRouteStopMap.StudentID,
        //             StudentName: y.StudentRouteStopMap.StudentName,
        //             ClassName: y.StudentRouteStopMap.ClassName,
        //             SectionName: y.StudentRouteStopMap.SectionName,
        //             StudentProfile: y.StudentRouteStopMap.StudentProfile,
        //             RouteID: y.RouteID,
        //             VehicleID: y.VehicleID,
        //             RouteStopMapID: y.RouteStopMapID,
        //             StopName: y.StudentRouteStopMap.StopName,
        //           });
        //         }
        //       });
        //     }
        //   }

        //   if ($scope.IsStaffDetails == true) {
        //     $scope.FilterScheduleStaffDropOutDetails = [];

        //     if ($scope.StaffDropOutSheduledList.length > 0) {
        //       $scope.StaffDropOutSheduledList.forEach((y) => {
        //         if (
        //           y.StaffRouteStopMap.EmployeeCode.toUpperCase().includes(
        //             searchData.toUpperCase()
        //           ) &&
        //           y.StaffRouteStopMap.IsPickupStop == true
        //         ) {
        //           $scope.FilterScheduleStaffDropOutDetails.push({
        //             DriverScheduleLogIID: y.DriverScheduleLogIID,
        //             EmployeeCode: y.StaffRouteStopMap.EmployeeCode,
        //             IsActive: y.StaffRouteStopMap.IsActive,
        //             IsDropStop: y.StaffRouteStopMap.IsDropStop,
        //             IsPickupStop: y.StaffRouteStopMap.IsPickupStop,
        //             IsStaffIn: y.StaffRouteStopMap.IsStaffIn,
        //             StaffID: y.StaffRouteStopMap.StaffID,
        //             StaffName: y.StaffRouteStopMap.StaffName,
        //             StaffProfile: y.StaffRouteStopMap.StaffProfile,
        //             RouteID: y.RouteID,
        //             VehicleID: y.VehicleID,
        //             RouteStopMapID: y.RouteStopMapID,
        //             StopName: y.StaffRouteStopMap.StopName,
        //           });
        //         }
        //       });
        //     }
        //   }

        //   if ($scope.IsAutoInOrOut == true)
        //   {
        //     var statuschange = false;
        //     if ($scope.IsStudentDetails == true)
        //     {
        //       $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStudentDropOutDetails[0],'Student',statuschange);
        //     }
        //     else{
        //       $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStaffDropOutDetails[0],'Employee',statuschange);
        //     }
        //     $scope.LoadScheduleInfo();
        //     $scope.ClearSearchDataLists();
        //   }

        // }
      } else {
        $timeout(function () {
          $scope.$apply(function () {
            $scope.ClearSearchDataLists();
          });
        });
      }
    };
    //pick schedule data filter function end------

    $scope.FilterDropSheduleDataBySearchData = function (
      searchData,
      routeDetails,
      vehicleDetail
    ) {
      var studentStops = [];
      var staffStops = [];

      if (searchData) {
        if ($scope.SheduleType == "DropIn") {
          if ($scope.IsStudentDetails == true) {
            $scope.FilterScheduleStudentDropInDetails = [];

            studentStops = routeDetails.Stops.filter(
              (x) => x.StopsStudentDetails.length > 0
            );

            studentStops.forEach((x) =>
              x.StopsStudentDetails.forEach((y) => {
                if (
                  y.AdmissionNumber.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.IsDropStop == true
                ) {
                  $scope.FilterScheduleStudentDropInDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    AdmissionNumber: y.AdmissionNumber,
                    IsActive: y.IsActive,
                    IsDropStop: y.IsDropStop,
                    IsPickupStop: y.IsPickupStop,
                    IsStudentIn: y.IsStudentIn,
                    StudentID: y.StudentID,
                    StudentName: y.StudentName,
                    ClassName: y.ClassName,
                    SectionName: y.SectionName,
                    StudentProfile: y.StudentProfile,
                    RouteID: routeDetails.RouteID,
                    VehicleID: vehicleDetail.VehicleIID,
                    RouteStopMapID: x.RouteStopMapIID,
                    StopName: x.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              })
            );
          }
          if ($scope.IsStaffDetails == true) {
            $scope.FilterScheduleStaffDropInDetails = [];

            staffStops = routeDetails.Stops.filter(
              (x) => x.StopsStaffDetails.length > 0
            );

            staffStops.forEach((x) =>
              x.StopsStaffDetails.forEach((y) => {
                if (
                  y.EmployeeCode.toUpperCase().includes(
                    searchData.toUpperCase()
                  ) &&
                  y.IsDropStop == true
                ) {
                  $scope.FilterScheduleStaffDropInDetails.push({
                    DriverScheduleLogIID: y.DriverScheduleLogIID,
                    EmployeeCode: y.EmployeeCode,
                    IsActive: y.IsActive,
                    IsDropStop: y.IsDropStop,
                    IsPickupStop: y.IsPickupStop,
                    IsStaffIn: y.IsStaffIn,
                    StaffID: y.StaffID,
                    StaffName: y.StaffName,
                    StaffProfile: y.StaffProfile,
                    RouteID: routeDetails.RouteID,
                    VehicleID: vehicleDetail.VehicleIID,
                    RouteStopMapID: x.RouteStopMapIID,
                    StopName: x.StopName,
                    ScheduleLogType: y.ScheduleLogType,
                    Status: y.Status,
                  });
                }
              })
            );
          }

          if ($scope.IsAutoInOrOut == true && $scope.FilterScheduleStudentDropInDetails.length == 1 && $scope.FilterScheduleStudentDropInDetails[0].AdmissionNumber.toUpperCase() == $scope.SearchText.toUpperCase() ||
            $scope.IsAutoInOrOut == true && $scope.FilterScheduleStaffDropInDetails.length == 1 && $scope.FilterScheduleStaffDropInDetails[0].EmployeeCode.toUpperCase() == $scope.SearchText.toUpperCase()) {
            var statuschange = true;
            if ($scope.FilterScheduleStudentDropInDetails.length > 0) {
              $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStudentDropInDetails[0], 'Student', statuschange);
            }
            else {
              $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStaffDropInDetails[0], 'Employee', statuschange);
            }
            $scope.LoadingSpinner = true;
            $scope.LoadScheduleInfo();
          }

        }
        if ($scope.SheduleType == "DropOut") {
          if ($scope.IsStudentDetails == true) {
            $scope.FilterScheduleStudentDropOutDetails = [];

            if (
              $scope.StudentStaffDropOutStopWiseDetails != null ||
              $scope.StudentStaffDropOutStopWiseDetails != undefined
            ) {
              studentStops = $scope.StudentStaffDropOutStopWiseDetails.Stops.filter(
                (x) => x.StopsStudentDetails.length > 0
              );

              studentStops.forEach((x) =>
                x.StopsStudentDetails.forEach((y) => {
                  if (
                    y.AdmissionNumber.toUpperCase().includes(
                      searchData.toUpperCase()
                    ) &&
                    y.IsDropStop == true
                  ) {
                    $scope.FilterScheduleStudentDropOutDetails.push({
                      DriverScheduleLogIID: y.DriverScheduleLogIID,
                      AdmissionNumber: y.AdmissionNumber,
                      IsActive: y.IsActive,
                      IsDropStop: y.IsDropStop,
                      IsPickupStop: y.IsPickupStop,
                      IsStudentIn: y.IsStudentIn,
                      StudentID: y.StudentID,
                      StudentName: y.StudentName,
                      ClassName: y.ClassName,
                      SectionName: y.SectionName,
                      StudentProfile: y.StudentProfile,
                      RouteID: routeDetails.RouteID,
                      VehicleID: vehicleDetail.VehicleIID,
                      RouteStopMapID: x.RouteStopMapIID,
                      StopName: x.StopName,
                      ScheduleLogType: y.ScheduleLogType,
                      Status: y.Status,
                    });
                  }
                })
              );
            }
          }
          if ($scope.IsStaffDetails == true) {
            //$scope.StudentStaffDropOutStopWiseDetails = [];
            $scope.FilterScheduleStaffDropOutDetails = [];

            if (
              $scope.StudentStaffDropOutStopWiseDetails != null ||
              $scope.StudentStaffDropOutStopWiseDetails != undefined
            ) {
              staffStops = $scope.StudentStaffDropOutStopWiseDetails.Stops.filter(
                (x) => x.StopsStaffDetails.length > 0
              );

              staffStops.forEach((x) =>
                x.StopsStaffDetails.forEach((y) => {
                  if (
                    y.EmployeeCode.toUpperCase().includes(
                      searchData.toUpperCase()
                    ) &&
                    y.IsDropStop == true
                  ) {
                    $scope.FilterScheduleStaffDropOutDetails.push({
                      DriverScheduleLogIID: y.DriverScheduleLogIID,
                      EmployeeCode: y.EmployeeCode,
                      IsActive: y.IsActive,
                      IsDropStop: y.IsDropStop,
                      IsPickupStop: y.IsPickupStop,
                      IsStaffIn: y.IsStaffIn,
                      StaffID: y.StaffID,
                      StaffName: y.StaffName,
                      StaffProfile: y.StaffProfile,
                      RouteID: routeDetails.RouteID,
                      VehicleID: vehicleDetail.VehicleIID,
                      RouteStopMapID: x.RouteStopMapIID,
                      StopName: x.StopName,
                      ScheduleLogType: y.ScheduleLogType,
                      Status: y.Status,
                    });
                  }
                })
              );
            }
          }
          if ($scope.IsAutoInOrOut == true && $scope.FilterScheduleStudentDropOutDetails.length == 1 && $scope.FilterScheduleStudentDropOutDetails[0].AdmissionNumber.toUpperCase() == $scope.SearchText.toUpperCase() ||
            $scope.IsAutoInOrOut == true && $scope.FilterScheduleStaffDropOutDetails.length == 1 && $scope.FilterScheduleStaffDropOutDetails[0].EmployeeCode.toUpperCase() == $scope.SearchText.toUpperCase()) {
            var statuschange = false;
            if ($scope.FilterScheduleStudentDropOutDetails.length > 0) {
              $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStudentDropOutDetails[0], 'Student', statuschange);
            }
            else {
              $scope.ChangeFilterDropDataStatus($scope.FilterScheduleStaffDropOutDetails[0], 'Employee', statuschange);
            }
            $scope.LoadingSpinner = true;
            $scope.LoadScheduleInfo();
          }
        }
      } else {
        $timeout(function () {
          $scope.$apply(function () {
            $scope.ClearSearchDataLists();
          });
        });
      }
    };

    $scope.ChangeFilterPickupDataStatus = function (
      filteredData,
      passengerType,
      status
    ) {
      if ($scope.SheduleType == "PickupIn") {
        if (passengerType == "Student") {
          if (status == true) {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 1, //For pickup status
                Status: "I",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 1, //For pickup status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        if (passengerType == "Employee") {
          if (status == true) {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 1, //For pickup status
                Status: "I",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 1, //For pickup status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        $scope.LoadScheduleInfo();
        $scope.GetInOutDetailCount();
      }

      if ($scope.SheduleType == "PickupOut") {
        if (passengerType == "Student") {
          if (status == true) {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 1, //For pickup status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 1, //For pickup status
                Status: "O",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        if (passengerType == "Employee") {
          if (status == true) {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 1, //For pickup status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsPickupStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 1, //For pickup status
                Status: "O",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
      }

      var syncStatus = false;
      var IsStudentIn = false;
      var IsStaffIn = true;
      // if (!$rootScope.IsOnline) {

      if (passengerType == "Employee") {
        IsStudentIn = false;
        IsStaffIn = true;
      }
      else {
        IsStudentIn = true;
        IsStaffIn = false;
      }

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, filteredData); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStudentIn, IsStaffIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline

      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            filteredData.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, filteredData, filteredData.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }
    };

    $scope.ChangeFilterDropDataStatus = function (
      filteredData,
      passengerType,
      status
    ) {
      if ($scope.SheduleType == "DropIn") {
        if (passengerType == "Student") {
          if (status == true) {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 2, //For drop status
                Status: "I",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 2, //For drop status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        if (passengerType == "Employee") {
          if (status == true) {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 2, //For drop status
                Status: "I",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 2, //For drop status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        $scope.LoadScheduleInfo();
      }

      if ($scope.SheduleType == "DropOut") {
        if (passengerType == "Student") {
          if (status == true) {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 2, //For drop status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: filteredData.StudentID,
                EmployeeID: null,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 2, //For drop status
                Status: "O",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
        if (passengerType == "Employee") {
          if (status == true) {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 1, //For IN status
                StopEntryStatusID: 2, //For drop status
                Status: "A",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          } else {
            if (filteredData.IsDropStop == true) {
              scheduleLogData = {
                DriverScheduleLogIID: filteredData.DriverScheduleLogIID,
                StudentID: null,
                EmployeeID: filteredData.StaffID,
                RouteID: filteredData.RouteID,
                RouteStopMapID: filteredData.RouteStopMapID,
                VehicleID: filteredData.VehicleID,
                SheduleLogStatusID: 2, //For OUT status
                StopEntryStatusID: 2, //For drop status
                Status: "O",
                ScheduleLogType: filteredData.ScheduleLogType,
              };
            }
          }
        }
      }

      var syncStatus = false;
      var IsStudentIn = false;
      var IsStaffIn = true;
      // if (!$rootScope.IsOnline) {

      if (scheduleLogData.SheduleLogStatusID == 1) {
        if (passengerType == "Employee") {
          IsStudentIn = false;
          IsStaffIn = true;
        }
        else {
          IsStudentIn = true;
          IsStaffIn = false;
        }

      }
      else {
        IsStudentIn = false;
        IsStaffIn = false;
      }

      $scope.LoadScheduleInfo();

      if ($rootScope.IsOnline) {
        syncStatus = true;
        $scope.SaveScheduleData(scheduleLogData, filteredData); //Save In ONLINE
      }
      $offlineSync.UpdateIndexedDBINOUTStatus(
        scheduleLogData.DriverScheduleLogIID,
        scheduleLogData.SheduleLogStatusID,
        scheduleLogData.StopEntryStatusID,
        scheduleLogData.Status, IsStudentIn, IsStaffIn, scheduleLogData.ScheduleLogType,
        syncStatus
      ); //UPDATE in Offline

      if (!$rootScope.IsOnline) {
        $timeout(function () {
          $scope.$apply(function () {

            filteredData.Status = scheduleLogData.Status;
            AutoInOutAfterSaveOffline(scheduleLogData, filteredData, filteredData.Status);
            // $scope.GetInOutDetailCount();
          });
        });
      }

    };

    $scope.ClearSearchDataLists = function () {
      $scope.FilterScheduleStudentPickInDetails = [];
      $scope.FilterScheduleStaffPickInDetails = [];

      $scope.FilterScheduleStudentPickOutDetails = [];
      $scope.FilterScheduleStaffPickOutDetails = [];

      $scope.FilterScheduleStudentDropInDetails = [];
      $scope.FilterScheduleStaffDropInDetails = [];

      $scope.FilterScheduleStudentDropOutDetails = [];
      $scope.FilterScheduleStaffDropOutDetails = [];
    };

    $scope.AutoMarkInOutCheckBoxChange = function (changeData) {
      $scope.IsAutoInOrOut = changeData;
    };

    $scope.AutoMarkInOutData = function (data) {
      if ($scope.IsAutoInOrOut == true) {
        if (data.length == 1) {
        }
      }
    };

    $scope.GetInOutDetailCount = function () {
      var totalCount = 0;
      var inCount = 0;
      var outCount = 0;

      var dropinTotal = 0;
      var dropinCount = 0;
      var dropnotinCount = 0;

      var pickOutTotal = 0;
      var pickOutCount = 0;
      var pickupNotOut = 0;

      var dropOutTotal = 0;
      var dropOutCount = 0;
      var dropnotOutCount = 0;

      // Pickup-In Checking Start
      if ($scope.SheduleType == "PickupIn") {
        if ($scope.ScheduleDetails.length != 0) {
          $scope.ScheduleDetails.forEach(x => {
            if (x.RoutesDetails.length > 0) {
              x.RoutesDetails.forEach(y => {
                if (y.Stops.length > 0) {
                  y.Stops.forEach(z => {
                    if (z.StopsStudentDetails.length > 0) {
                      var studentData = z.StopsStudentDetails.filter(s => s.StudentID != null);
                      if (studentData.length > 0) {
                        totalCount = totalCount + studentData.filter(a => a.ScheduleLogType == "PICK-IN").length;
                        inCount = inCount + studentData.filter(a => a.ScheduleLogType == "PICK-IN" && a.Status == "I").length;
                        outCount = outCount + studentData.filter(a => a.ScheduleLogType == "PICK-IN" && a.Status == "A").length;
                      }
                    }
                    if (z.StopsStaffDetails.length > 0) {
                      var staffData = z.StopsStaffDetails.filter(s => s.StaffID != null);
                      if (staffData.length > 0) {
                        totalCount = totalCount + staffData.filter(a => a.ScheduleLogType == "PICK-IN").length;
                        inCount = inCount + staffData.filter(a => a.ScheduleLogType == "PICK-IN" && a.Status == "I").length;
                        outCount = outCount + staffData.filter(a => a.ScheduleLogType == "PICK-IN" && a.Status == "A").length;
                      }
                    }
                  })
                }
              })
            }
          });
        }

        // $timeout(function () {
        //  $scope.$apply(function () {
        $scope.PickupInTotalCount = totalCount;
        $scope.PickupInCount = inCount;
        $scope.PickupNotInCount = outCount;
        // });
        //});
      }
      // Pickup-In Checking End

      // Pickup-Out Starts
      if ($scope.SheduleType == "PickupOut") {
        var pickOutCountStud = 0;
        var pickOutnotCountStud = 0;
        var pickOutCountStaff = 0;
        var pickOutnotCountStaff = 0;

        var studPickOutDatas = $scope.StudentPickupSheduledList.filter(x => x.StudentID != null && x.ScheduleLogType == 'PICK-OUT');
        var staffPickOutDatas = $scope.StaffPickupSheduledList.filter(x => x.EmployeeID != null && x.ScheduleLogType == 'PICK-OUT');


        if (studPickOutDatas.length > 0) {
          var pickOutCountStud = studPickOutDatas.filter(x => x.StudentRouteStopMap.Status == "O").length;
          var pickOutnotCountStud = studPickOutDatas.filter(x => x.StudentRouteStopMap.Status == "A").length;
        }
        if (staffPickOutDatas.length > 0) {
          var pickOutCountStaff = staffPickOutDatas.filter(x => x.StaffRouteStopMap.Status == "O").length;
          var pickOutnotCountStaff = staffPickOutDatas.filter(x => x.StaffRouteStopMap.Status == "A").length;
        }

        pickOutCount = pickOutCountStud + pickOutCountStaff;
        pickupNotOut = pickOutnotCountStud + pickOutnotCountStaff;
        pickOutTotal = studPickOutDatas.length + staffPickOutDatas.length;

        $scope.PickupOutTotalCount = pickOutTotal;
        $scope.PickupOutCount = pickOutCount;
        $scope.PickupNotOutCount = pickupNotOut;

      }
      // Pickup-Out End

      // Drop-In Checking Start
      if ($scope.SheduleType == "DropIn") {
        if ($scope.ScheduleDetails.length != 0) {
          $scope.ScheduleDetails.forEach(x => {
            if (x.RoutesDetails.length > 0) {
              x.RoutesDetails.forEach(y => {
                if (y.Stops.length > 0) {
                  y.Stops.forEach(z => {

                    if (z.StopsStudentDetails.length > 0) {
                      var studentData = z.StopsStudentDetails.filter(s => s.StudentID != null);
                      if (studentData.length > 0) {
                        dropinTotal = dropinTotal + studentData.filter(a => a.ScheduleLogType == "DROP-IN").length;
                        dropinCount = dropinCount + studentData.filter(a => a.ScheduleLogType == "DROP-IN" && a.Status == "I").length;
                        dropnotinCount = dropnotinCount + studentData.filter(a => a.ScheduleLogType == "DROP-IN" && a.Status == "A").length;
                      }
                    }
                    if (z.StopsStaffDetails.length > 0) {
                      var staffData = z.StopsStaffDetails.filter(s => s.StaffID != null);
                      if (staffData.length > 0) {
                        dropinTotal = dropinTotal + staffData.filter(a => a.ScheduleLogType == "DROP-IN").length;
                        dropinCount = dropinCount + staffData.filter(a => a.ScheduleLogType == "DROP-IN" && a.Status == "I").length;
                        dropnotinCount = dropnotinCount + staffData.filter(a => a.ScheduleLogType == "DROP-IN" && a.Status == "A").length;
                      }
                    }

                  })
                }
              })
            }
          });
        }

        // $timeout(function () {
        //  $scope.$apply(function () {
        $scope.DropTotalCount = dropinTotal;
        $scope.DropInCount = dropinCount;
        $scope.DropNotInCount = dropnotinCount;
        // });
        // });
      }
      // Drop-In Checking End

      // Drop-Out Start
      if ($scope.SheduleType == "DropOut") {

        if ($scope.StudentStaffDropOutStopWiseDetails != null || $scope.StudentStaffDropOutStopWiseDetails != undefined) {
          if ($scope.StudentStaffDropOutStopWiseDetails.Stops.length > 0) {
            $scope.StudentStaffDropOutStopWiseDetails.Stops.forEach(z => {

              if (z.StopsStudentDetails.length > 0) {
                var studentData = z.StopsStudentDetails.filter(s => s.StudentID != null);
                if (studentData.length > 0) {
                  dropOutTotal = dropOutTotal + studentData.filter(a => a.ScheduleLogType == "DROP-OUT").length;
                  dropOutCount = dropOutCount + studentData.filter(a => a.ScheduleLogType == "DROP-OUT" && a.Status == "O").length;
                  dropnotOutCount = dropnotOutCount + studentData.filter(a => a.ScheduleLogType == "DROP-OUT" && a.Status == "A").length;
                }
              }
              if (z.StopsStaffDetails.length > 0) {
                var staffData = z.StopsStaffDetails.filter(s => s.StaffID != null);
                if (staffData.length > 0) {
                  dropOutTotal = dropOutTotal + staffData.filter(a => a.ScheduleLogType == "DROP-OUT").length;
                  dropOutCount = dropOutCount + staffData.filter(a => a.ScheduleLogType == "DROP-OUT" && a.Status == "O").length;
                  dropnotOutCount = dropnotOutCount + staffData.filter(a => a.ScheduleLogType == "DROP-OUT" && a.Status == "A").length;
                }
              }
            })
          }

          //  $timeout(function () {
          //   $scope.$apply(function () {
          $scope.DropOutTotalCount = dropOutTotal;
          $scope.DropOutCount = dropOutCount;
          $scope.DropNotOutCount = dropnotOutCount;

          //  });
          // });
        }
      }
      // Drop-Out End

    };

    $scope.syncClick = function () {
      if ($rootScope.IsOnline == true) {
        $offlineSync.SyncLiveDB(function (result) {
          var IsSyncSuccess = result;
        });

        $rootScope.ToastMessage = "Offline data successfully synced"
        $('.toast').toast('show');
        location.reload()
      }
      else {
        $offlineSync
          .GetStudentsAndStaffScheduleDetails("ALL")
          .then(function (data) {
            $scope.ScheduleDetails = data;
            if ($scope.ScheduleDetails.length > 0) {
              $scope.GetInOutDetailCount();
              $rootScope.ToastMessage = "Offline data successfully synced!"
              // $('.toast').toast('show');
            }
            else { $scope.AlertText = "Offline Data is not available!" }

            $rootScope.ShowLoader = false;
            $scope.LoadingSpinner = false;
          });
      }

    };
    $scope.ClearCacheClick = function () {
      location.reload()
      $rootScope.ToastMessage = "Cache cleared"
      $('.toast').toast('show');
    };
    // $scope.init();
  },
]);
