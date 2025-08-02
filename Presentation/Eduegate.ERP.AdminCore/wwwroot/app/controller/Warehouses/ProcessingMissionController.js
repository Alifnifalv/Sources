
app.controller("ProcessingMissionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$interval", function ($scope, $http, $compile, $window, $timeout, $location, $route, $interval) {
    console.log("Processing Mission Controller");
    var windowContainer = null;
    var ViewName = null;
    var IID = null;
    $scope.ViewModels = [];
    var DefaultDynamicView = null;
    $scope.ShowTime = null;
    $scope.Model = null;
    $scope.LookUps = [];
    var lookLoadCount = 0;

    $interval(function () {
        $scope.ShowTime = Date.now();
    }, 1000);

    $scope.ShowRemainingHours = function (date) {
        return utility.getRemainingHoursText(date, $scope.ShowTime);
    }

    $scope.ShowHoursTaken = function (date) {
        if (date == null) return null;

        return utility.getRemainingHoursText($scope.ShowTime, date);
    }

    $scope.Init = function (window, viewName, model, iid) {
        IID = iid;
        windowContainer = '#' + window;
        ViewName = viewName;
        $scope.Model = model;
        LoadLookups(model.Urls);
        LoadJobDetails();
        $scope.ProcessJobURL = model.SaveURL;
    }

    function LoadJobDetails() {
      
        var lookUpLoads = setInterval(function () {
            if (lookLoadCount >= $scope.Model.Urls.length) {
                $http({
                    method: 'Get', url: 'Distributions/JobEntryDetail/Get?ID=' + IID.toString()
                })
               .then(function (result) {
                   $scope.Model = result;
                   $(".preload-overlay", $(windowContainer)).css("display", "none");
               });
                clearInterval(lookUpLoads);
            }

        }, 100);
    }

    $scope.CloseSummaryPanel = function (event) {
        $(event.currentTarget, $(windowContainer)).closest('.pagecontent').removeClass('summaryview detail-panel minimize-fields');
        $(".preload-overlay", $(windowContainer)).css("display", "none");
        $(windowContainer).find("#summarypanel").html('');
        var closesWindowContainer = $(windowContainer).closest('.windowcontainer');
        $('.subrow td[colspan]', closesWindowContainer).attr('colspan', $('.subrow td[colspan]', closesWindowContainer).attr('oldcolspan'));
        $('.subrow td[colspan]', closesWindowContainer).removeAttr('oldcolspan');
    }

    function LoadLookups(urls) {
        if (urls == undefined || urls == null) {
            return;
        }

        $.each(urls, function (index, url) {
            $scope.LookUps[url.LookUpName] = [{ 'Key': '', Value: 'Loading..' }];
            $.ajax({
                type: "GET",
                url: url.Url,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        $scope.LookUps[url.LookUpName] = result;
                        lookLoadCount++;
                    });
                }
            });
        });
    }

    $scope.SaveJobOperation = function () {
        if ($scope.Model.MasterViewModel.IsCompletedOrFailed == true) {
            return false;
        }

        $(".preload-overlay").css("display", "block");

        $.ajax({
            type: "POST",
            url: $scope.ProcessJobURL,
            data: JSON.stringify($scope.Model),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                //console.log(result);
                $().showMessage($scope, $timeout, false, "Job is completed.");
                $(".preload-overlay").css("display", "none");
            }            
        });
    }

    $scope.PickJob = function (event, ctrl) {
        console.log(ctrl);
    }

    $scope.Comments = function (event) {
        $('.popup.comments', $(windowContainer)).slideDown("fast");
        $('.transparent-overlay', $(windowContainer)).show();

        $.ajax({
            url: "Mutual/Comment?type=Job&referenceID=" + IID.toString(),
            type: 'GET',
            success: function (content) {
                  $('#commentPanel', $(windowContainer)).html($compile(content) ($scope));
            }
        });
    }

    $scope.CloseCommentOverlay = function (event) {
        $(event.currentTarget).hide();
        $('.popup.comments', $(windowContainer)).slideUp("fast");
    }

    $scope.GEOLocationPosition = function (event) {

        if ($scope.Model.MasterViewModel.ReferenceTransactionNo > 0) {

            $("#Location").addClass("show");
            $scope.GetLocation();
        }
    }

    $scope.CloseLocationPopup = function (event) {
        $("#Location").removeClass("show");
    }

    $scope.UpdateGEOLocation = function () {

        if (this.Model.MasterViewModel.LocationLatitude != 0 && this.Model.MasterViewModel.LocationLongitude != 0) {

            $.ajax({
                url: "JobEntryDetail/UpdateOrderContact?ID=" + this.Model.MasterViewModel.ReferenceTransactionNo + "&Latitude=" + this.Model.MasterViewModel.LocationLatitude + "&Longitude=" + this.Model.MasterViewModel.LocationLongitude,
                type: 'GET',
                success: function (result) {
                    if (result != null) {
                        if (result.IsError == false) {
                            $("#Location").removeClass("show");
                        }
                    }
                }
            });
        }
    }

    $scope.GetLocation = function () {
        if (navigator.geolocation) {
            return navigator.geolocation.getCurrentPosition(ShowPosition, ShowError);

        } else {
            alert("Geolocation is not supported by this browser.");
        }
    }

    function ShowPosition(position) {
        $scope.Model.MasterViewModel.LocationLatitude = position.coords.latitude;
        $scope.Model.MasterViewModel.LocationLongitude = position.coords.longitude;

        return $scope.Model.MasterViewModel.LocationLatitude + "," + $scope.Model.MasterViewModel.LocationLongitude;
    }

    function ShowError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                alert("User denied the request for Geolocation.");
                break;
            case error.POSITION_UNAVAILABLE:
                alert("Location information is unavailable.");
                break;
            case error.TIMEOUT:
                alert("The request to get user location timed out.");
                break;
            case error.UNKNOWN_ERROR:
                alert("An unknown error occurred.");
                break;
        }
    }

    $scope.GetLocationMapInNewWindow = function () {

        console.log($scope.Model.MasterViewModel.LocationLatitude, "  ", $scope.Model.MasterViewModel.LocationLongitude);

        if ($scope.Model.MasterViewModel.LocationLatitude != null && $scope.Model.MasterViewModel.LocationLongitude != null) {

            var latlong = $scope.Model.MasterViewModel.LocationLatitude + "," + $scope.Model.MasterViewModel.LocationLongitude;
            var img_url = "http://maps.googleapis.com/maps/api/staticmap?zoom=14&size=400x300&sensor=false&center=" + latlong + "&markers= " + latlong + "";
            window.open(img_url);
        }
    }

    $scope.OnJobOperationStatusChange = function (event, model) {

        if (model.JobOperationStatus.Key == '4') {
            model.Reason = null;
            model.IsStatusProver = true;
        }
        else {
            model.IsStatusProver = false;
        }
    }

}]);
