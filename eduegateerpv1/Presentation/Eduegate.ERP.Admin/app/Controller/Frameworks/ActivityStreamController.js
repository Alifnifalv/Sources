app.controller('ActivityStreamController', ['$scope', '$http', 'moment', 'alert', '$compile', 'subscriptionService',
    function ($scope, $http, moment, alert, $compile, $subscription) {
        var vm = this;

        vm.ActivityData = [];
        vm.WindowContainer = null;
        //vm.ActivityData.push({});

        vm.Init = function (window) {
            vm.WindowContainer = '#' + window;
            $subscription.subscribe(
                { 'subscribeTo': 'newactivity', 'componentID': 'Activity', 'container': '' },
                vm.subscriptionCallBack
            );
            vm.LoadActivityData();
        }

        vm.subscriptionCallBack = function (subscriptiodata) {
            $.each(subscriptiodata, function (index, data) {
                vm.ActivityData.splice(0, 0, data);
            });
        }

        vm.fromNow = function (date) {
            return moment(date).fromNow();
        }

        vm.LoadActivityData = function () {
            //$('.preload-overlay', $(vm.WindowContainer)).show();

            $.ajax({
                url: 'Activity/GetActivities',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.ActivityData = data;
                        $('.waypoint', $(vm.WindowContainer)).hide();
                    });
                }
            });
        }
    }]);