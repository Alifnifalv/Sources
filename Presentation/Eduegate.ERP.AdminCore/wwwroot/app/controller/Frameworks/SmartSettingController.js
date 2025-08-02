app.controller('SmartSettingController', ['$scope', '$http', '$compile', '$window', '$location', '$timeout', '$rootScope',
    function ($scope, $http, $compile, $window, $location, $timeout, $rootScope) {
        console.log('SmartSettingController controller loaded.')
        $scope.SettingModel = []
        $scope.UploadImageUrl = null;
        $scope.IsSyncProduct = false;
        $scope.IsSyncInventory = false;
        $scope.IsSyncPrice = false;
        $scope.IsSyncAllProduct = false;
        $scope.IsSyncFtpProduct = false;
        $scope.IsSyncPrice = false;
        $scope.IsSyncPromotion = false;
        $scope.Apps = []
        $scope.SelectAppID = null;

        var windowContainer = null

        $scope.Init = function (model, window,event) {
            windowContainer = '#' + window
            $scope.SettingModel = model

            $.ajax({
                url: 'Settings/Setting/GetSettings',
                type: 'GET',
                success: function (result) {
                    $scope.$apply(function () { $scope.SettingModel = result })
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                    $timeout(function () {
                        $("li#initialLoad > a").trigger("click");
                    }, 10)
                }
            });

            $.ajax({
                url: 'Mutual/GetDynamicLookUpData?lookType=Apps&defaultBlank=false',
                type: 'GET',
                success: function (result) {
                    $scope.$apply(function () { $scope.Apps = result })
                    $('.preload-overlay').fadeOut(500)
                }
            });

            $scope.LoadScreen(event, 'Home/CustomDashbaord?pageID=113', 'DataManagement');
        }

        $scope.GenerateOAuthToken = function () {
            //$('.preload-overlay', $(windowContainer)).fadeIn(500)

            $.ajax({
                url: 'OAuth/SetGoogleAuthenticationSettings',
                type: 'GET',
                success: function (result) {
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }
            });
        }

        $scope.TitleClick = function (event) {
            $(event.target).toggleClass('active')
            if ($(event.target).hasClass('active')) {
                $(event.target).next('.sublist-grid').slideDown('fast')
            } else {
                $(event.target).next('.sublist-grid').slideUp('fast')
            }
        }

        $scope.SaveChanges = function (setting) {
            if (!setting.IsDirty) return

            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $.ajax({
                url: 'Settings/Setting/SaveSettings',
                type: 'POST',
                data: setting,
                success: function (result) {
                    setting.IsDirty = false
                    $().showMessage($scope, $timeout, false, 'Updated the settings sucessfully.')
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }
            });
        }

        var loadingTemplate = '<center><span id="Load" class="fa fa-circle-o-notch fa-pulse waypoint" style="font-size:20px;color:black;"></span></center>';

        $scope.LoadScreen = function (event, url, name) {
            $(".smartMenus li a").removeClass("active");
            $(event.currentTarget).addClass("active");
            if (event != undefined) {
                event.preventDefault()
                if (event.stopPropagation) event.stopPropagation()
            }

            if ($(event.currentTarget).hasClass('brcolor') == true) { return }
            $('.smartmenu_Content_Inner').html(loadingTemplate);
            $http({ method: 'Get', url: url })
                .then(function (result) {
                    $('.smartmenu_Content_Inner').html($compile(result.data)($scope));
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });

        }

        $scope.SalesPosting = function () {

            var transactionNo = $('#transactionNumber').val();
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'Get', url: 'Frameworks/SmartSetting/PostTransaction?transactionNo=' + transactionNo,
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);

                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.PointPosting = function () {

            var transactionNo = $('#transactionNumber').val();
            var accountNumber = $('#accountNumber').val();
            var transactionDate = $('#transactionDate').val();
            var points = $('#points').val();

            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'Get', url: 'Frameworks/SmartSetting/PointPosting?transactionNo=' + transactionNo + '&accountNumber=' + accountNumber
                    + '&transactionDate=' + transactionDate + '&points=' + points,
            })
                .then(function (result) {
                    result = result.data;

                    if (result.IsError) {
                        $().showMessage($scope, $timeout, true, result.Message);
                    }
                    else {
                        $().showMessage($scope, $timeout, false, result.Message);
                    }

                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.PushNotification = function () {
            var deviceToken = $('#deviceToken').val();
            var notificationTitle = $('#notificationTitle').val();
            var notificationMessage = $('#notificationMessage').val();
            var notificationAllUsers = $('#notificationForAllUsers').val();

            if (notificationAllUsers == "on" && deviceToken == "") {
                $().showMessage($scope, $timeout, true, "Please provide device token.");
                return;
            }

            if (notificationAllUsers == "on") {
                deviceToken = "";
            }

            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'POST',
                url: 'Frameworks/SmartSetting/SendPushNotification',
                data: {
                    DeviceToken: deviceToken,
                    Title: notificationTitle,
                    Message: notificationMessage,
                }
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);                   
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchNewProducts = function (isSingle) {            
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchNewProducts?IsSingle='+isSingle,
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchAllProducts = function () {
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchAllProducts?IsAllMigration=' + $scope.IsSyncAllProduct + '&IsFtpMigration='
                    + $scope.IsSyncFtpProduct + '&IsInventoryMigration=' + $scope.IsSyncInventory + '&IsPriceMigration=' + $scope.IsSyncPrice +
                    '&IsImageMigration=' + $scope.IsSyncImage,
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchPriceAndInventory = function (isSyncPrice, isSyncInventory, isSyncPromotion, appID) {
            var arg = (isSyncPrice ? ' -prc' : '');
            arg = arg + (isSyncInventory ? ' -inv' : '');
            arg = arg + (isSyncPromotion ? ' -promo' : '');
            arg = arg + (appID ? ' -' + appID : '');

            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'Get', url: 'Frameworks/SmartSetting/PriceAndInventorySynchronizer?arg=' + arg,
            })
                .then(function (result) {
                    result = result.data;

                    if (result.IsError) {
                        $().showMessage($scope, $timeout, true, result.Message);
                    }
                    else {
                        $().showMessage($scope, $timeout, false, result.Message);
                    }

                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }


        $scope.SynchPromotion = function () {
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchPromotion',
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchInventory = function () {
            var item = $('#synchitemno').val();
            var barcode = $('#synchBarcode').val();
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchInventory?itemNumber=' + item
                    + '&barCode=' + barcode,
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  

                    totalCount = totalCount - 1;
                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500);
                    }
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);

                    totalCount = totalCount - 1;
                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500)
                    }
                });
        }

        $scope.SynchProductIndex = function () {
            $('.preload-overlay', $(windowContainer)).fadeIn(500);

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/SynchProductIndexes',
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  

                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SelectedPath = { folderId : '', folderName :  ''};

        $rootScope.SelectBrowsedFiles = function (id, value) {
            $('#globalRightDrawer').modal('hide');
            $scope.SelectedPath = { folderId : id, folderName : value };
        }

        $scope.SynchProductsFromFTP = function () {
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/SynchProductsFromFTP',
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  

                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchImages = function (isProduct, isCategory) {
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            if (!isProduct) {
                isProduct = false;
            }

            if (!isCategory) {
                isCategory = false;
            }

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchImages?isProduct='
                    + isProduct.toString() + '&isCategory=' + isCategory.toString() + '&folderId=' + $scope.SelectedPath.folderId,
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);

                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchSolr = function () {
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchSolr',
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);

                    $('.preload-overlay', $(windowContainer)).fadeOut(500);
                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);
                    $('.preload-overlay', $(windowContainer)).fadeOut(500)
                });
        }

        $scope.SynchPrice = function () {
            var item = $('#synchitemno').val();
            var barcode = $('#synchBarcode').val();
            $('.preload-overlay', $(windowContainer)).fadeIn(500);

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchPrice?itemNumber=' + (item ? item : '')
                    + '&barCode=' + (barcode ? barcode : ''),
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  

                    if (!totalCount) {
                        totalCount = 1;
                    }

                    totalCount = totalCount - 1;

                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500)
                    }

                }, function (error) {
                    $().showMessage($scope, $timeout, true, error);

                    if (!totalCount) {
                        totalCount = 1;
                    }

                    totalCount = totalCount - 1;
                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500);
                    }
                });
        }

        $scope.SynchProducts = function () {
            var item = $('#synchitemno').val();
            var barcode = $('#synchBarcode').val();
            $('.preload-overlay', $(windowContainer)).fadeIn(500)

            $http({
                method: 'GET', url: 'Frameworks/SmartSetting/ProcessSynchProducts?itemNumber=' + item
                    + '&barCode=' + barcode + ($scope.UploadImageUrl ? '&productImage=' + $scope.UploadImageUrl : ''),
            })
                .then(function (result) {
                    result = result.data;

                    $().showMessage($scope, $timeout, result.IsError, result.Message);  

                    totalCount = totalCount - 1;
                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500);
                    }

                    $scope.UploadImageUrl = null;

                    if ($scope.IsSyncPrice) {
                        totalCount = totalCount + 1;
                        $scope.SynchPrice();
                    }

                    if ($scope.IsSyncInventory) {
                        totalCount = totalCount + 1;
                        $scope.SynchInventory();
                    }

                }, function () {
                    totalCount = totalCount - 1;
                    if (totalCount === 0) {
                        $('.preload-overlay', $(windowContainer)).fadeOut(500);
                    }
                });
        }

        var totalCount;

        $scope.SyncAll = function () {
            totalCount = 0;
            if ($scope.IsSyncProduct) {
                totalCount = totalCount + 1;
                $scope.SynchProducts();
            } else {

                if ($scope.IsSyncPrice) {
                    totalCount = totalCount + 1;
                    $scope.SynchPrice();
                }

                if ($scope.IsSyncInventory) {
                    totalCount = totalCount + 1;
                    $scope.SynchInventory();
                }

            }

      
        }

        var cropper;

        $rootScope.SaveCroppedImage = function (fileName, url, prefixAsString, index, sourceModelAsString, imageType) {
            if (cropper) {
                var xhr = new XMLHttpRequest()
                var fd = new FormData()
                cropper.getCroppedCanvas({
                    fillColor: '#ffffff',
                    width: 1308,
                    height: 859
                }).toBlob(function (blob) {
                    fd.append('imageType', imageType);
                    fd.append('png', blob, fileName);
                    xhr.open('POST', url, true)
                    xhr.onreadystatechange = function (url) {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            var result = JSON.parse(xhr.response)
                            if (result.Success == true && result.FileInfo.length > 0) {
                                $scope.$apply(function () {
                                    $scope.UploadImageUrl = result.FileInfo[0].FilePath;
                                })
                            }
                        }
                    }
                    xhr.send(fd)
                });
            }

            $('#dynamicPopover .close').trigger('click');
        }

        $scope.UploadImageFiles = function (uploadfiles, url, imageType, prefixAsString, sourceModelAsString,
            dataRow, index, element) {
            var file = uploadfiles.files[0];
            if (file) {
                if (cropper) {
                    cropper.destroy();
                }
                $(".dynamicPopoverOverlay").show();
                utility.ShowPopup('#dynamicPopover', windowContainer);
                $("#dynamicPopover").addClass('cropPopup');
                var parentControl = $(element).closest('.controls');
                utility.ShowPopup($(parentControl).find('.croppingImagePopover'), $scope.CrudWindowContainer);

                var newImage = new Image();

                if (/^image\/\w+/.test(file.type)) {
                    uploadedImageType = file.type;
                    uploadedImageName = file.name;
                    newImage.src = URL.createObjectURL(file);
                    cropper = new Cropper(newImage, {
                        dragMode: 'move',
                        aspectRatio: 1 / 0.6567278287461774,
                        movable: false,
                        scalable: false,
                        zoomable: true,
                        restore: false,
                        guides: true,
                        center: false,
                        highlight: false,
                        cropBoxMovable: true,
                        cropBoxResizable: false,
                        toggleDragModeOnDblclick: false,
                    });
                }
                $('.dynamicPopoverContainer').html(newImage);
                $('.dynamicPopoverContainer').append("<div class='cropBtn'><button class='button-orange' onclick='angular.element(this).scope().$root.SaveCroppedImage(\""
                    + file.name + "\",\"" + url + "\",\"" + prefixAsString + "\"," + (index ? index : -1).toString() + ",\"" + sourceModelAsString + "\",\"" + imageType + "\")'>Save</button></div>");

            }

            $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
        }

        function GetVariableFromString(root, variableString) {
            var object = root;

            $.each(variableString.split('.'), function (index, value) {
                object = object[value]
            });

            return object;
        }
    }]);
