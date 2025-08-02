var app = angular.module("EduegateApp", [
  "ui.router",
  "ngSanitize",
  "angular.filter",
  "ngTouch",
  "pascalprecht.translate",
  "indexedDB",
]);

app.config(function ($stateProvider, $urlRouterProvider) {
  console.log("App.js loaded.");

  $urlRouterProvider.otherwise("/home");

  $stateProvider
    .state("home", {
      url: "/home",
      templateUrl: "partials/home.html",
      controller: "HomeController",
    })
    .state("mywards", {
      url: "/mywards",
      templateUrl: "partials/mywards.html",
      controller: "MyWardsController",
    })
    .state("profile", {
      url: "/profile",
      templateUrl: "partials/profile.html",
      controller: "ProfileController",
    })
    .state("enroll", {
      url: "/enroll",
      templateUrl: "partials/enroll.html",
      controller: "EnrollController",
    })
    .state("usersettings", {
      cache: false,
      url: "/usersettings:random",
      templateUrl: "partials/user-settings.html",
      //controller: 'UserSettingController'
    })
    .state("login", {
      url: "/login:redirectUrl?IsDigitalCart",
      templateUrl: "partials/login.html",
      controller: "LoginController",
    })
    .state("register", {
      url: "/register",
      templateUrl: "partials/register.html",
      controller: "RegisterController",
    })
    .state("changepassword", {
      url: "/changepassword",
      templateUrl: "partials/changepassword.html",
      controller: "ChangePasswordController",
    })
    .state("contactus", {
      url: "/contactus",
      templateUrl: "partials/contact-us.html",
      controller: "ContactUsController",
    })
    .state("aboutus", {
      url: "/aboutus",
      templateUrl: "partials/about-us.html",
      controller: "AboutUsController",
    })
    .state("studentattendance", {
      url: "/studentattendance",
      templateUrl: "partials/studentattendance.html",
      controller: "StudentAttendanceController",
    })
    .state("userregistration", {
      url: "/userregistration?id?isAnonymous",
      templateUrl: "partials/userregistration.html",
      controller: "UserRegistrationController",
    })
    .state("teacherclasses", {
      url: "/teacherclasses",
      templateUrl: "partials/teacherclass.html",
      controller: "TeacherClassController",
    })
    .state("staffattendance", {
      url: "/staffattendance",
      templateUrl: "partials/staffattendance.html",
      controller: "AttendanceController",
    })
    .state("staffleavelist", {
      url: "/staffleavelist",
      templateUrl: "partials/staffleavelist.html",
      controller: "StaffLeaveController",
    })
    .state("stafftimetable", {
      url: "/stafftimetable",
      templateUrl: "partials/stafftimetable.html",
      controller: "StaffTimeTableController",
    })
    .state("driverschedule", {
      url: "/driverschedule",
      templateUrl: "partials/driverschedule.html",
      controller: "DriverSchedulerController",
    })
    .state("routedetails", {
      url: "/routedetails",
      templateUrl: "partials/routedetails.html",
      controller: "DriverSchedulerController",
    })
    .state("vehicleattendantdetails", {
      url: "/vehicleattendantdetails",
      templateUrl: "partials/vehicleattendant.html",
      controller: "VehicleAttendantController",
    })
    .state("driverReportDetails", {
      url: "/driverReportDetails",
      templateUrl: "partials/driverReports.html",
      controller: "VehicleAttendantController",
    })
    .state("teacherclass", {
      url: "/teacherclass",
      templateUrl: "partials/teacherclass.html",
      controller: "TeacherClassController",
    })
    .state("assignments", {
      url: "/assignments",
      templateUrl: "partials/assignments.html",
      controller: "AssignmentController",
    })
    .state("lessonplan", {
      url: "/lessonplan",
      templateUrl: "partials/lessonplan.html",
      controller: "LessonPlanController",
    })
    .state("marklist", {
      url: "/marklist",
      templateUrl: "partials/marklist.html",
      controller: "MarklistController",
    })
    .state("notifications", {
      url: "/notifications",
      templateUrl: "partials/mailbox.html",
      controller: "MailBoxController",
    })
    .state("offlinepage", {
      url: "/offlinepage",
      templateUrl: "partials/offline-page.html",
      controller: "OfflineController",
    })
    .state("mycirculars", {
      url: "/mycirculars",
      templateUrl: "partials/circular.html",
      controller: "CircularController",
    })
    .state("classstudents", {
      url: "/classstudents?ID?classID&sectionID",
      templateUrl: "partials/classstudents.html",
      controller: "ClassStudentController",
    })    
    .state("IDCard", {
      url: "/IDCard",
      templateUrl: "partials/IDCard.html",
      controller: 'IDCardController'
    })    
    .state("studentpickerverification", {
      url: "/studentpickerverification",
      templateUrl: "partials/studentpickerverification.html",
      controller: 'StudentPickerVerificationController'
    })
    .state("employeeleaveapplication", {
      url: "/employeeleaveapplication",
      templateUrl: "partials/employeeleaveapplication.html",
      controller: 'EmployeeLeaveApplicationController'
    })
    .state("studentpickerverificationHome", {
      url: "/studentpickerverificationHome",
      templateUrl: "partials/studentpickerverificationHome.html",
      controller: 'PickerVerificationHomeController'
    });
});

app.config(function ($translateProvider) {
  $translateProvider.preferredLanguage("en");
  $translateProvider.useStaticFilesLoader({
    prefix: "scripts/jquery-localize/",
    suffix: ".json",
  });
});

app.factory("clientSettings", function ($http) {
  var client = "pearl";
  return clientSetting[client];
});

app.service("rootUrl", function ($http) {
  return {
    // RootUrl: 'http://api.pearlschool.org/MobileAppWrapper/AppDataService.svc/',
    // SchoolServiceUrl: "http://api.pearlschool.org/MobileAppWrapper/SchoolService.svc/",
    // SecurityServiceUrl: "http://api.pearlschool.org/MobileAppWrapper/AppSecurityService.svc/",
    // UserServiceUrl: "http://api.pearlschool.org/MobileAppWrapper/UserAccountService.svc/",
    // ErpUrl: "http://erp.pearlschool.org/",
    // ParentUrl: "https://parent.pearlschool.org/",

    RootUrl: 'http://stagingapi.pearlschool.org/MobileAppWrapper/AppDataService.svc/',
    SchoolServiceUrl: "http://stagingapi.pearlschool.org/MobileAppWrapper/SchoolService.svc/",
    SecurityServiceUrl: "http://stagingapi.pearlschool.org/MobileAppWrapper/AppSecurityService.svc/",
    UserServiceUrl: "http://stagingapi.pearlschool.org/MobileAppWrapper/UserAccountService.svc/",
    ErpUrl: "http://stagingerp.pearlschool.org/",
    ParentUrl: "http://stagingparent.pearlschool.org/",

    // RootUrl: "http://192.168.1.44/Eduegate.Services/MobileAppWrapper/AppDataService.svc/",
    // SchoolServiceUrl: "http://192.168.1.44/Eduegate.Services/MobileAppWrapper/SchoolService.svc/",
    // SecurityServiceUrl: "http://192.168.1.44/Eduegate.Services/MobileAppWrapper/AppSecurityService.svc/",
    // UserServiceUrl: "http://192.168.1.44/Eduegate.Services/MobileAppWrapper/UserAccountService.svc/",
    // ErpUrl: "http://192.168.1.44/Eduegate.ERP.Admin/",
    // ParentUrl: "http://192.168.1.44/Eduegate.ERP.School.Portal/",

    ImageUrl: "",
    ErrorProductImageUrl: "img/noimage5.png",
    ErrorHomePageImageUrl: "img/noimage5.png",
    BigErrorImageUrl: "img/noimage8.png",
  };
});

app.factory("mySharedService", function ($rootScope) {
  var sharedService = {};

  //sharedService.message = '';

  sharedService.prepForBroadcast = function (reload) {
    //this.message = msg;
    this.broadcastItem(reload);
  };

  sharedService.broadcastItem = function (reload) {
    $rootScope.$broadcast("handleBroadcast", {
      reload: reload,
    });
  };

  return sharedService;
});

app.filter("html", [
  "$sce",
  function ($sce) {
    return function (text) {
      return $sce.trustAsHtml(text);
    };
  },
]);

app.filter("htmldecode", function () {
  return function (encodedHtml) {
    return angular.element("<div>").html(encodedHtml).text();
  };
});

app.directive("simpleHtml", function () {
  return function (scope, element, attr) {
    scope.$watch(attr.simpleHtml, function (value) {
      element.html(scope.$eval(attr.simpleHtml));
    });
  };
});

app.directive("onErrorSrc", function () {
  return {
    link: function (scope, element, attrs) {
      element.bind("error", function () {
        if (attrs.src != attrs.onErrorSrc) {
          attrs.$set("src", attrs.onErrorSrc);
        }
      });
    },
  };
});

app.filter("ctime", function () {
  return function (jsonDate) {
    if (!jsonDate) return null;
    var date = new Date(parseInt(jsonDate.substr(6)));
    return date;
  };
});

app.run(function ($state, $rootScope, $timeout, offlineSync) {
  //FastClick.attach(document.body);
  document.addEventListener("deviceready", onDeviceReady, false);
  function onDeviceReady() {
    FastClick.attach(document.body);
    window.open = cordova.InAppBrowser ? cordova.InAppBrowser.open : null;
    var iOS5 = device.platform;

    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
    if (iOS) {
      //alert("ios");
      $("body").addClass("platform-ios");
    }

    initializeFireBase();
   
  }

  function initializeFireBase() {
    var firebasePlugin = window.FirebasePlugin
      ? window.FirebasePlugin
      : window.FCMPlugin
        ? window.FCMPlugin
        : null;
    if (!firebasePlugin) return;
    if (firebasePlugin.grantPermission) {
      firebasePlugin.grantPermission(); // Set Permission for IOS
    }

    if (firebasePlugin.hasPermission) {
      firebasePlugin.hasPermission(function (data) {
        //Check Permission
        console.log("isEnabled  ", data.isEnabled);
      });
    }

    if (firebasePlugin.getToken) {
      getFirebaseToken(firebasePlugin);
    }

    // Get notified when a token is refreshed
    if (firebasePlugin.onTokenRefresh) {
      firebasePlugin.onTokenRefresh(
        function (token) {
          // save this server-side and use it to push notifications to this device
          if (token) {
            window.localStorage.setItem(
              "firebasedevicetoken",
              JSON.stringify(token)
            );
          } else {
            setTimeout(getFirebaseToken(firebasePlugin), 1000);
          }
        },
        function (error) { }
      );
    }

    // Get notified when the user opens a notification
    if (firebasePlugin.onMessageReceived) {
      firebasePlugin.onMessageReceived(
        function (message) {
          console.log("Message type: " + message.messageType);
          if (message.messageType === "notification") {
            if (message.tap) {
              if (!$rootScope.IsGuestUser) {
                angular
                  .element(document.getElementById("rootBody"))
                  .scope()
                  .$root.redirectPage("notification");
              } else {
                $state.go("customer-new");
              }
            }

            if (message.body.includes("replacement request")) {
              var orderId = message.body.match(/\#(.*)\#/).pop();
              //show confirmation
              angular
                .element(document.getElementById("rootBody"))
                .scope()
                .$root.showModalWindow(
                  "notification",
                  "Replacement Request",
                  "You have a replacement request, do you want to proceed?",
                  "Cancel",
                  "Proceed",
                  null,
                  proceedCallback
                );
              var notifySound = "./assets/sound/notification.mp3";
              var audio = new Audio(notifySound);
              audio.play();
            }

            function proceedCallback() {
              var orderPage = document.getElementById("orderDetails" + orderId);

              if (orderPage) {
                angular.element(orderPage).scope().init();
              } else {
                angular
                  .element(document.getElementById("rootBody"))
                  .scope()
                  .$root.redirectPage("orderdetails", {
                    orderID: orderId,
                  });
              }
            }
          }
        },
        function (error) { }
      );
    }
  }

  function getFirebaseToken(firebasePlugin) {
    firebasePlugin.getToken(
      function (token) {
        // save this server-side and use it to push notifications to this device
        //alert(token);
        if (token) {
          window.localStorage.setItem(
            "firebasedevicetoken",
            JSON.stringify(token)
          );
        } else {
          setTimeout(getFirebaseToken(firebasePlugin), 1000);
        }
      },
      function (error) {
        //alert(error);
        console.error(error);
      }
    );
  }

  $timeout(function () {
    document.addEventListener("offline", () => {
      if ($rootScope.IsOnline == undefined || $rootScope.IsOnline == true) {
        $rootScope.IsOnline = false;
        onOffline();
      }
    });

    document.addEventListener("online", () => {
      $rootScope.IsOnline = true;
      onOnline();
      // offlineSync.SyncLiveDB(function (result) {
      //   var IsSyncSuccess = result;
      //   if (IsSyncSuccess = true) {
      //     onOnlinesyncCompleate()
      //   }
      // });
    });
  });

  // document.addEventListener("offline", onOffline, false);

  function onOffline() {
    $rootScope.ShowLoader = false;
    $(".offline-tag").show();
    // Handle the offline event
    $state.go("offlinepage");
  }
  function onOnline() {
    $rootScope.ShowLoader = false;
    $(".online-sync-tag").show();
    $(".go-online-button").show();


  }
  function onOnlinesyncCompleate() {
    $rootScope.ShowLoader = false;
    $(".online-sync-tag").hide();

  }


  // document.addEventListener("online", onOnline, false);

  const networkState = navigator.connection == undefined || navigator.connection == null ? null : navigator.connection.type;
  const states = {};
  if (window.Connection) {
    states[Connection.UNKNOWN] = "Unknown connection";
    states[Connection.ETHERNET] = "Ethernet connection";
    states[Connection.WIFI] = "WiFi connection";
    states[Connection.CELL_2G] = "Cell 2G connection";
    states[Connection.CELL_3G] = "Cell 3G connection";
    states[Connection.CELL_4G] = "Cell 4G connection";
    states[Connection.CELL] = "Cell generic connection";
    states[Connection.NONE] = "No network connection";
  }
  if (window.Connection && networkState === window.Connection.NONE) {
    $rootScope.ShowLoader = false;
    $(".footer").hide();
    //$state.go("offlinepage");
  }
});