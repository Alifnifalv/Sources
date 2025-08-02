app.controller("DashboardController", ["$scope", "$http","$interval", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http,$interval, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("DashboardController Loaded");

        $scope.SettingValue = null;
        $scope.dashBoardBannerImg = [];
        $scope.currentIndex = 0;
        $scope.headerIndex = 0;
        $scope.imagePathPrefix = "/Images/DashBoardBannerImgFolder/"; 

        $scope.BannerHeadings = [];

        $scope.Init = function (model, window, viewName) {
            $scope.DashBoardMenuCardCounts();
            $scope.GetNotifications();
            DashBoardBannerImages();
            $scope.GetUserProfile();

            $scope.BannerHeadings = [
                "Welcome to the Hub of Talent and Possibilities.",
                "Connecting Talent to Transformative Opportunities",
                "Dedicated to Building Your Professional Growth.",
                "Your Talent Meets Opportunity Here.",
                "Featured Jobs: Take the Next Step in Your Career Today.",
                "From Passion to Profession—Make a Difference "
            ]
        };

        $scope.GetUserProfile = function () {
            showSpinner();
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: "GET",
                    url: "/Home/GetProfileDetails",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result) {
                                $scope.ProfileDetails = result.Response;
                                resolve(); // Resolve the promise when successful  
                            } else {
                                $scope.ProfileDetails = null;
                                resolve(); // Resolve even if there's no data to continue the flow  
                            }
                        });
                        hideSpinner();
                    },
                    error: function (error) {
                        console.error("Error retrieving user profile:", error);
                        hideSpinner();
                        reject(error); // Reject the promise in case of error  
                    }
                });
            });
        }

        $scope.DashBoardMenuCardCounts = function () {  
            GetSettingValue("CHART_META_DATA_ID_FOR_VENDOR_PORTAL").then(function () {
                $scope.CardDatas = []; // Initialize CardDatas as an empty array  

                $.ajax({
                    type: "GET",
                    url: "/Home/GetCountsForDashBoardMenuCards?chartID=" + $scope.SettingValue,
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
            });
        };

        $scope.GetNotifications = function () {
            showSpinner();
            $.ajax({
                type: "GET",
                url: "/Home/GetNotifications",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.IsError === false) {
                            $scope.Notifications = result.Response;
                        } else {
                            $scope.Notifications = null;  
                        }
                    });
                    hideSpinner();
                }
            });
        }

        function GetSettingValue(settingCode) {
            // Return a promise  
            return $http({
                method: 'GET',
                url: utility.myHost + "Mutual/GetSettingValueByKey?settingKey=" + settingCode,
            }).then(function (result) {
                if (result.data) {
                    $scope.SettingValue = result.data;
                } else {
                    $scope.SettingValue = null;
                }
            });
        }

        function DashBoardBannerImages() {
            return $http({
                method: 'GET',
                url: utility.myHost + "Home/DashBoardBannerImages",
            }).then(function (result) {
                if (!result.data.IsError) {
                    $scope.dashBoardBannerImg = result.data.Response; 
                    startImageRotation();
                } else {
                    return false;
                }
            });
        }

        function startImageRotation() {
            const intervalTime = 3000;
            $interval(function () {
                $scope.currentIndex = ($scope.currentIndex + 1) % $scope.dashBoardBannerImg.length;
                $scope.headerIndex = ($scope.headerIndex + 1) % $scope.BannerHeadings.length;
            }, intervalTime);
        }

        $scope.getImageSource = function () {
            if ($scope.dashBoardBannerImg.length > 0) {
                return $scope.imagePathPrefix + $scope.dashBoardBannerImg[$scope.currentIndex];
            } else {
                return '';
            }
        };

        $scope.getBannerHeadings = function () {
            if ($scope.BannerHeadings.length > 0) {
                return $scope.BannerHeadings[$scope.headerIndex];
            } else {
                return '';
            }
        }; 

        $scope.MarkAllAsRead = function (){
            showSpinner();
            $.ajax({
                type: "POST",
                url: "/Home/MarkAllasReadNotifications",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result.IsError === false) {
                            toastr.success(result.UserMessage);
                            $scope.Notifications = null;
                        } else {
                            toastr.error(result.UserMessage);
                            $scope.Notifications = null;
                        }
                    });
                    hideSpinner();
                }
            });
        }

        function showSpinner() {
            document.getElementById('spinner').style.display = 'block';
        }

        function hideSpinner() {
            document.getElementById('spinner').style.display = 'none';
        } 

    }
]);
