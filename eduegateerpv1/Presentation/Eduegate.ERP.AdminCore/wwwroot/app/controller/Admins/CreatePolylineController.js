app.controller('CreatePolylineController', ['$scope', '$compile', '$http', '$timeout', 'productService', 'purchaseorderService', 'accountService', '$controller', 'NgMap', '$rootScope',
    function ($scope, $compile, $http, $timeout, productService, purchaseorderService, accountService, $controller, NgMap, $root) {
        console.log('DriverLocationController')
        var vm = this;
        vm.SchoolSettingPath = [];
        vm.Schools = [];
        vm.Branches = [];
        vm.SelectedSchoolTypeID = null;
        vm.BranchID = null;
        vm.AreaID = null;
        vm.map = null;

        vm.message = 'You can not hide. :)'

        NgMap.getMap('map').then(function (map) {
            vm.map = map
            vm.LoadSchoolSetting(null, vm.map);
            $timeout(function () {
                if (vm.map && vm.SchoolSettingPath && vm.SchoolSettingPath.length > 0) {
                    var location = new google.maps.LatLng(vm.SchoolSettingPath[0][0], vm.SchoolSettingPath[0][1]);
                    vm.map.setCenter(location);
                    vm.map(24);
                }
            });
        });

        vm.SetPositions = function (driverPosition) {
            vm.DriverPositions = angular.copy(driverPosition)
        }

        vm.addMarkerAndPath = function (event) {
            vm.SchoolSettingPath.push([event.latLng.lat(), event.latLng.lng()]);
        };

        vm.LoadSchoolSetting = function (schoolID, googleMap) {
            $timeout(function () {
                $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
            });

            if (!schoolID)
                return;

            $('.preload-overlay', $('#CreateCreatePolyline')).fadeIn();
            if (googleMap) {
                googleMap.shapes.polyline.getPath().clear();
            }
            $.ajax({
                url: '/Schools/School/GetGeoSchoolSetting?schoolID=' + schoolID,
                type: 'GET',
                success: function (result) {
                    $scope.$apply(function () {
                        vm.SchoolSettingPath = [];
                        result.forEach(function (value) {
                            vm.SchoolSettingPath.push([value.Latitude, value.Longitude]);
                        });

                        $timeout(function () {
                            if (googleMap && vm.SchoolSettingPath && vm.SchoolSettingPath.length > 0) {
                                googleMap.setCenter(vm.SchoolSettingPath[0][0], vm.SchoolSettingPath[0][1],7);
                            }
                        });

                        if (googleMap && googleMap.shapes) {
                            googleMap.shapes.polyline.setMap(vm.SchoolSettingPath);
                        }

                        $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
                    });
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText)
                }
            });
        }

        $.ajax({
            url: 'Mutual/GetLookUpData?lookType=School',
            type: 'GET',
            success: function (result) {
                vm.Schools = result;
            },
            error: function (request, status, message, b) {
                $().showGlobalMessage($root, $timeout, true, request.responseText)
            }
        });

        vm.SaveSchoolSetting = function (schoolID) {
            $('.preload-overlay', $('#CreateCreatePolyline')).fadeIn();
            var data = [];
            vm.SchoolSettingPath.forEach(function (value) {
                data.push({
                    'SchoolID': schoolID,
                    'ReferenceID1': schoolID,
                    'Latitude': value[0],
                    'Longitude': value[1],
                });
            });

            $.ajax({
                url: utility.myHost + 'Schools/School/SaveGeoSchoolSetting',
                type: 'POST',
                data: { 'geoSettings': data },
                success: function (result) {
                    $().showGlobalMessage($root, $timeout, false, "Successfully saved the geolocation.");
                    $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText ? request.responseText
                        : "Error occured while saving the polygon.");
                    $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
                }
            });
        };

        vm.ClearSchoolSetting = function (schoolID) {
            var $preloadOverlay = $('.preload-overlay', $('#CreateCreatePolyline'));
            $preloadOverlay.fadeIn();

            $.ajax({
                url: utility.myHost + 'Schools/School/ClearGeoSchoolSetting',
                type: 'GET',
                data: { schoolID: schoolID },
                success: function (result) {
                    vm.SchoolSettingPath = [];
                    $().showGlobalMessage($root, $timeout, false, "Successfully cleared the geolocation.");
                    $preloadOverlay.fadeOut();
                    vm.LoadSchoolSetting(schoolID, vm.map);
                },
                error: function (request, status, message) {
                    var errorMessage = request.responseText ? request.responseText : "Error occurred while deleting the polygon.";
                    $().showGlobalMessage($root, $timeout, true, errorMessage);
                    vm.SchoolSettingPath = [];
                    $preloadOverlay.fadeOut();
                    vm.LoadSchoolSetting(schoolID, vm.map);
                }
            });
        };

        vm.currentIndex = 0

        vm.getSchoolPath = function (param) {
            $('.preload-overlay', $('#CreateCreatePolyline')).fadeIn();

            $.ajax({
                url: utility.myHost + 'Schools/School/CurrentLocations',
                type: 'GET',
                success: function (result) {
                    vm.SetPositions(result.Locations);
                    $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText);
                    $('.preload-overlay', $('#CreateCreatePolyline')).fadeOut();
                }
            })
        }

        /*vm.LoadSchoolSetting(null, vm.map);*/
    }]);
