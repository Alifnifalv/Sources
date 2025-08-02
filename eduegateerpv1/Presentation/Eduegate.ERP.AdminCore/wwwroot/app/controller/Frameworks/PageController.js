app.controller("PageController", ["$scope", "$timeout", "$window", "$http", "$compile", "$sce", "$q",
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('PageController controller loaded.');

        $scope.runTimeParameter = null;
        $scope.LazyLoadBoilerPlateNos = 0;
        $scope.GetBoilerPlatesURL = '/CMS/Boilerplate/GetBoilerPlates';
        $scope.Model = null;
        $scope.window = "";
        $scope.windowname = "";
        $scope.pageID = null;
        $scope.PowerBIData = null;
        $scope.PowerBiReportID = null;
        $scope.init = function (model, window, windowname) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
            $scope.windowname = windowname;
            $scope.LoadBoilerPlates();

            $scope.pageID = model.ViewModel.PageID;
            if ($scope.pageID) {
                $scope.GetPowerBiData($scope.pageID);
            }
        };

        $scope.LoadBoilerPlates = function () {
            $timeout(function () {
                $.each($scope.Model.ViewModel.Boilerplates, function (index) {
                    var width = new JSLINQ(this.RuntimeParameters).Where(function (data) { return data.Key == "Width" }).items[0];

                    if (width == null || width == undefined) {
                        width = {};
                        width.Value = "100";
                    }

                    $("#" + $scope.windowname + " .main-page").append($compile("<div id='BoilerPlate" + this.BoilerplateMapIID + "' class='col-xl-" + width.Value + "'></div>")($scope));
                    $.ajax({
                        type: 'POST',
                        url: "/CMS/Boilerplate/Template",
                        data: this,
                        success: function (result) {
                            if (result) {
                                $("#" + $scope.windowname + " .main-page #BoilerPlate" + result.BoilerplateMapIID).append($compile(result.Content)($scope));
                            }
                        }
                    });
                });
            });

        }

    


    
        //function getPageAccessToken(reportIds) {
        //    Promise.all(reportIds.map(reportId =>
        //        fetch('https://your-api-url/Page/GetAccessToken')
        //            .then(response => {
        //                if (!response.ok) {
        //                    throw new Error('Network response was not ok for report ID: ' + reportId);
        //                }
        //                return response.text();
        //            })
        //            .catch(error => {
        //                console.error('There was a problem with the fetch operation for report ID: ', reportId, error);
        //            })
        //    ))
        //        .then(accessTokens => {
        //            accessTokens.forEach((accessToken, index) => {
        //                if (accessToken) {
        //                    embedReport(reportIds[index], 'https://app.powerbi.com/reportEmbed', accessToken);
        //                }
        //            });
        //        })
        //        .catch(error => {
        //            console.error('Error in processing access tokens: ', error);
        //        });
        //}



      


$scope.GetPowerBiData = function (pageID) {
        showOverlay();
        $.ajax({
            type: "GET",
            url: "CMS/Page/GetPowerBIDataUsingPageID?pageID=" + pageID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.$apply(function () {
                    if (result) {
                        $scope.PowerBIData = result;
                        $scope.PowerBiReportID = "https://app.powerbi.com/reportEmbed?reportId=" + result.ReportID + "&autoAuth=true&ctid=" + result.TenantID;
                        document.getElementById('dashboardFrame').src = $scope.PowerBiReportID;
                        hideOverlay();
                    }
                    else {
                        $scope.PowerBIData = null;
                        hideOverlay();
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error('Failed to fetch Power BI data:', error);
                hideOverlay(); 
            }
        });
    };

function showOverlay() {
    $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
}

function hideOverlay() {
    $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
}



    }]);
