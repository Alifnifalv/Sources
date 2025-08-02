app.controller('PowerBIController', ['$scope', '$http', "$compile", "$window", '$location', "$timeout", "$rootScope",
    function ($scope, $http, $compile, $window, $location, $timeout, $root) {
        console.log('PowerBIController controller loaded.');
        var models = window["powerbi-client"].models;
        $scope.init = function (Model, windowName) {
            var reportContainer = $("#report-container-" + windowName).get(0);

            var pageID = Model.ItemId;

       
            //$scope.GetEmbedInfo();
            $(function () {


                $.ajax({
                    type: "GET",
                    url: "CMS/Page/getembedinfo", data: { pageID: pageID },
                    success: function (data) {
                        embedParams = $.parseJSON(data);
                        reportLoadConfig = {
                            type: "report",
                            tokenType: models.TokenType.Embed,
                            accessToken: embedParams.EmbedToken.Token,
                            // You can embed different reports as per your need
                            embedUrl: embedParams.EmbedReport[0].EmbedUrl,
                            settings: {
                                panes: {
                                    filters: {
                                        visible: false
                                    },
                                    pageNavigation: {
                                        visible: false
                                    }
                                },
                                layoutType: getLayoutType() // Dynamically set layout type
                            }
                            // Enable this setting to remove gray shoulders from embedded report
                            // settings: {
                            //     background: models.BackgroundType.Transparent
                            // }
                        };

                        // Use the token expiry to regenerate Embed token for seamless end user experience
                        // Refer https://aka.ms/RefreshEmbedToken
                        tokenExpiry = embedParams.EmbedToken.Expiration;

                        // Embed Power BI report when Access token and Embed URL are available
                        var report = powerbi.embed(reportContainer, reportLoadConfig);

                        $window.addEventListener("resize", function () {
                            var layoutType = getLayoutType();
                            report.updateSettings({ layoutType: layoutType });
                            console.log("Switched to layoutType:", layoutType);
                        });

                        // Determine layout type based on screen width
                        function getLayoutType() {
                            return $window.innerWidth <= 768
                                ? models.LayoutType.MobilePortrait // Mobile view
                                : models.LayoutType.Custom; // Desktop view
                        }
                        // Clear any other loaded handler events
                        report.off("loaded");

                        // Triggers when a report schema is successfully loaded
                        report.on("loaded", function () {
                            console.log("Report load successful");
                        });

                        // Clear any other rendered handler events
                        report.off("rendered");

                        // Triggers when a report is successfully embedded in UI
                        report.on("rendered", function () {
                            console.log("Report render successful");
                        });

                        // Clear any other error handler events
                        report.off("error");

                        // Handle embed errors
                        report.on("error", function (event) {
                            var errorMsg = event.detail;

                            // Use errorMsg variable to log error in any destination of choice
                            console.error(errorMsg);
                            return;
                        });
                    },
                    error: function (err) {

                        // Show error container
                        var errorContainer = $(".error-container");
                        $(".embed-container").hide();
                        errorContainer.show();

                        // Format error message
                        var errMessageHtml = "<strong> Error Details: </strong> <br/>" + err.responseText;
                        errMessageHtml = errMessageHtml.split("\n").join("<br/>");

                        // Show error message on UI
                        errorContainer.append(errMessageHtml);
                    }
                });
            });

        };




        //$scope.GetEmbedInfo = function () {
        //    var pageID = 58;

        //    $http.get("CMS/Page/GetEmbedInfo", { params: { pageID: 58 } })
        //        .then(function (response) {
        //            $(document).ready(function () {
        //            var embedInfo =  response.data;
        //                var models = window['powerbi-client'].models
        //            if (embedInfo && embedInfo.EmbedToken && embedInfo.EmbedUrl && embedInfo.ReportId) {
        //                var config = {
        //                    type: 'report',
        //                    tokenType: models.TokenType.Embed,
        //                    accessToken: embedInfo.EmbedToken,
        //                    embedUrl: embedInfo.EmbedUrl,
        //                    id: embedInfo.ReportId,
        //                    permissions: models.Permissions.All,
        //                    settings: {
        //                        filterPaneEnabled: false,
        //                        navContentPaneEnabled: true
        //                    }
        //                };

        //                var reportContainer = document.getElementById('reportContainer');
        //                var report = powerbi.embed(reportContainer, config);

        //                report.on('loaded', function () {
        //                    console.log('Report loaded successfully');
        //                });

        //                report.on('error', function (event) {
        //                    console.error('Error embedding report:', event.detail);
        //                });
        //            } else {
        //                console.error('Invalid embed info received:', embedInfo);
        //                }
        //            });
        //        })
        //        .catch(function (error) {
        //            console.error('Error fetching embed info:', error);
        //        });
        //};

}]);

