app.controller('MyInboxController', ['$scope', '$http', 'moment', 'alert', '$compile',
    function ($scope, $http, moment, alert, $compile) {
        var vm = this;

        vm.InboxData = [];
        vm.WindowContainer = null;
        //vm.InboxData.push({});

        vm.Init = function (window) {
            vm.WindowContainer = '#' + window;
            vm.LoadInboxData();
        }

        vm.fromNow = function (date) {
            return moment(date).fromNow();
        }

        vm.LoadInboxData = function () {
            //$('.preload-overlay', $(vm.WindowContainer)).show();

            $.ajax({
                url: 'MyInbox/GetAlerts',
                type: 'GET',
                success: function (data) {
                    $scope.$apply(function () {
                        vm.InboxData = data;
                        //$('.preload-overlay', $(vm.WindowContainer)).hide();
                        $('.waypoint', $(vm.WindowContainer)).hide();
                    });
                }
            });
        }

        vm.CloseWindow = function () {
            $scope.CloseWindowTab($(vm.WindowContainer).closest('.windowcontainer').attr('windowindex'));
        }


        vm.DetailView = function (data, event) {
            var windowName = '';
            var viewName = '';

            if (data.ReferenceScreenID != null) {
                if (data.ReferenceScreenID == 2241) {
                    windowName = 'StudentLeaveApplication';
                    viewName = 'Student Leave Application';
                }
                else if (data.ReferenceScreenID == 2163) {
                    windowName = 'StudentTransferRequest';
                    viewName = 'Student Transfer /Discontinue Request';
                }

                if ($scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName))
                    return;

                $scope.AddWindow("Edit" + windowName, viewName, "Edit" + windowName);
                editUrl = 'Frameworks/CRUD/Create?screen=' + windowName + "&ID=" + data.ReferenceID;

                $http({ method: 'Get', url: editUrl })
                    .then(function (result) {
                        $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
                        $scope.ShowWindow("Edit" + windowName, viewName, "Edit" + windowName);
                    });
            }
            else {
                $("#summarypanel", vm.WindowContainer).html(' <center><span class="fa fa-circle-o-notch fa-pulse" style="font-size:20px;color:white;"></span></center>');
                $(".pagecontent", vm.WindowContainer).addClass('summaryview');
                $('#CreateMyInbox .highlightrow').removeClass('highlightrow')
                $(event.currentTarget).addClass('highlightrow');
                console.log("test view")
                $http({ method: 'Get', url: 'MyInbox/DetailedView?IID=' + data.NotificationAlertIID })
                    .then(function (result) {
                        $("#summarypanel", vm.WindowContainer).html($compile(result.data)($scope));
                    });
            }
        }
    }]);