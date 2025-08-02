app.controller("VendorController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope","$filter",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope,$filter) {
        console.log("VendorController Loaded");

        $scope.QuotationList = null;
        $scope.VendorContactList = null;
        $scope.VisibleDeclareCheckBox = false;
        $scope.IsPrimaryContactPerson = false;
        $scope.HideSaveButtons = false;
        $scope.toListScreen = null;
        $scope.UserDetails = null;

        $scope.TotalQuantity = 0;
        $scope.TotalPrice = 0;
        $scope.TotalAmount = 0;

        $scope.RFQItems = null;
        $scope.DocumentStatusID = null;
        $scope.DocumentTypeID = null;

        $scope.Init = function (page, iid) {

            if (page === "QuotationList") {
                $scope.GetQuotationList();
            }
            else if (page === "VendorContactList") {
                $scope.GetVendorContactList();
            }
            else if (page === "RFQItemList") {
                $scope.GetRFQItemList(iid);
            }
            else if (page === "VendorRegistration") {
                GetBussinessTypeLookup();
                GetCountriesLookup();
                GetContactPersons();
                GetSupplierData();
            }
        };

        function GetContactPersons() {
                $.ajax({
                    type: 'GET',
                    url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=SupplierContactPersons&defaultBlank=false",
                    success: function (result) {
                        $scope.ContactPersons = result;
                        $timeout(function () {
                            $('#ContactPersonString').val($('#ContactID').val());
                        }, 1000);
                    }
                });
        }
        function GetBussinessTypeLookup() {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=BusinessTypes&defaultBlank=false",
                success: function (result) {
                    $scope.BusinessTypes = result;
                    $timeout(function () {
                        $('#BusinessTypeString').val($('#BusinessTypeID').val());
                    }, 1000);
                }
            });
        }
        function GetCountriesLookup() {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=Countries&defaultBlank=false",
                success: function (result) {
                    $scope.Countries = result;
                    $timeout(function () {
                        $('#TaxJurisdictionCountryString').val($('#TaxJurisdictionCountryID').val());
                    }, 1000);
                }
            });
        }


        $scope.updateTotals =  function () {

            var totalQuantity = 0;
            var totalPrice = 0;
            var totalAmount = 0;

            angular.forEach($scope.RFQ.TransactionDetails, function (item) {
                totalQuantity += parseInt(item.Quantity) || 0;
                totalPrice += parseInt(item.UnitPrice) || 0;
                totalAmount += (item.Quantity * item.UnitPrice) || 0;
            });

            // Assign total quantities and prices to scope variables  
            $scope.TotalQuantity = totalQuantity;
            $scope.TotalPrice = totalPrice;
            $scope.TotalAmount = totalAmount;

            updateGrantTotal();
        };

        // Function to update discount amount based on percentage  
        function updateDiscountAmount() {
            if ($scope.RFQ.TransactionHead.DiscountPercentage) {
                const discountAmt = $scope.TotalAmount * ($scope.RFQ.TransactionHead.DiscountPercentage / 100);
                $scope.RFQ.TransactionHead.DiscountAmount = Math.round(discountAmt).toFixed(2);
            } else {
                $scope.RFQ.TransactionHead.DiscountAmount = 0;
            }
            updateGrantTotal();
        }

        // Function to update discount percentage based on amount  
        function updateDiscountPercentage() {
            if ($scope.TotalAmount > 0) {
                const discntPerct = ($scope.RFQ.TransactionHead.DiscountAmount / $scope.TotalAmount) * 100;
                $scope.RFQ.TransactionHead.DiscountPercentage = discntPerct.toFixed(2);
            } else {
                $scope.RFQ.TransactionHead.DiscountPercentage = 0;
            }
            updateGrantTotal();
        }

        // Function to calculate the grant total  
        function updateGrantTotal() {
            const finalDiscountAmount = Math.round($scope.RFQ.TransactionHead.DiscountAmount || 0);
            const grantTotal = $scope.TotalAmount - finalDiscountAmount;
            $scope.GrantTotal = grantTotal.toFixed(2);
        }

        // Watch for changes in Discount Amount and Discount Percentage  
        $scope.$watch('RFQ.TransactionHead.DiscountAmount', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                updateDiscountPercentage(); // Update percentage if amount changes  
            }
        });

        $scope.$watch('RFQ.TransactionHead.DiscountPercentage', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                updateDiscountAmount(); // Update amount if percentage changes  
            }

        });


        function GetSettingValue(settingCode, type) {
            $http({
                method: 'Get', url: utility.myHost + "Mutual/GetSettingValueByKey?settingKey=" + settingCode,
            }).then(function (result) {
                if (type == "docType") {
                    $scope.DocumentTypeID = result.data;
                }
                else {
                    $scope.DocumentStatusID = result.data;
                }
            });
        }
        function GetSupplierData() {
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Vendor/GetSupplierData",
                success: function (result) {
                    $scope.Vendor = result;
                }
            });
        }

        $scope.rowDataVisible = [];


        $scope.toggleRowDataVisible = function (index) {
            if ($scope.rowDataVisible[index]) {
                $scope.rowDataVisible[index] = false;
            } else {
                $scope.rowDataVisible[index] = true;
            }
        };

        $scope.QtyChanges = function (item) {
            item.Amount = item.Quantity * item.UnitPrice;
            $scope.updateTotals();
        };

        $scope.PriceChanges = function (item) {
            item.Amount = item.Quantity * item.UnitPrice;
            $scope.updateTotals();
        };

        $scope.GetQuotationList = function () {
            $.ajax({
                type: "GET",
                url: "/Vendor/GetQuotationList",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.QuotationList = result;
                        }
                        else {
                            $scope.QuotationList = null;
                        }
                    });
                }
            });
        }

        $scope.GetRFQItemList = function (iid) {

            GetSettingValue("TRANSACTION_DOC_STS_ID_SUBMITTED","docStatus");
            GetSettingValue("Doc_Type_PurchaseOrder","docType");

            $.ajax({
                type: "GET",
                url: "/Vendor/GetRFQItemListByIID?iid=" + iid,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.RFQ = result;
                            $scope.updateTotals();
                            if ($scope.RFQ.TransactionHead.IsQuotation == true && $scope.RFQ.TransactionHead.DocumentStatusID == $scope.DocumentStatusID) {
                                $scope.HideSaveButtons = true;
                            }
                            else if ($scope.RFQ.TransactionHead.DocumentTypeID == $scope.DocumentTypeID) {
                                $scope.HideSaveButtons = true;
                            }
                        }
                        else {
                            $scope.RFQ = null;
                            $scope.HideSaveButtons = false;
                        }
                    });
                }
            });
        }

        $scope.submitForm = function () {
            var form = document.getElementById("multiPageForm");
        }

        $scope.updateData = function () {
            $scope.IsPrimaryContactPerson = $scope.isChecked;
        };

        $scope.ContactPersonChanges = function (contactId) {
            $scope.ContactPersonTitle = null;
            $scope.ContactPersonPhone = null;
            $scope.ContactPersonEmail = null;

            $scope.GetVendorContactList().then(function () {
                if ($scope.VendorContactList != null) {
                    var contactPerson = $scope.VendorContactList.find(x => x.ContactID == contactId);
                    if (contactPerson != null) {
                        $scope.$apply(function () {
                            $scope.ContactPersonTitle = contactPerson.Title;
                            $scope.ContactPersonPhone = contactPerson.PhoneNo1;
                            $scope.ContactPersonEmail = contactPerson.AlternateEmailID1;
                        });
                    }
                }
            });
        };

        $scope.GetVendorContactList = function () {
            return new Promise((resolve, reject) => {
                $http.get('/Vendor/GetVendorContactList').then(function (response) {
                    $scope.VendorContactList = response.data;
                    resolve();
                }, function (error) {
                    console.error(error);
                    reject(error);
                });
            });
        };

        $scope.SaveQuotation = function (savetype, QTdata) {

            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;

            const saveBtn = document.getElementById('saveBtn');
            saveBtn.disabled = true;

            GetSettingValue("TRANSACTION_DOC_STS_ID_SUBMITTED","docStatus");

            if (savetype === "Submit") {
                QTdata.TransactionHead.DocumentStatusID = $scope.DocumentStatusID;
            }

            $.ajax({
                url: utility.myHost + "Vendor/SaveSupplierQuotation",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(QTdata),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        $scope.GetRFQItemList(result.TransactionHead.HeadIID);
                        submitBtn.disabled = false;
                        saveBtn.disabled = false;

                    }
                    else {
                        toastr.error(result.ReturnMessage);
                        submitBtn.disabled = false;
                        saveBtn.disabled = false;
                    }
                }
            });
        };

        $scope.SaveChanges = function (vendorData) {
            // Get the form element  
            var form = document.forms["myProfile"];

            var invalidFields = []; // Array to hold names of invalid fields  

            // Check if the form is valid  
            if (form.checkValidity() === false) {
                // If the form is not valid, collect invalid field names  
                for (var i = 0; i < form.elements.length; i++) {
                    if (!form.elements[i].validity.valid && !form.elements[i].disabled) {
                        invalidFields.push(form.elements[i].name); // Collect the name of the invalid field  
                        form.elements[i].focus(); // Focus on the first invalid field  
                        break; // Exit the loop after focusing on the first invalid field  
                    }
                }

                // Create a message listing all invalid fields  
                var errorMessage = "Please fill out the following required fields : " + invalidFields.join(", ");
                toastr.error(errorMessage); // Show the error message with invalid fields  

                return; // Exit the function if the form is invalid  
            } 

            document.querySelectorAll('.buttonsDiv').forEach(function (div) {
                div.classList.add('disabled');
            });

            $.ajax({
                url: utility.myHost + "Vendor/SaveSupplierData",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(vendorData),
                success: function (result) {
                    $timeout(function () {
                        var message = result ? "Changes Saved Successfully" : "Something went wrong!";
                        toastr.success(message);
                        document.querySelectorAll('.buttonsDiv').forEach(function (div) {
                            div.classList.remove('disabled');
                        });
                    }, 1000);
                    $scope.$apply(function () {
                        if (result) {
                            $scope.Vendor = result;
                        } else {
                            return false;
                        }
                    });
                }
            });
        }


        // Upload file function  
        $scope.UploadFile = function (file,toField) {
            if (file) { 
                var formData = new FormData();
                formData.append("file", file);

                var xhr = new XMLHttpRequest();
                xhr.open("POST", utility.myHost + "Content/UploadContents", true);

                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4) {
                        var response = JSON.parse(xhr.responseText); 
                        if (response.Success) {  
                            //toastr.success("File " + file.name + " uploaded successfully!");
                            $scope.$apply(function () {
                                $scope.Vendor.Document[toField] = response.FileInfo[0].ContentFileIID; 
                            }); 
                        } else {
                            toastr.error("Failed to upload file. Status: " + xhr.status);
                        }
                    }
                };

                // Send the file data  
                xhr.send(formData);
            } else {
                toastr.error("No file selected.");
            }
        }; 

        $scope.DownloadURL = function (contentID) {
            var url = utility.myHost + "Content/ReadContentsByID?contentID=" + contentID;
            var link = document.createElement("a");
            link.href = url;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            delete link;
        };

        $scope.DeleteContentData = function (field, contentID) {

            $.ajax({
                url: utility.myHost + "Content/DeleteContentsByID?contentID=" + contentID,
                type: "POST",
                contentType: "application/json",
                success: function (result) {
                    if (result) {
                        $scope.$apply(function () {
                            $scope.Vendor.Document[field] = null;
                        });
                    } else {
                        toastr.error("Something went wrong");
                    }
                }
            });
        };


        //Register Contact person
        $scope.SubmitContactPerson = function () {
            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Vendor/SubmitContactPerson",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify($scope.ContactPerson),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.Message);
                        setTimeout(function () {
                            window.location = '/Home/DataListView?listName=VendorContacts';
                        }, 1000);
                    } else {
                        toastr.error(result.Message);
                        submitBtn.disabled = false;
                        document.body.classList.remove('disabled');
                    }
                }
            });
        };

        $scope.validateDates = function (field) {

            if (field == 'CRExpiryDateString') {
                const crStartDate = new Date($scope.Vendor.BusinessDetail.CRStartDateString.split('/').reverse().join(','));
                const crExpiryDate = new Date($scope.Vendor.BusinessDetail.CRExpiryDateString.split('/').reverse().join(','));

                if (crStartDate >= crExpiryDate) {
                    $scope.Vendor.BusinessDetail.CRExpiry = null;
                    $scope.Vendor.BusinessDetail.CRExpiryDateString = null;
                    toastr.error('CR Start Date must be earlier than CR Expiry Date');
                }
                else {
                    return false;
                }
            }
            else if (field == 'LicenseExpiryDateString') {
                const licenseStartDate = new Date($scope.Vendor.BusinessDetail.LicenseStartDateString.split('/').reverse().join(','));
                const licenseExpiryDate = new Date($scope.Vendor.BusinessDetail.LicenseExpiryDateString.split('/').reverse().join(','));

                if (licenseStartDate >= licenseExpiryDate) {
                    $scope.Vendor.BusinessDetail.licenseExpiryDate = null;
                    $scope.Vendor.BusinessDetail.LicenseExpiryDateString = null;
                    toastr.error('License Start Date must be earlier than License Expiry Date');
                } else {
                    return false;
                }
            }
            else if (field == 'EstExpiryDateString') {
                const estIssueDate = new Date($scope.Vendor.BusinessDetail.EstFirstIssueDateString.split('/').reverse().join(','));
                const estExpDate = new Date($scope.Vendor.BusinessDetail.EstExpiryDateString.split('/').reverse().join(','));

                if (estIssueDate >= estExpDate) {
                    $scope.Vendor.BusinessDetail.EstExpiryDate = null;
                    $scope.Vendor.BusinessDetail.EstExpiryDateString = null;
                    toastr.error('First Issue Date must be earlier than Expiry Date');
                }
                else {
                    return false;
                }
            }

            return null; // No errors  
        }; 
    }
]);
