app.controller("DriverLocationController", ["$scope", "$http", "rootUrl", "$location", "GetContext", "$state", "$stateParams", "$rootScope", "loggedIn", "$sce", "$timeout", "$translate", function ($scope, $http, rootUrl, $location, GetContext, $state, $stateParams, $rootScope, loggedIn, $sce, $timeout, $translate) {
    
  var context = GetContext.Context();
  $scope.nearestplaces = [];
  var dataService = rootUrl.SchoolServiceUrl;

  // $scope.initDriverMapLocationPicker = function () {
  //   var mapOptions = {
  //     zoom: 7,
  //     center: new google.maps.LatLng(25.36929, 51.4960124),
  //     mapTypeId: google.maps.MapTypeId.ROADMAP,
  //   };
  //   var map = new google.maps.Map(
  //     document.getElementById("drivermap"),
  //     mapOptions
  //   );

  // //  $scope.GetDriverDetails();

  //   $http({
  //     url:
  //       dataService +
  //       "/GetDriverGeoLocation",
  //     method: "GET",
  //     headers: {
  //       Accept: "application/json;charset=UTF-8",
  //       "Content-type": "application/json; charset=utf-8",
  //       CallContext: JSON.stringify(context),
  //     },
  //   }).then(function (result) {
  //     if (!result.data) {
  //       $translate(["NOTAVAILABLEFORTRACKING"]).then(function (translation) {
  //         $rootScope.ShowToastMessage(translation.NOTAVAILABLEFORTRACKING);
  //       });          
  //       var GeoMarker = new GeolocationMarker(map);
  //       return;
  //     } else {
  //       var driverLocation = result.data.split(",");
  //       var pos = {
  //         lat: driverLocation[0],
  //         lng: driverLocation[1],
  //       };
  //       var mapOptions = {
  //         zoom: 17,
  //         center: new google.maps.LatLng(pos.lat, pos.lng),
  //         mapTypeId: google.maps.MapTypeId.ROADMAP,
  //       };
  //       var map = new google.maps.Map(
  //         document.getElementById("drivermap"),
  //         mapOptions
  //       );
  //       var GeoMarker = new GeolocationMarker(map);
  //       // getPlaces(pos);
  //     }
  //   }, function() {

  //   });
  // };

  $scope.GetDriverDetails = function () {
    $http({
      url:
        dataService +
        "/GetDriverDetails",
      method: "GET",
      headers: {
        Accept: "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        CallContext: JSON.stringify(context),
      },
    }).then(function (result) {
      if (result) {
        result = result.data;
        $scope.DriverDetails = result;
      }
    });
  };

  function getPlaces(position) {

    $.ajax({
      beforeSend: function (jqXHR, settings) {
        jqXHR.setRequestHeader('custom-xid-header', null);
      },
      url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng='
        + position.lat + "," + position.lng
        + "&key=" + rootUrl.GoogleAPIKey,
      dataType: 'json',
      success: function (r) {
        $scope.$apply(function () {
          if (r && r.results[0]) {
            $scope.driverPlaces = r.results[0].formatted_address
          }
          else {

            $rootScope.ErrorMessage = "Location unavailable ";
            $(".error-msg").removeClass('showMsg');
            $(".error-msg").addClass('showMsg').delay(2000).queue(function () {
              $(this).removeClass('showMsg');
              $(this).dequeue();
            });
          }
          $rootScope.ShowLoader = false;
        });
      },
      error: function (e) {
        $rootScope.ShowLoader = false;
      }
    });
  }


  $scope.initDriverMapLocationPicker = function (orderID) {
    // $scope.GetDriverDetails(orderID);

    $http({
      url: dataService + '/GetDriverGeoLocation',
      method: 'GET',
      headers: {
        "Accept": "application/json;charset=UTF-8", "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).then(function (result) {
      // Try HTML5 geolocation.
      if (!result.data) {
        $translate(['NOTAVAILABLEFORTRACKING']).then(function (translation) {
          $rootScope.ShowToastMessage(translation.NOTAVAILABLEFORTRACKING);
        });

        var mapOptions = {
          zoom: 7,
          center: new google.maps.LatLng(23.652246, 53.705974),
          mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('drivermap'),
          mapOptions);
        var GeoMarker = new GeolocationMarker(map);
        return;

      } else {
        var driverLocation = result.data.split(',');
        var pos = {
          lat: driverLocation[0],
          lng: driverLocation[1]
        };
        var mapOptions = {
          zoom: 17,
          center: new google.maps.LatLng(pos.lat, pos.lng),
          mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('drivermap'),
          mapOptions);
        // getPlaces(pos);
        $scope.DriverMap(map);
      }

    });
  }

  $scope.DriverMap = function (map, oldpos) {

    $http({
      url: dataService + '/GetDriverGeoLocation',
      method: 'GET',
      headers: {
        "Accept": "application/json;charset=UTF-8", "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).then(function (result) {
      // Try HTML5 geolocation.
      if (!result.data) {
        $translate(['NOTAVAILABLEFORTRACKING']).then(function (translation) {
          $rootScope.ShowToastMessage(translation.NOTAVAILABLEFORTRACKING);
        });

      } else {
        if ($scope.marker) {
          $scope.marker.setMap(null);
        }
        var driverLocation = result.data.split(',');
        var pos = {
          lat: driverLocation[0],
          lng: driverLocation[1]
        };
        //var GeoMarker = new GeolocationMarker(map);
        var uluru = { lat: parseFloat(pos.lat), lng: parseFloat(pos.lng) };
        $scope.marker = new google.maps.Marker({
          position: uluru, map: map,
          icon: './images/transport.png'
        });
        if (pos.lat != (oldpos ? oldpos.lat : null)) {
          // getPlaces(pos);
        }
        setTimeout(function () {
          $scope.DriverMap(map, pos);
        }, 5000);

      }
    });

  }

  $scope.initDriverMapLocationPicker();
  
}]);