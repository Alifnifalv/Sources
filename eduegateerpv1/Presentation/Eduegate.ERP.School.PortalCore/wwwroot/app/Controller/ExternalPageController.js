app.controller('ExternalPageController', ['$scope', '$http', '$compile', "$rootScope", "subscriptionService", "toaster", '$timeout', "$q", function ($scope, $http, $compile, $root, $subscription, $toaster, $timeout, $q) {
    console.log('ExternalPageController loaded.');

    $scope.init = function (model,type) {
        if (type == "QRCodeView") {
            $scope.ShowQRdetails = false;
            generateQRCode(model);
            timeValidation(model);
        }
    };

    function generateQRCode(data) {
        if (data.QRCode) {
            var qrcode = new QRCode("qrcode", data.QRCode);
            $scope.QrDatas = data;
            $scope.ShowQRdetails = true;
        }
        else {
            return false;
        }
    }

    function timeValidation(data) {
        var date1 = moment(new Date()).format('YYYY-MM-DDTHH:mm:ss');
        var date2 = moment(data.FromTimeString).format('YYYY-MM-DDTHH:mm:ss');

        var nwDate = moment(date1).utc().format('YYYY-MM-DD');
        var shrdDate = moment(date2).utc().format('YYYY-MM-DD');

        //date check for next steps
        if (nwDate == shrdDate) {

            //new date instance
            const dt_date1 = new Date(date1);
            const dt_date2 = new Date(date2);

            var currentHour = dt_date1.getHours();
            var sharedHour = dt_date2.getHours();

            var currentHourToMinut = currentHour * 60;

            if (sharedHour != null || sharedHour != "" || sharedHour != undefined) {
                var sharedHourToMinut = sharedHour * 60;

                //check the minut difference is between 6 hr  >> 6*60 = 360 minutes
                var minutDiff = currentHourToMinut - sharedHourToMinut;
                if (minutDiff <= "360") {
                    $scope.ShowQRdetails = true;
                }
                else {
                    $scope.ShowQRdetails = false;
                    $scope.Message1 = "Sorry !"
                    $scope.Message2 = "the QR CODE is expired.";
                    $scope.Message3 = "please ask parent to share again";
                }
            }
        }
        else {
            $scope.ShowQRdetails = false;
            $scope.Message1 = "Sorry !"
            $scope.Message2 = "the QR CODE is expired.";
            $scope.Message3 = "please ask parent to share again";
        }

    }

}]);