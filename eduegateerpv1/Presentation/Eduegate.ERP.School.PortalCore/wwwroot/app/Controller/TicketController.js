app.controller("TicketController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("Ticket Controller Loaded");

    $scope.AllTicketDetails = [];
    $scope.TicketCount = 0;

    $scope.TicketGeneration = {
        "Subject": null,
        "Description1": null,
        "DocumentTypeID": null,
    };

    $scope.ErrorMessage = null;
    $scope.IsError = false;

    $scope.ShowPreLoader = true;

    $scope.Init = function () {

        $scope.GetAllTickets();
        $scope.GetLookUpsAndSettings();
    };

    $scope.GetLookUpsAndSettings = function () {

        $.ajax({
            type: 'GET',
            url: utility.myHost + "Home/GetDynamicLookUpData?lookType=TicketDocumentTypes&defaultBlank=false",
            success: function (result) {
                $scope.TicketDocumentTypes = result;
            }
        });
    };

    $scope.GetAllTickets = function () {

        showOverlay();
        $scope.ShowPreLoader = true;

        $.ajax({
            type: "GET",
            url: utility.myHost + "Ticket/GetAllTickets",
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                $timeout(function () {
                    if (!result.IsError && result !== null) {
                        $scope.$apply(function () {
                            $scope.AllTicketDetails = result.Response;

                            $scope.TicketCount = $scope.AllTicketDetails.length;
                        });
                        hideOverlay();
                    }
                }, 1000);
            },
            error: function () {
                hideOverlay();
            },
            complete: function (result) {
                
            }
        });
        
    };

    function showOverlay() {
        $timeout(function () {
            $scope.$apply(function () {
                $("#TicketOverlay").fadeIn();
            });
        });
    };

    function hideOverlay() {
        $scope.ShowPreLoader = false;
        $timeout(function () {
            $scope.$apply(function () {
                $("#TicketOverlay").fadeOut();
            });
        });
    };

    $scope.SaveTicketCommunication = function (ticket, notes) {

        showOverlay();
        $scope.ShowPreLoader = true;

        if (!notes) {
            callToasterPlugin('error', "Input the information you'd like to send.");
            return false;
        }

        var communicationDTO = {
            "TicketCommunicationIID": 0,
            "TicketID": ticket.TicketIID,
            "Notes": notes,
        };

        $.ajax({
            type: "POST",
            url: utility.myHost + "Ticket/SaveTicketCommunication",
            data: JSON.stringify(communicationDTO),
            contentType: "application/json;charset=utf-8",
            success: function (result) {

                $timeout(function () {
                    if (!result.IsError && result !== null) {

                        callToasterPlugin('success', "Sent successfully");

                        hideOverlay();
                        $scope.$apply(function () {

                            $scope.GetAllTickets();
                        });
                    }
                    else {
                        callToasterPlugin('error', "Sending failed!");

                        hideOverlay();
                    }
                }, 1000);
            },
            error: function () {
                callToasterPlugin('error', "Sending failed!");
                hideOverlay();
            },
            complete: function (result) {

            }
        });

    };

    $scope.GenerateButtonClick = function () {

        $scope.IsError = false;
        $scope.ErrorMessage = null;

        $scope.TicketGeneration = {
            "Subject": null,
            "Description1": null,
            "DocumentTypeID": null,
        };

        $('#TicketGenerationModal').modal('show');
    };

    $scope.DocumentTypeChange = function (documentType) {
        //$scope.SelectedDocumentType.Key = documentType.Key;
        //$scope.SelectedDocumentType.Value = documentType.Value;

        $scope.TicketGeneration.DocumentTypeID = documentType.Key;

        var radioButton = document.getElementById("documentType_" + documentType.Key);
        if (radioButton) {
            radioButton.checked = true;
        }

        $scope.IsError = false;
        $scope.ErrorMessage = null;
    };

    $scope.SubmitAndGenerateTicket = function () {

        $scope.IsError = false;
        $scope.ErrorMessage = null;

        if (!$scope.TicketGeneration.DocumentTypeID) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Select any type!";
        }
        else if (!$scope.TicketGeneration.Subject) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Subject is required for submission!";
        }
        else if (!$scope.TicketGeneration.Description1) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Notes is required for submission!";
        }
        else if ($scope.TicketGeneration.Subject.length < 10) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Subject must be at least 10 letters long!";
        }
        else if ($scope.TicketGeneration.Description1.length < 20) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Notes must be at least 20 letters long!";
        }
        else {

            var button = document.getElementById("TicketSubmitionButton");
            button.innerHTML = "Submitting...";
            button.disabled = true;

            $.ajax({
                type: "POST",
                url: utility.myHost + "Ticket/GenerateTicket",
                data: JSON.stringify($scope.TicketGeneration),
                contentType: "application/json;charset=utf-8",
                success: function (result) {

                    $timeout(function () {
                        if (!result.IsError && result !== null) {

                            $('#TicketGenerationModal').modal('hide');

                            callToasterPlugin('success', "Service request successfully submitted.");

                            hideOverlay();
                            $scope.$apply(function () {

                                $scope.GetAllTickets();
                            });
                        }
                        else {
                            callToasterPlugin('error', "Sending failed!");

                            button.innerHTML = "Submit";
                            button.disabled = false;

                            hideOverlay();
                        }
                    }, 1000);
                },
                error: function () {
                    callToasterPlugin('error', "Sending failed!");
                    hideOverlay();
                },
                complete: function (result) {

                }
            });
        }

        //$('#TicketGenerationModal').modal('hide');
    };


}]);