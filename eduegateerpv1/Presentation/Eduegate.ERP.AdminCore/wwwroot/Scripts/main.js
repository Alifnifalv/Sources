///* I have modified Flickity Transformer to allow scrolling with mouse wheel, see example without scrolling here:
//https://codepen.io/elcontraption/pen/RGPboR */

//let flkty = new Flickity(".Slider", {
//    setGallerySize: false,
//    pageDots: false,
//    initialIndex: 1 // I set the initial index to the middle fruit.
//});

///** MY CODE BELOW TO ALLOW SCROLLING **/

//// get the carousel so can move next/previous.
//let $carousel = $(".Slider").flickity();

//// Check for mouse scroll using jquery mousewheel.
//$("body").on("mousewheel", function (event) {
//    // use flickity methods to go previous or next depending on scroll direction

//    if (event.deltaY == -1) {
//        $carousel.flickity("previous");
//    }

//    if (event.deltaY == 1) {
//        $carousel.flickity("next");
//    }
//});

///* End of my code, below is default transformer, can modify this for different effects */

//const transformer = new FlickityTransformer(flkty, [
//    {
//        name: "scale",
//        stops: [
//            [-300, 0.5],
//            [0, 0.8],
//            [300, 0.5]
//        ]
//    },
//    {
//        name: "translateY",
//        stops: [
//            [-1000, 500],
//            [0, 0],
//            [1000, 500]
//        ]
//    },
//    {
//        name: "rotate",
//        stops: [
//            [-300, -30],
//            [0, 0],
//            [300, 30]
//        ]
//    },
//    {
//        // Declare perspective here, before rotateY. At least two stops are required, hence the duplication.
//        name: "perspective",
//        stops: [
//            [0, 600],
//            [1, 600]
//        ]
//    },
//    {
//        name: "rotateY",
//        stops: [
//            [-300, 45],
//            [0, 0],
//            [300, -45]
//        ]
//    }
//]);
// external js: flickity.pkgd.js






const flkty = new Flickity('.Slider', {
    setGallerySize: false,
    pageDots: false,
    initialIndex: 1 // I set the initial index to the middle fruit.,


})


//var $carousel = $('.carousel').flickity({
//    imagesLoaded: true,
//    percentPosition: false,
//});

//var $imgs = $carousel.find('.carousel-cell img');
//// get transform property
//var docStyle = document.documentElement.style;
//var transformProp = typeof docStyle.transform == 'string' ?
//    'transform' : 'WebkitTransform';
//// get Flickity instance
//var flkty1 = $carousel.data('flickity');

//$carousel.on('scroll.flickity', function () {
//    flkty1.slides.forEach(function (slide, i) {
//        var img = $imgs[i];
//        var x = (slide.target + flkty1.x) * -1 / 3;
//        img.style[transformProp] = 'translateX(' + x + 'px)';
//    });
//});


const transformer = new FlickityTransformer(flkty, [
    {
        name: "scale",
        stops: [
            [-300, 0.8],
            [0, 1],
            [300, 0.8]
        ]
    },
    {
        name: "translateY",
        stops: [
            [-1000, 500],
            [0, 0],
            [1000, 500]
        ]
    },
    //{
    //    name: "rotate",
    //    stops: [
    //        [-300, -30],
    //        [0, 0],
    //        [300, 30]
    //    ]
    //},
    {
        // Declare perspective here, before rotateY. At least two stops are required, hence the duplication.
        name: "perspective",
        stops: [
            [1, 600],
            [0, 600]
        ]
    },
    {
        name: "rotateY",
        stops: [
            [-300, -30],
            [0, 0],
            [300, 30]
        ]
    }
])



