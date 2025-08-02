$(document).ready(function(){
    $(".featuresTab ul li").on("click", function () {
        $(".featuresTab ul li").removeClass("active");
        $(".featureContent").removeClass("active");
        var activeTab = $(this).attr("data-attr");
        $(".featuresTab ul li[data-attr="+activeTab+"]").addClass("active");
        $(".featureContent[data-attr="+activeTab+"]").addClass("active");
    });

    $("li.hasChildElement").on("click", function () {
        $(this).find("ul").fadeToggle("fast");
    });

    $("body").on("click", function (e) {
        if ($(e.target).closest("li.hasChildElement, li.hasChildElement ul li a").length === 0) {
            $("li.hasChildElement > ul").fadeOut();
        }
    });

    $('select').formSelect();
    $("select[required]").css({
        display: "inline",
        height: 0,
        padding: 0,
        width: 0
    }); 
});

(function (window, ResizableTableColumns, undefined) {
    var store = window.store && window.store.enabled
        ? window.store
        : null;

    var els = document.querySelectorAll('table.data');
    for (var index = 0; index < els.length; index++) {
        var table = els[index];
        if (table['rtc_data_object']) {
            continue;
        }

        var options = {
            store: store,
            maxInitialWidth: 100
        };
        if (table.querySelectorAll('thead > tr').length > 1) {
            options.resizeFromBody = false;
        }

        new ResizableTableColumns(els[index], options);
    }

})(window, window.validide_resizableTableColumns.ResizableTableColumns, void (0));



$(".base-image").slick({
  rows: 0,
  fade: true,
  dots: false,
  swipe: false,
  arrows: false,
  infinite: false,
  draggable: false,
  slidesToShow: 1,
  slidesToScroll: 1,
});

$(".additional-image-wrap").slick({
  rows: 0,
  dots: false,
  arrows: true,
  vertical: true,
  infinite: false,
  slidesToShow: 4,
  slideToScroll: 1,
  asNavFor: ".base-image",
  focusOnSelect: true,
  adaptiveHeight: true,
  verticalSwiping: true,
  responsive: [
    {
      breakpoint: 577,
      settings: {
        vertical: 1,
        variableWidth: 0,
        verticalSwiping: 1,
      },
    },
  ],
});

$(".landscape-left-tab-products").slick({
  rows: 0,
  dots: false,
  arrows: true,
  infinite: true,
  slidesToShow: 6,
  slidesToScroll: 6,
  prevArrow: ".arrow-prev",
  nextArrow: " .arrow-next",
  responsive: [
    {
      breakpoint: 1761,
      settings: {
        slidesToShow: 5,
        slidesToScroll: 5,
      },
    },
    {
      breakpoint: 1301,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4,
      },
    },
    {
      breakpoint: 1051,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
      },
    },
    {
      breakpoint: 992,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4,
      },
    },
    {
      breakpoint: 881,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
      },
    },
    {
      breakpoint: 661,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
      },
    },
    {
      breakpoint: 641,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2,
      },
    },
  ],
});

$('a[data-toggle="tab"]')
  .on("shown.bs.tab", function () {
    $($(this).attr("href"))
      .find(".landscape-right-tab-products")
      .slick({
        rows: 0,
        dots: true,
        arrows: 1,
        infinite: 0,
        slidesToShow: 6,
        slidesToScroll: 6,
        responsive: [
          {
            breakpoint: 1761,
            settings: {
              slidesToShow: 5,
              slidesToScroll: 5,
            },
          },
          {
            breakpoint: 1301,
            settings: {
              slidesToShow: 4,
              slidesToScroll: 4,
            },
          },
          {
            breakpoint: 1051,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
            },
          },
          {
            breakpoint: 992,
            settings: {
              slidesToShow: 4,
              slidesToScroll: 4,
            },
          },
          {
            breakpoint: 881,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 3,
            },
          },
          {
            breakpoint: 641,
            settings: {
              slidesToShow: 2,
              slidesToScroll: 2,
            },
          },
        ],
      });
  })
  .first()
  .trigger("shown.bs.tab");

$(".home-slider").slick({
  rows: 0,
  cssEase: "ease",
  speed: 1000,
  fade: false,
  dots: true,
  arrows: true,
  autoplay: true,
  autoplaySpeed: 5000,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        dots: 1,
      },
    },
  ],
});

$(".top-brands").slick({
  rows: 0,
  dots: false,
  arrows: true,
  infinite: 0,
  slidesToShow: 7,
  slidesToScroll: 7,
  responsive: [
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 6,
        slidesToScroll: 6,
      },
    },
    {
      breakpoint: 1050,
      settings: {
        slidesToShow: 5,
        slidesToScroll: 5,
      },
    },
    {
      breakpoint: 900,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4,
      },
    },
    {
      breakpoint: 750,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
      },
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2,
      },
    },
  ],
});


$(window).scroll(function () {
    var sticky = $(".header-wrap-inner"),
        scroll = $(window).scrollTop();

    if (scroll >= 100) sticky.addClass("sticky show");
    else sticky.removeClass("sticky show");
});


/// some script

$(".sidebar-menu-icon").on("click", function () {
  $(".sidebar-menu-wrap").addClass("active");
});
$(".sidebar-menu-close").on("click", function () {
  $(".sidebar-menu-wrap").removeClass("active");
});

/// Drop Down Append
$(".mega-menu > .dropdown > .menu-item").append(
  '<i class="las la-angle-right"></i>'
);

$(".mega-menu > .fluid-menu > .menu-item").append(
  '<i class="las la-angle-right"></i>'
);

// Baner Height
$(".category-dropdown").css({
  height: $(".home-section-wrap").height() + "px",
});
//alert( $(".home-section-wrap").offset().left    "left: " + offset.left + ", top: " + offset.top );
var offset = $(".home-slider-wrap").offset();

var hidehight = offset.top + $(".home-slider-wrap").height();

var listItems = $(".vertical-megamenu > li");
console.log(listItems.offset().top);

//hidehight = listItems.offset().top+

//console.log(listItems[0].height());
var myheight = "";
var loopi = 1;
for (let li of listItems) {
  let product = $(li);

  var lihight = listItems.offset().top + product.height();

  console.log(lihight);

  if (listItems.offset().top + loopi * product.height() > hidehight) {
    product.css("display", "none");
  }

  console.log(product.height());

  myheight += listItems.offset().top + loopi * product.height() + ",";

  if (product.offset.top > hidehight) {
    //product.hide();
    alert("hided");
  }
  loopi++;
}

$(".sidebar-menu-wrap .list-inline > li").on("click", function () {
  $(".sidebar-menu-wrap .list-inline > li").removeClass("active");
  var listItems = $(".sidebar-menu-wrap .list-inline > li");

  for (let li of listItems) {
    let product = $(li);
    product.children().eq(1).removeClass("open");
    product.children().eq(1).css("display", "none");
  }

  $(this).addClass("active");
  $(this).children().eq(1).addClass("open").css("display", "block");
});

$(".sidebar-menu-wrap .multi-level .menu-item").after(
  "<i class='las la-angle-right'></i>"
);
