app.controller('StudentTransportRequestListController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('StudentTransportRequestListController controller loaded.');

    $scope.Applications = [];
    $scope.Casts = [];
    $scope.Relegions = [];
    $scope.Genders = [];
    $scope.Classes = [];
    $scope.Categories = [];

    $scope.init = function () {

        $timeout(function () {
            showOverlay();
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Home/GetUserTransportApplications",
                success: function (result) {
                    $timeout(function () {
                        $scope.Applications = result.Response;
                        console.log($scope.Applications);
                    });

                    hideOverlay();
                }
            });  
        });
        $('.preload-overlay').hide();
    };

    $scope.NewTransportApplicationClick = function () {
        window.location = "StudentTransportRequestApplication";
    };

    //$scope.EditTransportApplictionClick = function () {
    //    window.location = "StudentTransportRequestApplication";
    //};

    $scope.CancelTransportApplication = function (application) {

        var mapIID = application.TransportApplctnStudentMapIID;

        if (!mapIID) {
            return false;
        }

        showOverlay();

        $.ajax({
            type: "POST",
            url: utility.myHost + "Home/CancelTransportApplication?mapIID=" + mapIID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    callToasterPlugin('error', result.Response);

                    hideOverlay();
                    return false;
                }
                else {
                    callToasterPlugin('success', result.Response);

                    $timeout(function () {
                        $scope.init();
                    }, 1000);
                }
            }
        });
    };


    function showOverlay() {
        $("#StudentTransportRequestListOverlay").fadeIn();
        $("#StudentTransportRequestListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#StudentTransportRequestListOverlay").fadeOut();
        $("#StudentTransportRequestListOverlayButtonLoader").fadeOut();
    }

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    function showOverlay() {
        $("#CircularListOverlay").fadeIn();
        $("#CircularListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#CircularListOverlay").fadeOut();
        $("#CircularListOverlayButtonLoader").fadeOut();
    }

}]).filter("filterdate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        try {
            var m = x.match(re);
            if (m) return new Date(parseInt(m[1]));
            else return null;
        }
        catch (e) {}
        
    };
});