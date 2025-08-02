app.controller('RealtimeController', ['$scope', '$http', 'moment', '$compile', '$element', '$timeout', 'realtimeService',
    function ($scope, $http, moment, $compile, $element, $timeout, $subscription) {
    var vm = this

        var android = L.icon({
            iconUrl: '/img/android.svg',
            shadowUrl: '',
            iconSize: [38, 95], // size of the icon
            shadowSize: [50, 64], // size of the shadow
            iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 62],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });

        var iOS = L.icon({
            iconUrl: '/img/apple.svg',
            shadowUrl: '',
            iconSize: [38, 95], // size of the icon
            shadowSize: [50, 64], // size of the shadow
            iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 62],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });

        var order = L.icon({
            iconUrl: '/img/order.svg',
            shadowUrl: '',
            iconSize: [38, 95], // size of the icon
            shadowSize: [50, 64], // size of the shadow
            iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 62],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });

        var orderDelivered = L.icon({
            iconUrl: '/img/order-delivered.svg',
            shadowUrl: '',
            iconSize: [38, 95], // size of the icon
            shadowSize: [50, 64], // size of the shadow
            iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 62],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });

        var user = L.icon({
            iconUrl: '/img/user.svg',
            shadowUrl: '',
            iconSize: [38, 95], // size of the icon
            shadowSize: [50, 64], // size of the shadow
            iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
            shadowAnchor: [4, 62],  // the same for the shadow
            popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
        });
        var map;

        vm.init = function () {
            $timeout(function () {
                map = L.map('realtimeMap').setView([25, 51], 5);
                const mainLayer = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}.jpg', {
                    minZoom: 3,
                    maxZoom: 22,
                    maxNativeZoom: 19,
                    attribution: ''
                });

                mainLayer.addTo(map);
                L.Control.geocoder().addTo(map);
            });


            $subscription.subscribe(
                { subscribeTo: 'realtimedata', componentID: 'realtimeviewer', container: '' },
                vm.subscriptionCallBack
            )
        }

        vm.subscriptionCallBack = function (subscriptiodata) {
            debugger;
            L.marker([23.5582589, 53.403095], { icon: android }).addTo(map)
                .openPopup();
            L.marker([22.5582589, 52.403095], { icon: iOS }).addTo(map)
                .openPopup();
            L.marker([24.5582589, 52.403095], { icon: order }).addTo(map)
                .openPopup();
            L.marker([21.5582589, 52.403095], { icon: orderDelivered }).addTo(map)
                .openPopup();
            L.marker([23.5582589, 52.403095], { icon: user }).addTo(map)
                .openPopup();
        }

        vm.adjustWidth = function (event) {
            $(event.currentTarget).css('width', '100%');
            $(event.currentTarget).css('height', '100%');
        }
  }])
