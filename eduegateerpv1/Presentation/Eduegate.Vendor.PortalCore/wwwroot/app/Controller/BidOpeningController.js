app.controller("BidOpeningController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("BidOpeningController Loaded");

        $scope.bidLoginDTO = {
            LoginUserID: '',
            LoginEmailID: '',
            Password: ''
        };

        $scope.BidUserDetails = {};

        $scope.Init = function (window,chartID) {
                $.ajax({
                    url: utility.myHost + "Bid/GetBidUserDetails",
                    type: "GET",
                    success: function (result) {
                        if (!result.IsError && result.Response != null) {
                            $scope.$apply(function () {
                                $scope.BidUserDetails = result.Response;
                            });
                        }
                    }
                }); 

            $scope.DashBoardMenuCardCounts(chartID);
        };

        $scope.bidLoginForm = function () {

            $.ajax({
                url: utility.myHost + "Bid/BidLoginValidate",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.bidLoginDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        setTimeout(function () {
                            window.location = '/Bid/BidHome';
                        }, 1000);
                    } else {
                        toastr.error(result.ReturnMessage);
                        return false;
                    }
                }
            });
        };

        $scope.showPassword = function () {
            var x = document.getElementById("ConfirmPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        $scope.GetBidUserDetails = function (dto) {
            $scope.UserID = dto.LoginUserID;
        }

        $scope.DashBoardMenuCardCounts = function (chartID) {
                $scope.CardDatas = []; // Initialize CardDatas as an empty array  
                $.ajax({
                    type: "GET",
                    url: "/Home/GetCountsForDashBoardMenuCards?chartID=" + chartID,
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result) {
                                const columnDatas = result.ColumnDatas.map(item => JSON.parse(item));
                                const CardDatas = columnDatas.map(data => ({
                                    Header: data[0],          // Title  
                                    Value: data[1],           // Value  
                                    CardColor: data[2]       // Color  
                                }));
                                $scope.CardDatas = CardDatas;
                            } else {
                                $scope.CardDatas = [];  // Ensure CardDatas is empty if result is null  
                            }
                        });
                    }
                });
        };

    }
]);
