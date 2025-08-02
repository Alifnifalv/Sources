
$(document).ready(function () {

    $("#filterbuttonoption").click(function () {
        $("#filterview").slideToggle();
    });

    //code to customize normal dropdown 

    $(".editrow1").click(function () {
        $('.editrow1-open').toggle();
    });

    $(".editrow2").click(function () {
        $('.editrow2-open').toggle();
    });

    $(".editrow3").click(function () {
        $('.editrow3-open').toggle();
    });

    $(".editrow4").click(function () {
        $('.editrow4-open').toggle();
    });

    // adding class to li which has submenu's 
    // this is to prevent anchor tag if li has submenu
    $('.bodyleftnav li').each(function (e) {
        var item = $(this).find("ul");
        if (item.length > 0) {
            $(this).addClass('parent_li');
        }
        else {
            $(this).addClass('nosubitem');
        }
    });

    $('.bodyleftnav li ul > li').each(function (e) {
        var child = $(this).find("ul");
        if (child.length > 0) {
            $(this).removeClass('parent_li').addClass('submenu');
        }
    });  


    $('#leftnavfullview').on('click', function () {
        $(this).toggleClass('active');
        $('.bodyleftnav').removeClass('active').addClass('inactive');
        if ($(this).hasClass('active')) {
            $('li.parent_li > ul').attr('style', '');
            $('li.parent_li > a.open').closest('li').find('> ul').slideDown('fast');
            $('.bodyleftnav').addClass('active').removeClass('inactive');
            $('.bodyleftnav').animate({
                width: "256px",
                minWidth: "256px"
            }, 100);
            $('#LayoutContentSection').animate({
                paddingLeft: "266px"
            }, 100);
            setTimeout(function () {
                $('.bodyleftnav ul li > a span label').show();
            }, 400);
        }
        else {
            $('.bodyleftnav').removeClass('active').addClass('inactive');
            $('.bodyleftnav ul li > a span label').hide();
            $('.bodyleftnav').animate({
                width: "94px",
                minWidth: "94px"
            }, 100);
            $('#LayoutContentSection').animate({
                paddingLeft: "104px"
            }, 100);
        }
    });

    $('.main-body').on('click', '.bodyleftnav.inactive .searchmenu', function () {
        $(this).addClass('expand');
    });


    //code for leftmenu starts here

    $('table.listing').on('click', 'tr.product-list', function (e) {
        $('table.listing tr').removeClass('editable');
        $(this).addClass('editable');
        if ($(this).hasClass('editable')) {
            $('table.listing tr td input[type="text"]').prop('readonly', false);
        }
        else {
            $('table.listing tr td input[type="text"]').prop('readonly', true);
        }
    }); //listing click

    $(".message").on('click', function () {
        $(".mymessage").slideToggle(200);
    });

    $(".profilepicname").on('click', function () {
        $(".myprofile-dropdown").slideToggle(200);
    });


    $("#myTab a").click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    $(".datepicker").datepicker();

    $('.listing').on('click', '.product-name a', function (e) {
        e.preventDefault();
        $('.product_details').fadeOut();
        $(this).closest('td').find('.product_details').fadeIn(200);
    });

    //summary header panel toggle view //
    $('.windowcontainer .languageoption ').each(function () {
        $(this).resizable();
    });
    
    $("body").on('click', '.pagecontent-column', function (e) {
        var currentwindow = $(this).closest('.windowcontainer').attr('windowindex');
        //alert(currentwindow);
        if ($(e.target).closest(".fieldpopup").length === 0 && $(e.target).closest(".languageoption").length === 0 && $(e.target).closest(".languageoption .ui-resizable-handle").length === 0) {  
            $(".windowcontainer[windowindex=" + currentwindow + "] .languageoption").hide();
            //$("windowcontainer  .languageoption").hide();
            $(".windowcontainer[windowindex=" + currentwindow + "] .fieldpopup").removeClass('active');
        }
    });

    $("body").on('click', '.pagecontent', function (e) {
        var currentwindow = $(this).closest('.windowcontainer').attr('windowindex');
        //alert(currentwindow);
        if ($(e.target).closest(".fieldpopup").length === 0 && $(e.target).closest(".languageoption").length === 0 && $(e.target).closest(".languageoption .ui-resizable-handle").length === 0) {
            $(".windowcontainer[windowindex=" + currentwindow + "] .languageoption").hide();
            //$("windowcontainer  .languageoption").hide();
            $(".windowcontainer[windowindex=" + currentwindow + "] .fieldpopup").removeClass('active');
        }

        if ($(e.target).closest(".tablewrapper td.toggleContainer table td").length === 0) {
            $(".popup.workFlow").removeClass('show');
        }
    });

    $("body").on('click', function (e) {
        if ($(e.target).closest('table.listing tr td').length === 0) {
            $('table.listing tr.product-list').removeClass('editable');
        }

        if ($(e.target).closest('.product-name a').length === 0) {
            $('.product_details').fadeOut(200);
        }

        if ($(e.target).closest('.profilepicname').length === 0) {
            $('.myprofile-dropdown').slideUp('fast');
        }

        if ($(e.target).closest('.message').length === 0) {
            $(".mymessage").slideUp('fast');
        }
        if ($(e.target).closest('.searchmenu').length === 0) {
            $('.searchmenu').removeClass('expand');
        }
        if ($(e.target).closest('.button-orange.paybtn, .button-orange.savebtn').length === 0) {
            $('.errormsg').removeClass('show');
        }
        if ($(e.target).closest('.search_box').length === 0) {
            $('.search_box').removeClass('active');
        }
        if ($(e.target).closest('.headerFilter, .pageheadername a').length === 0) {
            $('.headerFilter').fadeOut();
        }

        if ($(e.target).closest('.attendanceStatus, .attendents_Status').length === 0) {
            $('.attendents_Status').hide();
        }

        //if ($(e.target).closest('.popup.gridpopupfields').length === 0) {
        //    $('.popup.gridpopupfields').fadeOut(100);      
        //    setTimeout(function (e) {
        //        $('.popup.gridpopupfields').css('top', '');
        //        $('.popup.gridpopupfields').removeClass('fixedpos');
        //    },200);
           
        //}
    }); //body click target

    $(".custom-select").each(function () {
        $(this).wrap("<span class='select-wrapper'></span>");
        $(this).after("<span class='holder'></span>");
    });

    $(".custom-select").change(function () {
        var selectedOption = $(this).find(":selected").text();
        $(this).next(".holder").text(selectedOption);
    }).trigger('change');

    $(".custom-checkbox").each(function () {
        $(this).wrap("<span class='check-wrapper'></span>");   
    });

    $(".custom-checkbox").on('change', function () {
        if ($(this).is(':checked')) {
            $(this).closest('span').addClass('checked');
        }
        else {
            $(this).closest('span').removeClass('checked');
        }
    });

    $('.fl-advsearch').on('click', 'tbody td', function () {
        $('.fl-advsearch tbody tr').removeClass('row-selected');
        $(this).closest('tr').addClass('row-selected');
    });

    $('body').on('click', '.searchforclick', function () {
       // alert();
        $('.search_box').addClass('active');
    });
       
    //===script for dashboard starts===//
    //$(".draggable").draggable();

    //$("#sortable").sortable();

    $("#barchartclose").click(function () {
        $("#barchart").hide();
    });
    $("#linechartclose").click(function () {
        $("#linechart").hide();
    });
    $("#piechartclose").click(function () {
        $("#piechart").hide();
    });
    $("#recentactclose").click(function () {
        $("#recentact").hide();
    });
    $("#qiklaunchclose").click(function () {
        $("#qiklaunch").hide();
    });

    //===script for dashboard ends===//

    //code for leftmenu tree structure starts here

    
    //code for leftmenu tree structure starts here
    if (document.documentElement.clientWidth <= 992) {
        $(".leftnaviconfull").removeClass("active");
        $('.bodyleftnav').removeClass('active').addClass('inactive');
        $('.bodyleftnav ul li > a span label').hide();
        $(".bodyleftnav ul li.nosubitem").on('click', function () {
            $('.bodyleftnav').removeClass('active').addClass('inactive');
            $('.bodyleftnav ul li > a span label').hide();
            $('.bodyleftnav').animate({
                width: "50px",
                minWidth: "50px"
            }, 100);
        });
    }
    if (document.documentElement.clientWidth <= 590) {
        $(".leftnaviconfull").removeClass("active");
        $(".bodyleftnav").addClass("inactive").removeClass("active");
        $("#leftnavfullview").click(function () {
            $(".bodyleftnav").addClass("inactive").removeClass("active");
            $(this).toggleClass("show");
            if($(this).hasClass("show")) {
                $(".bodyleftnav.inactive").addClass("showmobmenu");
            }
            else {
                $(".bodyleftnav.inactive").removeClass("showmobmenu");
            }
           
        });
        $(".bodyleftnav ul li.nosubitem").on('click', function () {
            $('.bodyleftnav').hide();
        });
    }

    $('body').on('mouseover', '.tooltipp', function () {
        var tooltiptext = $(this).find('span').text();
        var textwidth = $(this).find('span').width();
        var containerwidth = $(this).width();
        if ( textwidth >= containerwidth) {
            $(this).append('<div class="tooltipwrap"><div class="tooltip-inner">' + tooltiptext + '</div></div>');
        }   
    });

    $('body').on('mouseout', '.tooltipp', function () {
        $('.tooltipwrap').remove();
    });

    $('body').on('mouseover', 'span.select2-chosen', function () {
        var selectedtext = $(this).find('span').text();
        var select2width = $(this).closest('.select2-container').find('span.select2-chosen:nth-of-type(2)').width();
        var selectedtextwidth = $(this).closest('.select2-container').find('span.select2-chosen span').width();
        if (selectedtextwidth > select2width) {
            $(this).closest('.select2-container').append('<div class="tooltipwrap"><div class="tooltip-inner">' + selectedtext + '</div></div>');
        }
    });

    $('body').on('mouseout', '.select2-chosen', function () {
        $('.tooltipwrap').remove();
    });

    $('body').on('click', '.buttonscrollleft', function () {
        $(this).closest('.topmenuwrap').find('.topmenuwrap-inner').animate({ scrollLeft: "-=" + 200 });
    });
    $('body').on('click', '.buttonscrollright', function () {
        $(this).closest('.topmenuwrap').find('.topmenuwrap-inner').animate({ scrollLeft: "+=" + 200 });
    }); 

    $('body').on('click', '.dockToggle', function () {
        $(this).toggleClass("activeDoc");
        if ($(this).hasClass("activeDoc")) {
            $(".overlaySummaryView").addClass('show');
        }
        else {
            $(".overlaySummaryView").removeClass('show');
        }
    });

    $(document).on("scroll", function () {
        $(".gridItemPopup").fadeOut("fast");
        $(".gridItemOverlay").hide();
    });


    $(window).on("scroll", function () {
        $(".pageheader.stickdiv, .pageheadername.stickdiv").removeClass("sticktotop").css("max-width", "");
        $(".windowcontainer").removeClass("stickytab");
        var windowContainer = $(document).find(".windowcontainer.active");
        var stickywidth = $(windowContainer).find('.pageheader.stickdiv, .pageheadername.stickdiv').outerWidth();
        if ($(this).scrollTop() > 56) {
            if ($(windowContainer).find(".pageheader.stickdiv").length > 0) {
                $(windowContainer).addClass("stickytab");
                $(".pageheader.stickdiv, .pageheadername.stickdiv", windowContainer).addClass("sticktotop").css("max-width", stickywidth);
            }
        }
        else {
            $(".pageheader.stickdiv, .pageheadername.stickdiv", windowContainer).removeClass("sticktotop").css("max-width", "");
            $(windowContainer).removeClass("stickytab");
        }
    });

    $("body").on('click', 'ul.tabMenu li.tabmenuItem', function () {
        var datatab = $(this).attr('data-tab');
        var hasPin = $('ul.tabMenu li.tabmenuItem').hasClass('pinned');
        $(this).toggleClass("selected");
        if ($(this).hasClass("selected")) {
            $(".smartviewTab:not(.pinned)").addClass('showQuickViewPanel');
            $(".tabMenu li.tabmenuItem").removeClass('selected pinned');
            $(".smartViewInner").removeClass("show");
            $(".smartViewInner[data-tab=" + datatab + "]").addClass("show");
            $(this).addClass('selected');
            if (hasPin) {
                $(this).addClass('pinned');
            }
        }
        else {
            $(this).removeClass('selected');
            $(".smartviewTab:not(.pinned)").removeClass('showQuickViewPanel');
        }
    });

    $("body").on('click', '.smartview_close', function () {
        $(".smartviewTab:not(.pinned)").removeClass('showQuickViewPanel');
        $("ul.tabMenu li.tabmenuItem:not(.pinned)").removeClass('selected');
    });

    $("body").on('click', function (e) {
        if ($(e.target).closest('.smartviewTab, li.tabmenuItem').length === 0) {
            $(".smartviewTab:not(.pinned)").removeClass('showQuickViewPanel');
            $(".tabMenu li.tabmenuItem:not(.pinned)").removeClass('selected');
        }
    });

    //code for smartview ends
    $("body").on('click', '.viewTooltip', function (event) {
        $(".notifyjs-wrapper").remove();
        var currentItem = $(event.currentTarget),
            itemData = currentItem.text();

        if (currentItem.notify) {
            currentItem.notify(itemData, {
                autoHide: false,
                clickToHide: false,
                className: 'warn'
            });
        }
    });

    $(".tabs ul li").on("click", function () {
        var tabattr = $(this).attr("data-tab");
        $(".tabs ul li").removeClass("active");
        $(this).addClass("active");
        if ($(this).hasClass("active")) {
            $(".tabContent").removeClass("active");
            $(".tabContent[data-tab='" + tabattr + "']").addClass("active");
        }
    });

    $("body").on('click', '.export', function () {
        $(this).find("div.slide").slideToggle("fast");
    });

}); //document.ready

$.fn.updateValidation = function () {
    var $this = $(this);
    var form = $this.find("form");  
    form.removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.setDefaults({
        ignore: []
    });
    $.validator.unobtrusive.parse(form);
    return $this;
};

$.fn.validateForm = function () {
    var form = $(this);
    form.unbind('submit').submit(function (e) {
        e.preventDefault();
        form.valid();

        try {
            if (this.hasChildNodes('.tab-pane.htab-content')) {
                var validator = $(this).validate();
                $(this).find("input:not(:disabled):not(:checkbox),select").each(function () {
                    //console.log(this);
                    if (!validator.element(this)) {
                        var container1 = form.find('[navbarid="' + $(this).closest('.innertab,.tab-pane,.panel-collapse').attr('refnavbarid') + '"]a');
                        container1 && container1.length > 0 && container1.hasClass('collapsed') && container1[0].click();

                        var conatiner2 = form.find('[data-target="' + $(this).closest('.innertab,.tab-pane,.panel-collapse').attr('refnavbarid') + '"]a');
                        conatiner2 && conatiner2.length > 0 && container1.hasClass('collapsed') && conatiner2[0].click();

                        if ($(this)[0]) {
                            var control = $(this)[0];
                            setTimeout(function () {
                                control.focus();
                                $(window).scrollTop(control.position().top);
                            }, 500);
                        }

                        return false;
                    }
                });

            }
        }
        catch (ex) { return false; }
        return false;
    });

    form.removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.setDefaults({
        ignore: []
    });
    $.validator.unobtrusive.parse(form);

    form.submit();

    if (form.valid())
        return true;
    else
        return false;
}

$.fn.showMessage = function (scope, timeout, isError, message) {
    scope.IsError = isError;
    scope.Message = message;
    timeout(function () {
        $('.msg-alert').addClass('show');
        if (isError == true) {
            $('.msg-alert').addClass('errormsg');
        }
        else {
            timeout(function () {
                $('.msg-alert').removeClass('show');
                $('.msg-alert').removeClass('errormsg');
            }, 1000);
        }
    });
};

$.fn.hideMessage = function () {
    $('.msg-alert').removeClass('show');
    $('.msg-alert').removeClass('errormsg');
};

$.fn.showGlobalMessage = function (scope, timeout, isError, message, endTime) {
    scope.IsGlobalError = isError;
    scope.GlobalMessage = message;
    endTime = endTime != null || endTime != undefined ? endTime : 1000
    timeout(function () {
        $('.glb-msg-alert').addClass('show');
        if (isError == true) {
            $('.glb-msg-alert').addClass('errormsg');
        }
        timeout(function () {
            $('.glb-msg-alert').removeClass('show');
            $('.glb-msg-alert').removeClass('errormsg');
        }, endTime);
    });
};

$.fn.hideGlobalMessage = function () {
    $('.glb-msg-alert').removeClass('show');
    $('.glb-msg-alert').removeClass('errormsg');
};

var printerWidth = null;
jsPrint = function (htmlString) {

    //window.open('url', 'window name', 'comma,separated,options');

    if (!printerWidth) {
        printerWidth = 300;
    }

    var mywindow = window.open('', '', 'height=600,width=' + printerWidth.toString());
    //mywindow.document.write('<html><head><title></title>');
    /*optional stylesheet*/
    //mywindow.document.write('<link rel="stylesheet" type="text/css" href="http://erp/Content/bootstrap.min.css"><link rel="stylesheet" type="text/css" href="http://erp/Content/bootstrap-theme.min.css"><link rel="stylesheet" type="text/css" href="http://erp/Content/font-awesome.css"><link rel="stylesheet" href="http://erp/css/select2.css"><link rel="stylesheet" type="text/css" href="http://erp/Content/styles.css">');
    // mywindow.document.write('<link rel="stylesheet" type="text/css" href="http://erp/Content/styles.css" />');



    //mywindow.document.write('</head>');
    //if (myWindow.closed) { /* do something, e.g., open it again */ }
    mywindow.document.write(htmlString);
    //mywindow.document.write('</html>');

    window.setTimeout(function () { }, 1000);
    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10
    mywindow.print();
    mywindow.close();
    setTimeout('mywindow.close()', 10);

    return true;
};