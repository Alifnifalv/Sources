reportController = function() {
    initialize = function (report) {
        $(document).ready(function () {
            var splittedDateTime = _dateTimeFormat.split(" ");
            var dateTimeFormat = splittedDateTime[0];

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: dateTimeFormat,
                timepicker: false
            });

            //$('[datafield=AccountID].select2').each(function (index, element) {
            //    var selectedData = $(element.parentElement).find('[type=hidden]').val();

            //    if (selectedData != "") {
            //        $.each(JSON.parse(selectedData), function (index, data) {
            //            var option = new Option(data.text, data.id, true, true);
            //            $(element).append(option).trigger('change');
            //        });
            //    }
            //});

            //$('[datafield=AllAccountID].select2').each(function (index, element) {
            //    var selectedData = $(element.parentElement).find('[type=hidden]').val();

            //    if (selectedData != "") {
            //        $.each(JSON.parse(selectedData), function (index, data) {
            //            var option = new Option(data.text, data.id, true, true);
            //            $(element).append(option).trigger('change');
            //        });
            //    }
            //});

            $('.select2').each(function (index, element) {
                $(element).select2({
                    multiple: element.attributes.multiple !== null,
                    allowClear: true,
                    placeholder: true,
                    ajax: {
                        url: utility.myHost + 'Mutual/GetLazyLookUpDataByReportField?columnName=' + element.attributes["datafield"].value,
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

                //// Fetch the preselected item from hidden field, and add to the control
                var preSelectedVal = $(element.parentElement).find('[type=hidden]').val();
                if (preSelectedVal != "") {
                    // create the option and append to Select2
                    var preSelectedOptions = $.parseJSON(preSelectedVal);
                    $.each(preSelectedOptions, function (index, val) {
                        var option = new Option(val.text, val.id, true, true);
                        $(element).append(option).trigger('change');

                        // manually trigger the `select2:select` event
                        $(element).trigger({
                            type: 'select2:select',
                            params: {
                                data: val
                            }
                        });
                    });
                }

            });

            //$('.print').click(function (event) {
            //    ////var reportUrl = "Home/GeneratePDFReports?reportName=" + reportFullName + "&HeadID=" + $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID + '&returnFileBytes=' + true;
            //    //var reportUrl = "Home/ViewReports?reportName=" + reportFullName + "&HeadID=" + headID; // $scope.CRUDModel.Model.MasterViewModel.TransactionHeadIID;
            //    //$('#' + windowName).append('<script>function onLoadComplete() { }</script><center></center><iframe reportname="' + reportName + '" src=' + reportUrl + ' width=100% height=1200px"></></iframe>');
            //    //var $iFrame = $('iframe[reportname=' + reportName + ']');
            //    //$iFrame.on('load', function () {
            //    //    $("#Load", $('#' + windowName)).hide();
            //    //});     
            //});
        });

        $('.js-data-example-ajax').select2({
            ajax: {
                url: 'https://api.github.com/search/repositories',
                dataType: 'json'
                // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
            }
        });
    };

    return {
        Initialize: initialize
    };
};
