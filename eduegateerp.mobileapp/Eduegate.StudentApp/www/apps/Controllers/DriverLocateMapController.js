app.controller("DriverLocationController", ["$scope", "$http", "rootUrl", "$location", "GetContext", "$state", "$stateParams", "$rootScope", "loggedIn", "$sce", "$timeout", "$translate", function ($scope, $http, rootUrl, $location, GetContext, $state, $stateParams, $rootScope, loggedIn, $sce, $timeout, $translate) {

  var context = GetContext.Context();
  $scope.nearestplaces = [];
  var dataService = rootUrl.SchoolServiceUrl;
  var appDataUrl = rootUrl.RootUrl;
  $scope.StudentID = $stateParams.studentID;;
  $scope.StopsPositionsByRoute = [];



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
  $scope.GetSettingValues = function () {
    $http({
      method: 'GET',
      url: appDataUrl + '/GetSettingValueByKey?settingKey=' + 'DEFAULT_SCHOOL_LATITUDE',
      headers: {
        "Accept": "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).success(function (result) {

      $scope.Default_School_Latitude = result;
    }).error(function () {
    });

    $http({
      method: 'GET',
      url: appDataUrl + '/GetSettingValueByKey?settingKey=' + 'DEFAULT_SCHOOL_LONGITUDE',
      headers: {
        "Accept": "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).success(function (result) {

      $scope.Default_School_Longitde = result;
    }).error(function () {
    });
  }

  $scope.initDriverMapLocationPicker = function (studentID) {
    // $scope.GetDriverDetails(orderID);

    $http({
      url: dataService + '/GetDriverGeoLocationByStudent?studentID=' + $scope.StudentID,
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
          center: new google.maps.LatLng($scope.Default_School_Latitude, $scope.Default_School_Longitde),
          mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map;
        map = new google.maps.Map(document.getElementById('drivermap'),
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
          heading: 320,
          tilt: 47.5,
          mapTypeId: google.maps.MapTypeId.ROADMAP,
          mapTypeControl: false, // Disable map type controls (removes satellite view)
          // Optional: Additional UI options can be customized here
          fullscreenControl: true,   // Keep fullscreen control (optional)
        };
        var noPoi = [
          {
            featureType: "poi",
            stylers: [
              { visibility: "off" }
            ]
          }
          , {
            featureType: "all",
            stylers: [
              { saturation: -80 }
            ]
          },
          {
            featureType: "road.arterial",
            elementType: "geometry",
            stylers: [
              { hue: "#00ffee" },
              { saturation: 50 }
            ]
          }, {
            featureType: "poi.business",
            elementType: "labels",
            stylers: [
              { visibility: "off" }
            ]
          },
          {
            "elementType": "labels.text.fill",
            "stylers": [
              { "color": "#dddddd" }, // Text color
              { "visibility": "on" }, // Text visibility
              { "weight": 0.1 },      // Text weight
              { "gamma": 0.5 }        // Text gamma
            ]
          },
          {
            "elementType": "labels.text.stroke",
            "stylers": [
              { "color": "#ffffff" }, // Text stroke color
              { "visibility": "on" } // Text stroke visibility
            ]
          }

        ];

        var map = new google.maps.Map(document.getElementById('drivermap'),
          mapOptions);
        map.setOptions({ styles: noPoi });


        // getPlaces(pos);
        $scope.DriverMap(map);
      }

    });
  }



  $scope.DriverMap = function (map, oldPos) {
    $http({
      url: `${dataService}/GetDriverGeoLocationByStudent?studentID=${$scope.StudentID}`,
      method: 'GET',
      headers: {
        "Accept": "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        "CallContext": JSON.stringify(context)
      }
    }).then(handleSuccess)
      .catch(handleError);

    function handleSuccess(response) {
      if (!response.data) {
        showErrorMessage('NOTAVAILABLEFORTRACKING');
        return;
      }

      const driverLocation = response.data.split(',');
      const newPos = { lat: parseFloat(driverLocation[0]), lng: parseFloat(driverLocation[1]) };
      updateOrMoveMarker(newPos);
      checkStopArrivals(newPos);
      setTimeout(() => $scope.DriverMap(map, newPos), 10000);
    }

    function handleError(error) {
      console.error('Error fetching driver location:', error);
      showErrorMessage('ERROR_FETCHING_LOCATION');
    }

    function showErrorMessage(messageKey) {
      $translate([messageKey]).then(translation => {
        $rootScope.ShowToastMessage(translation[messageKey]);
      });
    }

    async function updateOrMoveMarker(newPos) {
      const uluru = { lat: newPos.lat, lng: newPos.lng };
      let rotation = 0;

      if ($scope.marker) {
        if ($scope.oldPosition && !isNaN($scope.oldPosition.lat) && !isNaN($scope.oldPosition.lng)) {
          // Check if the positions are different
          if ($scope.oldPosition.lat !== uluru.lat || $scope.oldPosition.lng !== uluru.lng) {
            rotation = getRotationAngle($scope.oldPosition, uluru);
          }
        }

        // Smoothly animate the marker position with rotation
        await animateMarker($scope.marker, $scope.oldPosition, newPos, 5000, rotation);
      } else {
        // Create a new marker
        if ($scope.marker) {
          $scope.marker.setMap(null);
        }

        if ($scope.oldPosition) {
          // Check if the positions are different
          if ($scope.oldPosition.lat !== uluru.lat || $scope.oldPosition.lng !== uluru.lng) {
            rotation = getRotationAngle($scope.oldPosition, uluru);
          }
        }

        // Create rotated icon and set it
        const iconUrl = await createRotatedIcon(40, rotation);
        $scope.marker = new google.maps.Marker({
          position: uluru,
          map: map,
          icon: {
            url: iconUrl,
            scaledSize: new google.maps.Size(40, 40),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(20, 20)
          },
          draggable: true
        });

        map.setCenter(newPos); // Center map on the marker's position
      }

      // Update the stored old position
      $scope.oldPosition = { lat: newPos.lat, lng: newPos.lng };
    }

    async function animateMarker(marker, startPos, endPos, duration, finalRotation) {
      const start = performance.now();
      const startLatLng = new google.maps.LatLng(startPos.lat, startPos.lng);
      const endLatLng = new google.maps.LatLng(endPos.lat, endPos.lng);

      const animate = async (time) => {
        const elapsed = time - start;
        const progress = Math.min(elapsed / duration, 1);

        const lat = startPos.lat + (endPos.lat - startPos.lat) * progress;
        const lng = startPos.lng + (endPos.lng - startPos.lng) * progress;
        const position = new google.maps.LatLng(lat, lng);

        marker.setPosition(position);
        map.panTo(position);

        // Only calculate rotation if positions are different
        if (startPos.lat !== endPos.lat || startPos.lng !== endPos.lng) {
          const intermediateRotation = getRotationAngle(startPos, { lat, lng });
          const iconUrl = await createRotatedIcon(40, intermediateRotation);
          marker.setIcon({
            url: iconUrl,
            scaledSize: new google.maps.Size(40, 40),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(20, 20)
          });
        }

        if (progress < 1) {
          requestAnimationFrame(animate);
        } else {
          // Apply the final rotation once animation completes
          if (startPos.lat !== endPos.lat || startPos.lng !== endPos.lng) {
            const finalIconUrl = await createRotatedIcon(40, finalRotation);
            marker.setIcon({
              url: finalIconUrl,
              scaledSize: new google.maps.Size(40, 40),
              origin: new google.maps.Point(0, 0),
              anchor: new google.maps.Point(20, 20)
            });
          }
        }
      };

      return new Promise((resolve) => {
        requestAnimationFrame(animate);
        setTimeout(resolve, duration);
      });
    }



    function getRotationAngle(p1, p2) {
      const deltaY = p1.lat - p2.lat; // Invert the y-axis
      const deltaX = p2.lng - p1.lng;
      const angle = Math.atan2(deltaY, deltaX) * (180 / Math.PI);
      return (angle + 360) % 360; // Normalize angle to [0, 360) degrees
    }

    async function createRotatedIcon(size, rotation) {
      // Create a canvas element
      const canvas = document.createElement('canvas');
      const context = canvas.getContext('2d');

      // Set canvas size
      canvas.width = size;
      canvas.height = size;

      // Draw your icon image onto the canvas
      const iconImage = await loadImage('./images/transport/transport.png'); // Load your icon image
      context.translate(size / 2, size / 2); // Move the origin to the center of the canvas
      context.rotate((rotation * Math.PI) / 180); // Rotate the canvas
      context.drawImage(iconImage, -size / 2, -size / 2, size, size); // Draw the image

      // Convert the canvas to a data URL
      const iconUrl = canvas.toDataURL();

      return iconUrl; // Return the URL of the rotated icon
    }

    // Helper function to load an image
    function loadImage(src) {
      return new Promise((resolve, reject) => {
        const img = new Image();
        img.onload = () => resolve(img);
        img.onerror = reject;
        img.src = src;
      });
    }



    function checkStopArrivals(driverPos) {
      $scope.StopsPositionsByRoute.forEach(stop => {
        const latLng = new google.maps.LatLng(parseFloat(stop.Latitude), parseFloat(stop.Longitude));
        const distance = google.maps.geometry.spherical.computeDistanceBetween(driverPos, latLng);
        if (distance < 50) {
          stop.arrived = true;
        }
      });
    }
  }
  $scope.GetStopsPositionsByRoute = function () {
    $http({
      url:
        dataService +
        '/GetStopsPositionsByRoute?studentID=' + $scope.StudentID,
      method: "GET",
      headers: {
        Accept: "application/json;charset=UTF-8",
        "Content-type": "application/json; charset=utf-8",
        CallContext: JSON.stringify(context),
      },
    }).then(function (result) {
      if (result) {
        result = result.data;
        $scope.StopsPositionsByRoute = result;
      }
    });
  }


  $scope.GetSettingValues();
  $scope.GetStopsPositionsByRoute();
  $scope.initDriverMapLocationPicker($scope.StudentID);

}]);