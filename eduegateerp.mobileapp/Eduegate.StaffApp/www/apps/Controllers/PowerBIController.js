app.controller("PowerBIController", ["$scope", "$http", "$state", "rootUrl", "$location", "$rootScope", "$stateParams", "GetContext", "$sce", "$timeout", "$window", function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, GetContext, $sce, $timeout , $window) {
  console.log("PowerBIController loaded.");
  $scope.PageName = "PowerBIController";

  var PowerBIService = rootUrl.PowerBIServiceUrl;

    
  var models = window["powerbi-client"].models;
  $scope.init = function () {
      var reportContainer = $("#report-container-").get(0);
      $(function () {
          $.ajax({
              type: "GET",
              url: PowerBIService + "/GetEmbedInfoAsync", data: { pageID: $scope.pageID },
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
                          layoutType: getLayoutType(), // Dynamically set layout type
                          background: models.BackgroundType.Transparent
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
  $scope.init();
}]);
