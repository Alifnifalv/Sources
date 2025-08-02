app.controller('ActivityController', ['$scope', '$http', 'moment', 'alert', '$compile', 'subscriptionService',
    function ($scope, $http, moment, alert, $compile, $subscription) {
        var vm = this;        

        vm.ActivityData = [];
        vm.WindowContainer = null;
        //vm.ActivityData.push({});
        var pagination = new endlessPagination();

        vm.Init = function (window) {
            vm.WindowContainer = '#' + window;
            $subscription.subscribe(
                { 'subscribeTo': 'newactivity', 'componentID': 'Activity', 'container': '' },
                vm.subscriptionCallBack
            );

            pagination.initialize('activityStream', false, 'ActivityIID',
                '.overlaySummaryInnerview', $('#activityWayPoint'));

            vm.LoadActivityData();
        }

        vm.subscriptionCallBack = function(subscriptiodata) {

        }

        vm.LoadActivityData = function () {
            //$('.preload-overlay', $(windowContainer)).show();

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

        vm.fromNow = function (date) {
            return moment(date).fromNow();
        }

        vm.CloseWindow = function () {
            $scope.CloseWindowTab($(vm.WindowContainer).closest('.windowcontainer').attr('windowindex'));
        }


        vm.DetailView = function (data, event) {
            $("#summarypanel", vm.WindowContainer).html(' <center><span class="fa fa-circle-o-notch fa-pulse" style="font-size:20px;color:white;"></span></center>');
            $(".pagecontent", vm.WindowContainer).addClass('summaryview');
            $('#CreateActivity .highlightrow').removeClass('highlightrow')
            $(event.currentTarget).addClass('highlightrow');

            $http({ method: 'Get', url: 'Activity/DetailedView?IID=' + data.ActivityID })
                .then(function (result) {
                    $("#summarypanel", vm.WindowContainer).html($compile(result.data)($scope));
                });
        }
    }]);