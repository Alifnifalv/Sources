app.controller('LoginController', ['$scope', '$http', '$location', "$rootScope", "$window", function ($scope, $http, $location, $rootScope, $window) {
    console.log('LoginController loaded.');
    var x = document.getElementById("#indicator-progress");
    $scope.isLoggingIn = false;
    $scope.CultureLanguages = [];
    $scope.selectedLanguage = "";

    $scope.Init = function (model) {
        $scope.LoginModel = model;
        $scope.IsEmailPasswordIncorrect = false;
        $("#indicator-progress").hide();
        const video = document.getElementById('video');
        const canvas = document.getElementById('canvas');
        const ctx = canvas.getContext('2d');
        $rootScope.CultureLanguage = "";
        $scope.GetCultureLanguages();
        $scope.GetDefaultCultureCode();

        //$scope.selectedLanguage = $scope.CultureLanguages[0];

        // Toggle dropdown
        $scope.dropdownOpen = false;
        $scope.toggleDropdown = function () {
            $scope.dropdownOpen = !$scope.dropdownOpen;
        };

    }



    Signup = function () {
        $location.path('/Signup')

    }
    //const video = document.getElementById('video');
    //const canvas = document.getElementById('canvas');
    //const ctx = canvas.getContext('2d');

    //$scope.startFaceDetection = function() {
    //    navigator.mediaDevices.getUserMedia({ video: true })
    //        .then(stream => {
    //            video.srcObject = stream;
    //            video.play();

    //            const cap = new cv.VideoCapture(video);
    //            const face_cascade = new cv.CascadeClassifier();

    //            // Load the pre-trained face detection model (cascade classifier)
    //            const modelUrl = 'haarcascade_frontalface_default.xml';

    //            face_cascade.load(modelUrl);
    //            if (face_cascade.empty()) {
    //                console.error('Error loading face cascade classifier.');
    //                // Handle the error gracefully (e.g., display an error message, disable face detection)
    //                return; // Or take other appropriate action
    //            }

    //            video.addEventListener('play', () => {
    //                const height = video.videoHeight;
    //                const width = video.videoWidth;

    //                const processFrame = () => {
    //                    if (!video.paused && !video.ended) {
    //                        const height = video.videoHeight;
    //                        const width = video.videoWidth;

    //                        const frame = new cv.Mat(height, width, cv.CV_8UC4);
    //                        cap.read(frame);

    //                        // Convert the frame to grayscale (necessary for face detection)
    //                        const gray = new cv.Mat();
    //                        cv.cvtColor(frame, gray, cv.COLOR_RGBA2GRAY);

    //                        // Detect faces using the loaded model
    //                        const faces = new cv.RectVector();
    //                        face_cascade.detectMultiScale(gray, faces, 1.1, 4);

    //                        // Draw rectangles around detected faces on canvas
    //                        canvas.width = width;
    //                        canvas.height = height;
    //                        ctx.clearRect(0, 0, canvas.width, canvas.height);

    //                        for (let i = 0; i < faces.size(); ++i) {
    //                            const face = faces.get(i);
    //                            ctx.strokeStyle = 'red';
    //                            ctx.lineWidth = 2;
    //                            ctx.strokeRect(face.x, face.y, face.width, face.height);
    //                        }

    //                        frame.delete();
    //                        gray.delete();
    //                        faces.delete();

    //                        requestAnimationFrame(processFrame);
    //                    }
    //                };
    //                video.addEventListener('loadedmetadata', () => {
    //                    // Start processing frames here
    //                    processFrame();
    //                });
    //            });

    //        })
    //        .catch(err => {
    //            console.error('Error accessing webcam:', err);
    //        });
    //}

    //$scope.faceDetectionModel = function () {
    //    $scope.startFaceDetection();

    //}


    //$scope.capture = function () {

    //    // New width and height for the scaled image
    //    var newWidth = 200; // Adjust as needed
    //    var newHeight = 150; // Adjust as needed

    //    // Create a FormData object
    //    var formData = new FormData();

    //    // Convert canvas content to a Blob
    //    canvas.toBlob(function (blob) {
    //        // Append the Blob to the FormData with a key name 'photo' (adjust the name if needed)
    //        formData.append('photo', blob, 'scaled_image.png');

    //        // Now you can send formData to the server using AJAX
    //        $.ajax({
    //            url: utility.myHost + 'Account/Face',
    //            type: 'POST',
    //            processData: false, // Prevent automatic transformation of formData
    //            contentType: false, // Set appropriate content type based on photo data
    //            data: formData,
    //            success: function (result) {
    //                // Handle successful response (e.g., display success message)
    //            },
    //            error: function (jqXHR, textStatus, errorThrown) {
    //                // Handle errors (e.g., console.error, alert user)
    //            },
    //            complete: function () {
    //                // Optional cleanup or post-operation tasks
    //            }
    //        });
    //    }, 'image/png'); // You can use 'im

    //}


    $scope.Signin = function (event) {
        event.preventDefault();

        if ($('#frmLogin').valid() == true) {
            LoginViewLoaderWithOverlay();

            $.ajax({
                url: utility.myHost + 'Account/Login',
                type: 'POST',
                data: $scope.LoginModel,
                success: function (result) {
                    if (result.IsSuccess == true) { // Success
                        /*var redirectUrl = utility.myHost + '?1';*/
                        var redirectUrl = utility.myHost + 'Home/SchoolSelection';
                        if (result.UserSettings) {
                            utility.UserCache.UserSettings = result.UserSettings;
                            var layout = result.UserSettings.find(setting => setting.SettingCode.toLowerCase() == 'layout');
                            var theme = result.UserSettings.find(setting => setting.SettingCode.toLowerCase() == 'theme');
                            var lang = $scope.selectedLanguage.code;

                            if (layout) {
                                redirectUrl = redirectUrl + '&layout=' + layout.SettingValue;
                            }

                            if (theme) {
                                redirectUrl = redirectUrl + '&theme=' + theme.SettingValue;
                            }

                            if (lang) {
                                redirectUrl = redirectUrl + '\?language=' + lang;
                                $rootScope.CultureLanguage = lang;
                            }
                        }

                        utility.redirect(redirectUrl);
                        window.setTimeout(function () {
                            LoginHideLoaderWithOverlay();
                        }, 500)
                    } else { // Failure
                        LoginHideLoaderWithOverlay();
                        $scope.IsEmailPasswordIncorrect = true;
                        $scope.$apply();
                    }
                }
            })
        }
    }

    //Type any text in email input field, setting boolean property (IsEmailPasswordIncorrect) is false
    $scope.UserIDKeyup = function () {
        $scope.IsEmailPasswordIncorrect = false;
    }

    //Type any text in email input field, setting boolean property (IsEmailPasswordIncorrect) is false
    $scope.EmailKeyup = function () {
        $scope.IsEmailPasswordIncorrect = false;
    }

    //Type any text in password input field, setting boolean property (IsEmailPasswordIncorrect) is false
    $scope.PasswordKeyup = function () {
        $scope.IsEmailPasswordIncorrect = false;
    }

    //form is valid on submit login loading button loader with overlay
    function LoginViewLoaderWithOverlay() {
        $("#LoginOverlay").fadeIn();
        $("#LoginButtonLoader").fadeIn();
        $scope.isLoggingIn = true;
    }

    //
    function LoginHideLoaderWithOverlay() {
        $("#LoginOverlay").fadeOut();
        $("#LoginButtonLoader").fadeOut();
        $scope.isLoggingIn = false;
    }

    $scope.GetCultureLanguages = function () {

        $scope.CultureLanguages = [
            { name: "English", code: "en", flag: "united-states.svg" },
            { name: "Arabic", code: "ar", flag: "arabic.png" },
            { name: "French", code: "fr", flag: "france.svg" },
        ];
    }

    $scope.selectLanguage = function (lang) {
        $scope.selectedLanguage = lang;
        $scope.dropdownOpen = false;
        //$scope.GetTranslation(lang.name);

        console.log("Selected Language:", lang);

        $window.location = '\?language=' + lang.code;
    };

    $scope.GetDefaultCultureCode = function () {

        const urlParams = new URLSearchParams(window.location.search);
        const languageCode = urlParams.get("language");

        if (languageCode) {

            $scope.selectedLanguage = $scope.CultureLanguages.find(lang => lang.code === languageCode);

        } else {

            $http({
                method: 'Get', url: "GetSettingValueByKey?settingKey=" + "DEFAULT_CULTURE",
            }).then(function (result) {
                $scope.language = result.data;

                $scope.selectedLanguage = $scope.CultureLanguages.find(lang => lang.code === result.data);
            })
        }
    };

    $scope.GetTranslation = function (key) {

        $http({
            method: 'Get', url: "GetTranslation?key=" + key,

        }).then(function (response) {
            $scope.TranslatedValue = response.data;
        })
            .catch(function (error) {
                console.error("Error fetching translation:", error);
            });
    };


}]);

