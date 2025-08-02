reportController = function() {
    initialize = function (report) {
        $(document).ready(function () {
            $('.date-picker').datetimepicker({
                format: 'DD/MM/YYYY'
            });

            $('[datafield=AllAccountID].select2').select2({
                multiple: false,
                allowClear: true,
                placeholder: true,
                ajax: {
                    url: utility.myHost + '/Mutual/GetLazyLookUpData?lookType=Account&lookupName=Accounts',
                    dataType: 'json',
                    type: "GET",
                    quietMillis: 50,
                    // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
                    data: function (params) {
                        var queryParameters = {
                            searchText: params.term == undefined ? '' : params.term
                        };
                        return queryParameters;
                    },
                    processResults: function (data) {
                        return {
                            results: $.map(data.Data, function (item) {
                                return {
                                    text: item.Value,
                                    id: item.Key
                                };
                            })
                        };
                    }
                }
            }).on('change', function (e) {
                $(e.currentTarget.parentElement).find('[type=hidden]').val(JSON.stringify($(e.currentTarget).select2('data')));
            });

            $('[datafield=AccountID].select2').select2({
                multiple: false,
                allowClear: true,
                placeholder: true,
                ajax: {
                    url: utility.myHost + '/RVMission/Get_AllCustomers_Accounts?lookupName=LookUps.CustomerAccounts',
                    dataType: 'json',
                    type: "GET",
                    quietMillis: 50,
                    // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
                    data: function (params) {
                        var queryParameters = {
                            searchText: params.term == undefined ? '' : params.term
                        };
                        return queryParameters;
                    },
                    processResults: function (data) {
                        return {
                            results: $.map(data.Data, function (item) {
                                return {
                                    text: item.Value,
                                    id: item.Key
                                };
                            })
                        };
                    }
                }
            }).on('change', function (e) {
                $(e.currentTarget.parentElement).find('[type=hidden]').val(JSON.stringify($(e.currentTarget).select2('data')));
            });

            $('[datafield=AccountID].select2').each(function (index, element) {
                var selectedData = $(element.parentElement).find('[type=hidden]').val();

                if (selectedData != "") {
                    $.each(JSON.parse(selectedData), function (index, data) {
                        var option = new Option(data.text, data.id, true, true);
                        $(element).append(option).trigger('change');
                    });
                }
            });

            $('[datafield=AllAccountID].select2').each(function (index, element) {
                var selectedData = $(element.parentElement).find('[type=hidden]').val();

                if (selectedData != "") {
                    $.each(JSON.parse(selectedData), function (index, data) {
                        var option = new Option(data.text, data.id, true, true);
                        $(element).append(option).trigger('change');
                    });
                }
            });

            $('.print').click(function (event) {
                ////var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
                //var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + headID; // $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
                //$('#' + windowName).append('<script>function onLoadComplete() { }</script><center></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
                //var $iFrame = $('iframe[reportname=' + reportName + ']');
                //$iFrame.on('load', function () {
                //    $("#Load", $('#' + windowName)).hide();
                //});     
            });
        });
    };

    return {
        Initialize: initialize
    };
};
