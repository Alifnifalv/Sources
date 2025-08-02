app.controller("CaseManagementController", ["commonService", "$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function (commonService, $scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("CaseManagementController Loaded");

    $scope.Init = function (model, windowname, type) {
        $scope.type = type;
    };

    function showOverlay() {
        $('.preload-overlay', $(vm.windowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $(vm.windowContainer)).hide();
    }

    //#region Tab open and close
    $scope.onCaseManagementViewTab = function (tabId) {
        if (tabId == "Tab_01") {
            $scope.TabName = "Tab_01";

            $("#Tab_01").show();

            $("#Tab_02").hide();
            $("#Tab_03").hide();
            $("#Tab_04").hide();
            $("#Tab_05").hide();

            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element?.classList.add("active");

            //Deactivate tabs
            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element?.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element?.classList.remove("active");

            var tab4Element = document.getElementById("Tab_04_nav");
            tab4Element?.classList.remove("active");

            var tab5Element = document.getElementById("Tab_05_nav");
            tab5Element?.classList.remove("active");
        }
        else if (tabId == "Tab_02") {
            $scope.TabName = "Tab_02";

            $("#Tab_02").show();

            $("#Tab_01").hide();
            $("#Tab_03").hide();
            $("#Tab_04").hide();
            $("#Tab_05").hide();

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element?.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element?.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element?.classList.remove("active");

            var tab4Element = document.getElementById("Tab_04_nav");
            tab4Element?.classList.remove("active");

            var tab5Element = document.getElementById("Tab_05_nav");
            tab5Element?.classList.remove("active");
        }
        else if (tabId == "Tab_03") {
            $scope.TabName = "Tab_03";

            $("#Tab_03").show();

            $("#Tab_01").hide();
            $("#Tab_02").hide();
            $("#Tab_04").hide();
            $("#Tab_05").hide();

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element?.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element?.classList.remove("active");

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element?.classList.remove("active");

            var tab4Element = document.getElementById("Tab_04_nav");
            tab4Element?.classList.remove("active");

            var tab5Element = document.getElementById("Tab_05_nav");
            tab5Element?.classList.remove("active");
        }
        else if (tabId == "Tab_04") {
            $scope.TabName = "Tab_04";

            $("#Tab_04").show();

            $("#Tab_01").hide();
            $("#Tab_02").hide();
            $("#Tab_03").hide();
            $("#Tab_05").hide();

            var tab4Element = document.getElementById("Tab_04_nav");
            tab4Element?.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element?.classList.remove("active");

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element?.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element?.classList.remove("active");

            var tab5Element = document.getElementById("Tab_05_nav");
            tab5Element?.classList.remove("active");
        }
        else if (tabId == "Tab_05") {
            $scope.TabName = "Tab_05";

            $("#Tab_05").show();

            $("#Tab_01").hide();
            $("#Tab_02").hide();
            $("#Tab_03").hide();
            $("#Tab_04").hide();

            var tab5Element = document.getElementById("Tab_05_nav");
            tab5Element?.classList.add("active");

            //Deactivate tabs
            var tab1Element = document.getElementById("Tab_01_nav");
            tab1Element?.classList.remove("active");

            var tab2Element = document.getElementById("Tab_02_nav");
            tab2Element?.classList.remove("active");

            var tab3Element = document.getElementById("Tab_03_nav");
            tab3Element?.classList.remove("active");

            var tab4Element = document.getElementById("Tab_04_nav");
            tab4Element?.classList.remove("active");
        }
    };
    //#endregion End tab open and close

    $scope.CreateTicket = function (row, documentTypeID, type) {
        var parameters = {
            "DocumentType": documentTypeID.toString(),
            "Priority": 1,
            "Action": 1,
            "Status": 1,
            "StudentID": row.StudentIID,
            //"ReferenceID": row.StudentFeeDueIID,
            "LoginID": row.ParentLoginID,
            "ParentID": row.ParentID,
            "ParentName": row.ParentCode + " - " + row.FatherName,
            "ReferenceTypeID": row.ReferenceTypeID,
            "Parent": { "Key": row.ParentID, "Value": row.ParentCode + " - " + row.FatherName }
        }

        if (type.toLowerCase() == "feeoutstanding") {
            if (documentTypeID) {
                url = utility.myHost + 'Mutual/GetNextTransactionNumber?documentTypeID=' + documentTypeID;

                $http.get(url).then(function (response) {
                    parameters.TransactionNo = response.data;

                    parameters.Subject = "Fee Outstanding - Important Information";
                    parameters.CustomerNotification = true;
                    parameters.IsSendMail = true;
                    parameters.ProblemDescription = "<p>The Fee Outstanding details:</p>  <p>Admission Number : " + row.AdmissionNumber + "</p>  <p>Student : " + row.Student;
                    parameters.IsAutoCreation = true;

                    $scope.OpenNewCasePage(parameters);

                });
            }

        }
    };

    $scope.OpenNewCasePage = function (parameters) {
        $timeout(function () {
            $scope.$apply(function () {
                var view = "Frameworks/CRUD/Create?screen=Ticket";
                var title = "Create Case";
                var windowName = "Ticket";

                var menuParameters = Object.keys(parameters).map(function (key) {
                    return key + "=" + parameters[key];
                }).join(";");

                var activeitem = $('ul.bodyrightmain-tab').find('li.active')?.position()?.left;
                $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
                //if (event !== undefined) {
                //    event.preventDefault();
                //    if (event.stopPropagation) event.stopPropagation();
                //}

                $("html, body").animate({ scrollTop: 0 }, "fast");

                if (title === undefined)
                    title = 'Create ' + view.substring(view.indexOf('/') + 1);

                if ($scope.ShowWindow('Create' + windowName, title, 'Create' + windowName))
                    return;

                $scope.AddWindow('Create' + windowName, title, 'Create' + windowName);

                var createUrl;

                if (menuParameters !== null) {
                    if (view.includes("?"))
                        createUrl = view + '&parameters=' + menuParameters;
                    else
                        createUrl = view + '?parameters=' + menuParameters;
                }
                else {
                    createUrl = view;
                }

                $.ajax({
                    url: createUrl,
                    type: 'GET',
                    success: function (result) {
                        $('#Create' + windowName, "#LayoutContentSection").replaceWith($compile(result)($scope)).updateValidation();
                        $scope.ShowWindow('Create' + windowName, title, 'Create' + windowName);
                        var topmenucontainer = $("body").find(".topmenuwrap-inner").outerWidth();
                        var itemwidth = 0;
                        $('ul.bodyrightmain-tab').children().each(function () {
                            itemwidth += $(this).outerWidth();
                        });
                        if (itemwidth > topmenucontainer) {
                            $('.btncontrols-wrap').show();
                        }
                        else {
                            $('.btncontrols-wrap').hide();
                        }

                        var activeitem = $('ul.bodyrightmain-tab').find('li.active')?.position()?.left;
                        $('.topmenuwrap-inner').animate({ scrollLeft: activeitem + 'px' });
                    },
                    error: function (request, status, message, b) {
                        $().showGlobalMessage($root, $timeout, true, request.responseText);
                    }
                });
            });
        });
    };

    $scope.MailFeeDueReport = function (row, reportName) {
        var dueDTO = {
            "StudentID": row.StudentIID,
            "ClassID": row.ClassID,
            "AdmissionNo": row.AdmissionNumber,
            "StudentName": row.Student,
            "ParentEmailID": row.ParentEmail,
            "ParentLoginID": row.ParentLoginID,
            "ReportName": reportName
        }

        //$http({
        //    method: 'POST',
        //    url: url,
        //    data: dueDTO
        //}).then(function (result) {
        //    if (result.data.IsError) {
        //        $().showGlobalMessage($root, $timeout, true, "Fee due statement sending failed");
        //    }
        //    else {
        //        $().showGlobalMessage($root, $timeout, false, "Fee due statement successfully sent");
        //    }

        //    return false;
        //}, function () {

        //});

        $.ajax({
            url: utility.myHost + "Supports/Ticket/GenerateAndSendFeeDueMailReportToParent",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(dueDTO),
            success: function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, "Fee due statement sending failed");
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, "Fee due statement successfully sent");
                }
                //hideOverlay();
            },
            complete: function (result) {
            }
        });
    }

}]);