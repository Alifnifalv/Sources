app.controller("JobOperationLiveViewController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
        console.log("JobOperationLiveViewController");

        angular.extend(this, $controller('LiveViewController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $location: $location, $route: $route }));

        $scope.PrintJobs = function (event, reportName, reportFullName, reportHeader) {
            var IDs = new JSLINQ($scope.$$childTail.RowModel.rows)
                           .Where(function (item) {
                               return item.IsRowSelected == true;
                           });

            var IDString = '';
            $(IDs.items).each(function (index) {
                IDString = IDs.items[index].JobEntryHeadIID + ',' + IDString;
            });


            if ($scope.ShowWindow(reportName, reportHeader, reportName))
                return;

            //var windowName = $scope.AddWindow(reportName, reportHeader, reportName);

            //var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
            var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + IDString;
            //$('#' + windowName).append('<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:white;"></span></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px onload="onLoadComplete(this);"></></iframe>');
            var $iFrame = $('iframe[reportname=' + reportName + ']');
            $http({ method: 'GET', url: "Home/ViewReports?returnFileBytes=true&HeadID=" + IDString + "&reportName=" + reportFullName })
                   .then(function (filename) {
                       var w = window.open();
                       w.document.write('<iframe onload="isLoaded()" id="pdf" name="pdf" src="' + filename + '"></iframe><script>function isLoaded(){window.frames[\"pdf\"].print();}</script>');
                   });
            $iFrame.load(function () {
                $("#Load", $('#' + windowName)).hide();
            });
        }
    }]);