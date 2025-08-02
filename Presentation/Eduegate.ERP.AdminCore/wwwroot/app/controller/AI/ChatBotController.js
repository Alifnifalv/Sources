app.controller("ChatBotController", function ($scope, $http, $timeout) {
    $scope.messages = [];  // Store chat messages

    function scrollToBottom() {
        $timeout(function () {
            let chatContainer = document.querySelector(".scroll-y");
            if (chatContainer) {
                chatContainer.scrollTo({
                    top: chatContainer.scrollHeight,
                    behavior: 'smooth' // Enable smooth scrolling
                });
            }
        }, 100); // Delay to ensure DOM updates
    }

    $scope.getIconForMenu = function (menu) {
        const iconMap = {
            'list': 'fa-list',
            'report': 'fa-file',
            'load': 'fa-download'
            // Add more mappings as needed
        };
        return iconMap[menu.toLowerCase()] || 'fa-comment'; // Default icon
    }

    $scope.sendMessage = function () {
        let userMessage = document.getElementById("userInput").value;
        if (!userMessage) return;

        $scope.messages.push({ sender: "user", text: userMessage });
        scrollToBottom();

        var url = "AI/ChatBot/rasaChat";

        $http.post(url, { sender: "user", message: userMessage })
            .then(function (response) {
                if (response.data.success && response.data.message) {
                    let botMessages = JSON.parse(response.data.message);
                    botMessages.forEach(function (msg) {
                        if (msg.text) {
                            $scope.messages.push({ sender: "bot", text: msg.text });
                        }

                        if (msg.custom) {
                            if (msg.custom.type === "text_popup" && msg.custom.menu_options) {
                                let menuItems = msg.custom.menu_options.flatMap(option => option.actions);
                                $scope.messages.push({
                                    sender: "bot",
                                    type: "menu",
                                    menu_categories: [{ category: "Options", items: menuItems }]
                                });
                            }

                            if (msg.custom.type === "confirmation" && msg.custom.confirmation) {
                                let confirmations = msg.custom.confirmation;
                                $scope.messages.push({
                                    sender: "bot",
                                    type: "confirmation",
                                    choices: confirmations
                                });
                            }

                            if (msg.custom.menu_names) {
                                let categorizedMenu = [];
                                msg.custom.menu_names.forEach(function (category) {
                                    let key = Object.keys(category)[0];
                                    categorizedMenu.push({ category: key, items: category[key] });
                                });

                                $scope.messages.push({
                                    sender: "bot",
                                    type: "menu",
                                    menu_categories: categorizedMenu
                                });
                            }

                            if (msg.custom.type === "link") {
                                $scope.messages.push({ sender: "bot", type: "link", link: msg.custom });
                            }
                        }
                    });
                } else {
                    console.error("Chatbot returned an empty response.");
                }

                scrollToBottom();
            })
            .catch(function (error) {
                console.error("Error communicating with Rasa:", error);
            });

        document.getElementById("userInput").value = "";
    };

    $scope.checkEnter = function ($event) {
        if ($event.keyCode === 13) {
            $scope.sendMessage();
        }
    };

    $scope.sendMessageFromMenu = function (menu) {
        document.getElementById("userInput").value = menu;
        $scope.sendMessage();
    };

    $scope.sendVoiceMessage = function (audioBlob) {
        let formData = new FormData();
        formData.append("VoiceData", audioBlob, "voice_message.wemb");

        var voiceUrl = "AI/ChatBot/rasaChat";

        // Push the user's message immediately to the UI before sending it to Rasa
        let userMessage = {
            sender: "user",
            text: "Voice message sent..." // Placeholder, since we can't transcribe the audio yet
        };
        $scope.messages.push(userMessage);
        scrollToBottom();

        $http.post(voiceUrl, formData, { headers: { "Content-Type": undefined } })
            .then(function (response) {
                if (response.data.success && response.data.message) {
                    let botMessages = JSON.parse(response.data.message);

                    botMessages.forEach(function (msg) {
                        // If 'user_message' exists in 'custom', replace the placeholder text
                        if (msg.custom && msg.custom.user_message) {
                            userMessage.text = msg.custom.user_message;
                        }

                        if (msg.text) {
                            $scope.messages.push({ sender: "bot", text: msg.text });
                        }

                        if (msg.custom) {
                            if (msg.custom.type === "text_popup" && msg.custom.menu_options) {
                                let menuItems = msg.custom.menu_options.flatMap(option => option.actions);
                                $scope.messages.push({
                                    sender: "bot",
                                    type: "menu",
                                    menu_categories: [{ category: "Options", items: menuItems }]
                                });
                            }

                            if (msg.custom.type === "confirmation" && msg.custom.confirmation) {
                                let confirmations = msg.custom.confirmation;
                                $scope.messages.push({
                                    sender: "bot",
                                    type: "confirmation",
                                    choices: confirmations
                                });
                            }

                            if (msg.custom.menu_names) {
                                let categorizedMenu = [];
                                msg.custom.menu_names.forEach(function (category) {
                                    let key = Object.keys(category)[0];
                                    categorizedMenu.push({ category: key, items: category[key] });
                                });

                                $scope.messages.push({
                                    sender: "bot",
                                    type: "menu",
                                    menu_categories: categorizedMenu
                                });
                            }

                            if (msg.custom.type === "link") {
                                $scope.messages.push({ sender: "bot", type: "link", link: msg.custom });
                            }
                        }
                    });
                } else {
                    console.error("Chatbot returned an empty response.");
                }

                scrollToBottom();
            })
            .catch(function (error) {
                console.error("Error sending voice message:", error);
            });
    };

    // Function to play voice message
    $scope.playAudio = function (audioUrl) {
        let audio = new Audio(audioUrl);
        audio.play();
    };

    // --------------------- VOICE MESSAGE HANDLING --------------------- //
    //$scope.sendVoiceMessage = function (audioBlob) {
    //    let formData = new FormData();
    //    formData.append("audio", audioBlob, "voice_message.wemb");

    //    let voiceUrl = "http://192.168.29.100:947/chat"; // Rasa voice endpoint

    //    $http.post(voiceUrl, formData, {
    //        headers: { "Content-Type": undefined }
    //    })
    //        .then(function (response) {
    //            let botMessages = response.data;
    //            botMessages.forEach(function (msg) {
    //                if (msg.text) {
    //                    $scope.messages.push({ sender: "bot", text: msg.text });
    //                }
    //            });
    //            scrollToBottom();
    //        })
    //        .catch(function (error) {
    //            console.error("Error sending voice message:", error);
    //        });
    //};

    // --------------------- AUDIO RECORDING FEATURE --------------------- //
    let mediaRecorder;
    let audioChunks = [];
    $scope.startRecording = function () {
        navigator.mediaDevices.getUserMedia({ audio: true })
            .then(stream => {
                mediaRecorder = new MediaRecorder(stream);
                audioChunks = []; // Reset previous recordings
                mediaRecorder.start();

                mediaRecorder.ondataavailable = event => {
                    audioChunks.push(event.data);
                };

                mediaRecorder.onstop = async () => {
                    let audioBlob = new Blob(audioChunks, { type: "audio/webm" });
                    let audioUrl = URL.createObjectURL(audioBlob); // Create playback URL

                    // Show voice message in UI
                    let voiceMessage = {
                        sender: "user",
                        type: "voice",
                        audioUrl: audioUrl
                    };
                    $scope.messages.push(voiceMessage);
                    $scope.$apply();
                    scrollToBottom();

                    // Send voice message to server
                    $scope.sendVoiceMessage(audioBlob);
                    audioChunks = [];
                };
            })
            .catch(error => {
                console.error("Error accessing microphone:", error);
            });
    };

    $scope.stopRecording = function () {
        if (mediaRecorder && mediaRecorder.state !== "inactive") {
            mediaRecorder.stop();
        }
    };


    $scope.initAudio = function (msg) {
        msg.audio = new Audio(msg.audioUrl);
        msg.audio.addEventListener("loadedmetadata", function () {
            msg.duration = msg.audio.duration;
            msg.progress = 0;
        });

        msg.audio.addEventListener("timeupdate", function () {
            msg.progress = (msg.audio.currentTime / msg.audio.duration) * 100;
            $scope.$apply();
        });

        msg.audio.addEventListener("ended", function () {
            msg.isPlaying = false;
            msg.progress = 0;
            $scope.$apply();
        });
    };

    $scope.togglePlayVoice = function (msg) {
        if (msg.isPlaying) {
            msg.audio.pause();
        } else {
            msg.audio.play();
        }
        msg.isPlaying = !msg.isPlaying;
    };

    app.filter("formatTime", function () {
        return function (seconds) {
            let min = Math.floor(seconds / 60);
            let sec = Math.floor(seconds % 60);
            return min + ":" + (sec < 10 ? "0" : "") + sec;
        };
    });

    $scope.togglePlayVoice = function (msg) {
        if (!msg.audio) {
            msg.audio = new Audio(msg.audioUrl);

            msg.audio.addEventListener('timeupdate', function () {
                $scope.$apply(() => {
                    msg.currentTime = msg.audio.currentTime.toFixed(1);
                    msg.progress = (msg.audio.currentTime / msg.audio.duration) * 100;
                });
            });

            msg.audio.addEventListener('ended', function () {
                $scope.$apply(() => {
                    msg.isPlaying = false;
                    msg.progress = 100; // Ensures it reaches full width
                    setTimeout(() => {
                        $scope.$apply(() => {
                            msg.progress = 0; // Reset after a slight delay
                        });
                    }, 500);
                });
            });
        }

        if (msg.isPlaying) {
            msg.audio.pause();
        } else {
            msg.audio.play();
        }

        msg.isPlaying = !msg.isPlaying;
    };

   
});
