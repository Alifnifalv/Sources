app.controller('CircularController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout) {
    console.log('Circular Controller loaded.');

    $scope.CircularList = [];

    $scope.ShowPreLoader = true;

    $scope.init = function () {

        showOverlay();

        $.ajax({
            type: "GET",
            url: utility.myHost + "Home/GetCircularList",
            success: function (result) {
                $timeout(function () {
                $scope.$apply(function () {
                    if (!result.IsError && result !== null) {

                        $scope.CircularList = result.Response;

                        hideOverlay();

                        $scope.ShowPreLoader = false;
                    }
                });
                });
            },
            error: function () {
                hideOverlay();
                $scope.ShowPreLoader = false;
            },
            complete: function (result) {
                hideOverlay();
                $scope.ShowPreLoader = false;
            }
        });

    };

    $scope.NewApplicationClick = function () {
        window.location.replace(utility.myHost + "NewApplicationFromSibling?loginID=" + $rootScope.LoginID);
    };

    $scope.DownloadURL = function (url) {
        var link = document.createElement("a");
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    };

    $scope.GetExpiryDateDifference = function (circularData) {
        
        var currentDate = new Date();
        var circularExpiryDate = new Date(moment(circularData.ExpiryDate, 'DD/MM/YYYY'));

        const diffTime = Math.abs(circularExpiryDate - currentDate);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        return diffDays;
    };

    function showOverlay() {
        $("#CircularListOverlay").fadeIn();
        $("#CircularListOverlayButtonLoader").fadeIn();
    }

    function hideOverlay() {
        $("#CircularListOverlay").fadeOut();
        $("#CircularListOverlayButtonLoader").fadeOut();
    }

    $scope.DirectDownload = function (contentID) { 
        showOverlay();

        $.ajax({
            type: "GET",
            url: utility.myHost + "Content/DirectDownloadByContentID?contentID=" + contentID,
            xhrFields: {
                responseType: 'blob' // Set the response type to blob
            },
            success: function (data, textStatus, xhr) {

                if (xhr.status === 200) {

                    var fileName = '';
                    var fileType = data.type;

                    const headers = xhr.getAllResponseHeaders();

                    const disposition = headers.trim().split(/[\r\n]+/)[0];

                    if (disposition && disposition.indexOf('attachment') !== -1) {
                        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                        var matches = filenameRegex.exec(disposition);
                        if (matches != null && matches[1]) {
                            fileName = matches[1].replace(/['"]/g, '');
                        }
                    }

                    var blob = new Blob([data], { type: fileType });
                    var url = window.URL.createObjectURL(blob);
                    var a = document.createElement('a');
                    document.body.appendChild(a);
                    a.style = 'display: none';
                    a.href = url;
                    a.download = fileName; // Set the appropriate file name here
                    a.click();
                    window.URL.revokeObjectURL(url);

                    callToasterPlugin('success', "File downloaded successfully!");
                } else {
                    callToasterPlugin('error', "No data received.");
                }
            },
            error: function (result) {
                callToasterPlugin('error', "Failed to download file! Error: " + error);
            },
            complete: function (result) {
                hideOverlay();
            }
        });
    };

}]);