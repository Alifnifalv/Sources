var app = angular.module("EduegateApp", [
  "ui.router",
  "ngSanitize",
  "angular.filter",
  "ngTouch",
  "pascalprecht.translate",
  "indexedDB",
  'bc.Flickity',
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
      url: "/classstudents?ID?studentID",
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
    })
    .state("homebanner", {
      url: "/homebanner",
      templateUrl: "partials/home-single-banner.html",
      controller: 'HomeSingleBannerController'
    })
    .state("vehicletracking", {
      url: "/vehicletracking",
      templateUrl: "partials/vehicletracking.html",
      controller: 'VehicleTrackingController'
    })
    .state('driverlocation', {
      url: "/driverlocation:employeeID",
      templateUrl: "partials/driverlocation.html",
      controller: 'DriverLocationController',
      params: {
        employeeID: null,
      }
    })
    .state('salaryslip', {
      url: "/salaryslip:employeeID",
      templateUrl: "partials/salaryslip.html",
      controller: 'SalarySlipController',
      params: {
        employeeID: null,
      }
    })
    .state('biometricauthentication', {
      url: "/biometricauthentication",
      templateUrl: "partials/biometricauthentication.html",
      controller: 'BiometricAuthenticationController',
    })
    .state('inbox', {
      url: "/inbox",
      templateUrl: "partials/inbox.html",
      controller: 'InboxController',
    })
    .state('message', {
      url: "/message:RecieverID?ParentName?StudentID?StudentName",
      templateUrl: "partials/message.html",
      controller: 'MessageController',
      params: {
        RecieverID: null,
        ParentName: null,
        StudentID: null,
        StudentName: null,
      }
    })
    .state('createannouncement', {
      url: "/createannouncement",
      templateUrl: "partials/createannouncement.html",
      controller: 'CreateAnnouncementController',
    })
    .state('apponboarding', {
      url: "/apponboarding",
      templateUrl: "partials/apponboarding.html",
      controller: 'AppOnboardingController'
    })

    .state('broadcast', {
      url: "/broadcast:ListName?BroadcastName?BroadcastID",
      templateUrl: "partials/broadcast.html",
      controller: 'BroadcastController',
      params: {
        ListName: null,
        BroadcastName: null,
        BroadcastID: null,
      }
    })

    .state('financialhealthdashbaord', {
      url: "/financialhealthdashbaord",
      templateUrl: "partials/financialhealthdashbaord.html",
      controller: 'FinancialHealthDashboardController',
    })

    .state('academicperformancedashbaord', {
      url: "/academicperformancedashbaord",
      templateUrl: "partials/academicperformancedashbaord.html",
      controller: 'AcademicPerformanceDashboardController',
    })

    .state('staffingandhrdashbaord', {
      url: "/staffingandhrdashbaord",
      templateUrl: "partials/staffingandhrdashbaord.html",
      controller: 'StaffingAndHRDashboardController',
    })

    .state('liverevenuedashbaord', {
      url: "/liverevenuedashbaord",
      templateUrl: "partials/liverevenuedashbaord.html",
      controller: 'LiveRevenueDashboardController',
    })

    .state('studentdashbaord', {
      url: "/studentdashbaord",
      templateUrl: "partials/studentdashbaord.html",
      controller: 'StudentDashbaordController',
    })
    .state('editbroadcast', {
      url: "/editbroadcast:BroadcastID",
      templateUrl: "partials/editbroadcast.html",
      controller: 'EditBroadcastController',
      params: {
        BroadcastID: null,
      }
    })
      // 1. Classes list page
    .state('attendanceclasses', {
      url: '/attendanceclasses',
      templateUrl: 'partials/attendanceclasses.html',
      controller: 'AttendanceClassesController'
    })

    // 2. Students list in class
    .state('attendancestudents', {
      url: '/attendancestudents:classId?sectionId',
      templateUrl: 'partials/attendancestudents.html',
      controller: 'AttendanceStudentsController',
      params: {
        classId: null,
        sectionId: null
      }
    })

    // 3. Student attendance details
    .state('attendancestudentdetail', {
      url: '/attendancestudentdetail:studentID?studentName',
      templateUrl: 'partials/attendancestudentdetail.html',
      controller: 'AttendanceStudentDetailController',
      params: {
        studentID: null,
        studentName: null,
      }
    })
    .state('topic', {
      url: "/topic?ID?studentID",
      templateUrl: "partials/topic.html",
      controller: 'TopicController'
    })
    .state('studentleaverequest', {
      url: "/studentleaverequest",
      templateUrl: "partials/studentleaverequest.html",
      controller: 'StudentLeaveRequestController'
    })
    .state('studentearlypickup', {
      url: "/studentearlypickup",
      templateUrl: "partials/studentearlypickup.html",
      controller: 'StudentEarlyPickupController'
    })
    ;

});


app.factory("clientSettings", function ($http) {
  var client = "pearl";
  // var client = "eduegate";
  return clientSetting[client];
});


app.factory('rootUrl', function ($http, clientSettings) {
  // var environment = "live";
  // var environment = "staging";
  // var environment = "test";
  // var environment = "linux";
  var environment = "local";

  var urls = appSettings[clientSettings.Client + '-' + environment];
  return {
    RootUrl: urls.RootUrl,
    SchoolServiceUrl: urls.SchoolServiceUrl,
    PowerBIServiceUrl: urls.PowerBIServiceUrl,
    SecurityServiceUrl: urls.SecurityServiceUrl,
    UserServiceUrl: urls.UserServiceUrl,
    ContentServiceUrl: urls.ContentServiceUrl,
    CommunicationServiceUrl: urls.CommunicationServiceUrl,
    SignalRhubURL: urls.SignalRhubURL,
    ErpUrl: urls.ErpUrl,
    ParentUrl: urls.ParentUrl,
    CurrentAppVersion: urls.CurrentAppVersion,
    AppID: clientSettings.AppID,


    ImageUrl: "",
    ErrorProductImageUrl: 'clients/' + clientSettings.Client + '/img/no-image.svg',
    ErrorHomePageImageUrl: 'clients/' + clientSettings.Client + '/img/no-image.svg',
    BigErrorImageUrl: 'clients/' + clientSettings.Client + '/img/no-image.svg',
    client: clientSettings.Client
  };
});

app.config(function ($translateProvider) {
  $translateProvider.useLoader("ClientTranslationLoader");
  $translateProvider.preferredLanguage("en");
});

app.factory("ClientTranslationLoader", function ($q, $http, clientSettings) {
  return function (options) {
    var deferred = $q.defer();

    var lang = options.key || "en";
    var clientCode = clientSettings?.Client || "default";

    var clientUrl = "clients/" + clientCode + "/lang/" + lang + ".json";
    var defaultUrl = "scripts/jquery-localize/" + lang + ".json";

    // Load both translations
    $q.all([
      $http.get(defaultUrl).catch(() => ({ data: {} })), // load default or empty object if fail
      $http.get(clientUrl).catch(() => ({ data: {} }))   // load client or empty object if fail
    ]).then(function (responses) {
      var defaultTranslations = responses[0].data || {};
      var clientTranslations = responses[1].data || {};

      // Merge client over default (client keys overwrite default keys)
      var mergedTranslations = Object.assign({}, defaultTranslations, clientTranslations);

      deferred.resolve(mergedTranslations);
    }).catch(function (error) {
      deferred.reject("Error loading translations: " + error);
    });

    return deferred.promise;
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
app.filter('newlineToBr', function () {
  return function (text) {
    return text ? text.replace(/\n/g, '<br/>') : '';
  };
});

app.filter('trustedHtml', ['$sce', function ($sce) {
  return function (text) {
    return $sce.trustAsHtml(text);
  };
}]);

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

var appState = {
  takingPicture: true,
  // uploadingPicture:true,
  imageUri: ""
};


var APP_STORAGE_KEY = "exampleAppState";


app.run(function ($state, $rootScope, $timeout, offlineSync, LocationService) {
  document.addEventListener("deviceready", onDeviceReady, false);
  document.addEventListener('pause', onPause, false);
  document.addEventListener('resume', onResume, false);

  function onDeviceReady() {

    if (localStorage.getItem("isDriver") === null) {
      localStorage.setItem("isDriver", "false");
      console.log("isDriver not set. Defaulting to false.");
  }
    // Load the saved setting from local storage
    var isBiometricEnabled = localStorage.getItem("biometricEnabled") === "true";
    var biometricSwitch = document.getElementById("biometricSwitch"); // Ensure biometricSwitch is defined

    if (biometricSwitch) {
      biometricSwitch.checked = isBiometricEnabled;
      biometricSwitch.addEventListener("change", function () {
        localStorage.setItem("biometricEnabled", biometricSwitch.checked);
      });
    }

    FastClick.attach(document.body);
    window.open = cordova.InAppBrowser ? cordova.InAppBrowser.open : null;

    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
    if (iOS) {
      $("body").addClass("platform-ios");
    }

    initializeFireBase();
    document.addEventListener("backbutton", onBackKeyDown, false);

    if (isBiometricEnabled) {
      showBiometricAuthentication();
    }

    // Assume this value is determined during login or initialization
    var isDriver = localStorage.getItem("isDriver") === "true";

    if (isDriver) {
      console.log("User is a driver. Enabling background location tracking.");

      // Configure the plugin
      BackgroundGeolocation.configure({
        locationProvider: BackgroundGeolocation.DISTANCE_FILTER_PROVIDER, // Use activity-based location updates
        desiredAccuracy: BackgroundGeolocation.HIGH_ACCURACY,      // Set desired accuracy
        stationaryRadius: 10,                                      // Distance in meters for stationary updates
        distanceFilter: 50,                                        // Send updates every 50 meters
        interval: 5000,                                           // Update interval when app is in foreground (ms)
        fastestInterval: 2000,                                     // Minimum interval for updates (ms)
        activitiesInterval: 7000,                                 // Interval for activity-based updates (ms)
        stopOnTerminate: true,                                    // Continue tracking after app termination
        startOnBoot: true,                                         // Start tracking on device boot
      });

      // Start tracking
      BackgroundGeolocation.start();

      // Listen for location updates
      BackgroundGeolocation.on('location', function (location) {
        LocationService.sendLocationToServer(location)
          .then(response => {
            console.log('Location successfully sent:', response);
          })
          .catch(error => {
            console.error('Error sending location:', error);
          });
      });

      // Listen for errors
      BackgroundGeolocation.on('error', function (error) {
        // console.error('BackgroundGeolocation error:', error);
      });
    } else {
      console.log("User is not a driver. Background location tracking is disabled.");
    }
    if (cordova && cordova.plugins && cordova.plugins.CorHttpd) {
      cordova.plugins.CorHttpd.startServer({
        'www_root': '', // Correct
        'port': 8080,
        'localhost_only': false
      }, function (url) {
        console.log("Local server started at: " + url);
        window.localServerUrl = url;
      }, function (error) {
        console.error("Failed to start server: ", error);
      });
      
    }
  }

  function onPause() {
    if (appState.takingPicture || appState.imageUri) {
      window.localStorage.setItem(APP_STORAGE_KEY, JSON.stringify(appState));
    }
  }

  function onResume(event) {
    var storedState = window.localStorage.getItem(APP_STORAGE_KEY);
    if (storedState) {
      appState = JSON.parse(storedState);
    }

    if (!appState.takingPicture && appState.imageUri) {
      document.getElementById("get-picture-result").src = appState.imageUri;
    } else if (appState.takingPicture && event.pendingResult) {
      if (event.pendingResult.pluginStatus === "OK") {
        onSuccess(event.pendingResult.result);
      } else {
        onFail(event.pendingResult.result);
      }
    } else {
      return false;
    }
  }



  function onSuccess(imageUri) {
    appState.takingPicture = false;
    appState.imageUri = imageUri;
    document.getElementById("get-picture-result").src = imageUri;
  }

  function onFail(error) {
    appState.takingPicture = false;
    console.error("Camera error: ", error);
  }

  function showBiometricAuthentication() {
      $state.go("biometricauthentication");

    Fingerprint.isAvailable(isAvailableSuccess, isAvailableError);

    function isAvailableSuccess(result) {
      $state.go("biometricauthentication");

      var authType = (result === 'face' || result === 'biometric') ? 'Face ID' : 'Fingerprint';

      Fingerprint.show({
        description: "Please authenticate to access the app",
        disableBackup: true // Optional, disable backup authentication methods (like password)
      }, successCallback, errorCallback);

      function successCallback() {
        // alert(authType + " Authentication successful");
        // Proceed with normal app initialization here
        $state.go("home");

      }

      function errorCallback(error) {
        // alert(authType + " Authentication failed: " + error.message);
        // Optionally, you can close the app or restrict access here
        // navigator.app.exitApp(); // This will close the app
        $state.go("biometricauthentication");

      }
    }

    function isAvailableError(error) {
      // alert("Biometric authentication not available: " + error.message);
      // Optionally, you can close the app or restrict access here
      // navigator.app.exitApp(); // This will close the app
    }
  }

  function onBackKeyDown() {
    var modalOpen = document.querySelector(".modal.show");

    if ($state.is('home') || $state.is('biometricauthentication')) {
      // e.preventDefault();
      navigator.notification.confirm("Are you sure you want to exit?", onConfirm, "Please Confirm", "Yes,No");
    }

    else if (modalOpen) {
      e.preventDefault(); // prevent default back action
      $(modalOpen).modal("hide"); // close modal
    } 
    else {
      navigator.app.backHistory();
    }
  }
  function onConfirm(button) {
    if (button == 2) {//If User selected No, then we just do nothing
      return;
    } else {
      navigator.app.exitApp();// Otherwise we quit the app.
    }
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
// App State
var appState = {
  takingPicture: false,
  imageUri: ""
};

var APP_STORAGE_KEY = "exampleAppState";