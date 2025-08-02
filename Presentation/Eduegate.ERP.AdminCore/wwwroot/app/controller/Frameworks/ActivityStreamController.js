app.controller('ActivityStreamController', ['$scope', '$http', 'moment', '$compile', 'subscriptionService',
    function ($scope, $http, moment, $compile, $subscription) {
        var vm = this

        vm.ActivityData = []

        vm.WindowContainer = null

        vm.Init = function (window) {
            vm.WindowContainer = '#' + window;

            if ($subscription && $subscription.subscribe) {
                $subscription.subscribe(
                    { subscribeTo: 'newactivity', componentID: 'Activity', container: '' },
                    vm.subscriptionCallBack
                );
            }

            vm.LoadActivityData();
            vm.LoadRunningActivityData();
        }

        vm.subscriptionCallBack = function (subscriptiodata) {
            $.each(subscriptiodata, function (index, data) {
                $scope.$apply(function () {
                    if (!vm.ActivityData) vm.ActivityData = [];
                    vm.ActivityData.splice(0, 0, data)
                });
            })
        }

        vm.fromNow = function (date) {
            return moment(date).fromNow()
        }

        vm.LoadActivityData = function () {
            $.ajax({
                url: 'Activity/GetActivities',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.ActivityData = data
                        $('.waypoint', $(vm.WindowContainer)).hide()
                    })
                }
            })
        }

        vm.RunningActivityDatas = []

        vm.LoadRunningActivityData = function () {
            $.ajax({
                url: 'Activity/GetRunningActivities',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.RunningActivityDatas = data;
                        $('.waypoint', $(vm.WindowContainer)).hide()
                    })
                }
            })
        }

        vm.ScheduledActivityDatas = [];

        vm.LoadScheduledActivityData = function () {
            $.ajax({
                url: 'Activity/GetScheduledActivities',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.ScheduledActivityDatas = data;
                        $('.waypoint', $(vm.WindowContainer)).hide()
                    })
                }
            })
        }

        vm.ErrroDatas = [];

        vm.LoadErrors = function () {
            $.ajax({
                url: 'Error/Recent?count=10&type=json',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.ErrroDatas = JSON.parse(data);
                        $('.waypoint', $(vm.WindowContainer)).hide()
                    })
                }
            })
        }

    }])
