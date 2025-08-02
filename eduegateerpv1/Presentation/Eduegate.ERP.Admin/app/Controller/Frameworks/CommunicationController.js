app.controller("CommunicationController", ["$scope", "$http", "$compile", "$window", "$timeout", function ($scope, $http, $compile, $window, $timeout) {
    console.log("CommunicationController is loaded");

    $scope.Model = null;
    $scope.EmailTemplates = [];
    var windowContainer = null;

    $scope.Init = function (window, model, referenceID) {
        windowContainer = '#' + window;
        $scope.Model = model;
        //$scope.GetMailIDs(referenceID);
        //EmailTemplates
        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=EmailTemplates&defaultBlank=false",
        }).then(function (result) {
            $scope.EmailTemplates = result.data;
        });

        $http({
            method: 'Get', url: "Mutual/GetDynamicLookUpData?lookType=CommunicationTypes&defaultBlank=false",
        }).then(function (result) {
            $scope.CommunicationTypes = result.data;
        });
    }

    //$scope.GetMailIDs = function (referenceID) {
    //    var url = "Mutual/GetMailIDDetailsFromLead?leadID=" + referenceID;
    //    $http({ method: 'Get', url: url })
    //        .then(function (result) {

    //            $scope.Model.From = result.data.From;
    //            $scope.Model.To = result.data.To;

    //        }, function () {

    //        });
    //};

    $scope.submitCommunication = function (model) {
        //    showOverlay();
        var communication = model;

        $("#SubmitCommunicationBtn").html("Sending...");
        $("#SubmitCommunicationBtn").prop("disabled", true);

        $.ajax({
            type: "POST",
            data: JSON.stringify(communication),
            url: utility.myHost + "Mutual/SubmitCommunication",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $("#SubmitCommunicationBtn").html("Sent");
                }

            },
            error: function () {

            },
            complete: function (result) {
                //hideOverlay();
            }
        });
    };

    $scope.changeEmailTemplate = function (model, template) {
        var communicationModel = model;
        model.EmailTemplateKey = template.Key;
        var url = "Mutual/GetEmailTemplateByID?TemplateID=" + template.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                communicationModel.EmailContent = result.data.EmailTemplate;

            }, function () {

            });
    };

    $scope.onCommunicationChange = function (model, type) {
        var id = type.Key;
        model.CommunicationTypeKey = type.Key;
        if (id == ("1")) {
            $("#EmpTemplate").val("");
            $scope.onCommunication = true;
        }
        else {
            $scope.onCommunication = false;
        }
    };

}]);