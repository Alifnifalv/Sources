app.controller("ExamQuestionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $root) {
    console.log("ExamQuestionController Loaded");

    $scope.ExamQuestionDatas = [];
    $scope.ExamDuration = 0;
    $scope.IsAutoSave = false;

    $scope.CurrentTime = new Date().toLocaleTimeString();

    $scope.Init = function (questionList, candidateExamMap) {

        $scope.ExamQuestionDatas = questionList;
        $scope.TotalQuestions = questionList.length;
        $scope.CandidateExamMapData = candidateExamMap;
        $scope.ExamStartTime = candidateExamMap.ExamStartTime;
        $scope.ExamEndTime = candidateExamMap.ExamEndTime;

        //fill exam duration start
        var duration = 0;
        if (candidateExamMap.Duration) {
            duration = candidateExamMap.Duration;

            if (candidateExamMap.AdditionalTime) {
                duration += candidateExamMap.AdditionalTime;
            }
        }
        else {
            duration = questionList[0].ExamMaximumDuration;
        }

        $scope.ExamDuration = duration;
        //fill exam duration end

        $scope.LoadCurrentTime();
    };

    $scope.LoadCurrentTime = function () {
        $.ajax({
            type: "POST",
            url: utility.myHost + "/Home/SyncServerTime",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $scope.CurrentDate = new Date(result.match(/\d+/)[0] * 1);

                $scope.CurrentTime = $scope.CurrentDate.toLocaleTimeString();

                syncServerTime();
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    };

    function syncServerTime() {

        //var localDateNow = new Date();
        //var localTimeNow = localDateNow.getTime();

        //var serverTimeNow = $scope.CurrentDate.getTime();
        //var startTime = new Date($scope.ExamStartTime).getTime();

        //if (localTimeNow < serverTimeNow) {
        //    $('#examTimeChangeAlert').modal('show');
        //}

        //if (serverTimeNow < startTime) {

        //    //Exam End Alert open
        //    //$scope.LoadExamEndAlert();

        //    //Automatically save answer
        //    //$scope.AutoSaveAnswer();
        //}
        //else {
            $.ajax({
                type: "POST",
                url: utility.myHost + "/Home/SyncServerTime",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError) {
                        $scope.CurrentDate = new Date(result.match(/\d+/)[0] * 1);

                        $scope.CurrentTime = $scope.CurrentDate.toLocaleTimeString();
                    }
                },
                error: function () {

                },
                complete: function (result) {

                }
            });

            $scope.CheckTimes = setTimeout(function () {
                syncServerTime();
            }, 1 * 1000);
        //}
    }

    $scope.OptionalChanges = function (option, question, type) {

        if (type == "MultipleChoice") {
            if (question.QuestionOptions.length > 0) {
                question.QuestionOptions.forEach(x => {

                    option.IsSelected = true;

                    var unSelectedOptions = question.QuestionOptions.filter(y => y.QuestionOptionMapIID != option.QuestionOptionMapIID);
                    if (unSelectedOptions.length > 0) {
                        unSelectedOptions.forEach(z => z.IsSelected = false);
                    }
                })
            }
        }

        if (type == "MultiSelect") {

            option.IsSelected = true;
        }

        $scope.SaveCandidateQuestionAnswer(option, question, type);
    };

    $scope.SaveCandidateQuestionAnswer = function (option, question, type) {

        var selectedOptions = [];

        selectedOptions = question.QuestionOptions.filter(x => x.IsSelected).map(x => x.QuestionOptionMapIID);

        var candidateAnswer = {
            CandidateAnswerIID: 0,
            CandidateID: question.CandidateID,
            QuestionOptionMapID: option?.QuestionOptionMapIID,
            OtherAnswers: question.QuestionAnswer,
            CandidateOnlineExamMapID: question.CandidateOnlinExamMapID,
            DateOfAnswer: null,
            Comments: null,
            OtherDetails: null,
            OnlineExamID: question.OnlineExamIID,
            OnlineExamQuestionID: question.QuestionIID,
            OnlineExamQuestion: question.Question,
            QuestionOptionMapIDs: selectedOptions,
        };

        if (type == "textAnswer") {
            if (!question.QuestionAnswer) {
                callToasterPlugin('error', "Type any answer to save!", 1000);
                return false;
            }
        }

        $.ajax({
            type: "POST",
            data: JSON.stringify(candidateAnswer),
            url: utility.myHost + "/Home/SaveCandidateAnswer",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.IsError) {
                    callToasterPlugin('error', result.Response);
                }
                else {
                    if (type == "textAnswer") {
                        callToasterPlugin('success', "Answer saved");
                    }
                }
            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    }

    //Fill parent index no for id change
    $scope.FillIndex = function (indexNo) {

        $scope.QuestionIndexNo = indexNo;
    }

    //To Save Answers automatically when ends the exam time
    $scope.AutoSaveAnswer = function () {

        $scope.IsAutoSave = true;
        //$scope.SaveQuestionAnswers();
        $scope.UpdateExamStatus();
    };

    $scope.SaveQuestionAnswers = function () {

        $scope.QuestionAnswers = [];

        $scope.ExamQuestionDatas.forEach(x => {
            if (x.QuestionOptions.length > 0) {
                x.QuestionOptions.forEach(y => {
                    if (y.IsSelected == true) {
                        $scope.QuestionAnswers.push({
                            CandidateAnswerIID: 0,
                            CandidateID: x.CandidateID,
                            QuestionOptionMapID: y.QuestionOptionMapIID,
                            OtherAnswers: x.QuestionAnswer,
                            CandidateOnlineExamMapID: x.CandidateOnlinExamMapID,
                            DateOfAnswer: null,
                            Comments: null,
                            OtherDetails: null,
                            OnlineExamID: x.OnlineExamIID,
                            OnlineExamQuestionID: x.QuestionIID,
                            OnlineExamQuestion: x.Question
                        });
                    }
                })
            }
            else {
                if (x.QuestionAnswer) {
                    $scope.QuestionAnswers.push({
                        CandidateAnswerIID: 0,
                        CandidateID: x.CandidateID,
                        QuestionOptionMapID: null,
                        OtherAnswers: x.QuestionAnswer,
                        CandidateOnlineExamMapID: x.CandidateOnlinExamMapID,
                        DateOfAnswer: null,
                        Comments: null,
                        OtherDetails: null,
                        OnlineExamID: x.OnlineExamIID,
                        OnlineExamQuestionID: x.QuestionIID,
                        OnlineExamQuestion: x.Question
                    });
                }
            }
        });

        if ($scope.QuestionAnswers.length > 0) {

            $.ajax({
                type: "POST",
                data: JSON.stringify($scope.QuestionAnswers),
                url: utility.myHost + "/Home/SaveCandidateFullAnswers",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result) {
                        callToasterPlugin('success', result);

                        if (!$scope.IsAutoSave) {
                            window.location.replace(utility.myHost + "/home/exam");
                        }
                    }
                    else {
                        callToasterPlugin('error', "Something went wrong!");
                    }
                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        }
        else {
            if (!$scope.IsAutoSave) {
                callToasterPlugin('error', "Need atleast one answer!");
            }
        }
    };

    $scope.UpdateExamStatus = function () {
        if ($scope.CandidateExamMapData != null) {
            $.ajax({
                type: "POST",
                data: JSON.stringify($scope.CandidateExamMapData),
                url: utility.myHost + "/Home/UpdateCandidateExamMapStatus",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result) {
                        callToasterPlugin('success', result);

                        if (!$scope.IsAutoSave) {
                            window.location.replace(utility.myHost + "/home/exam");
                        }
                    }
                    else {
                        callToasterPlugin('error', "Something went wrong!");
                    }
                },
                error: function () {

                },
                complete: function (result) {

                }
            });
        }
        else {
            callToasterPlugin('error', "Saving failed, contact your admin!");
        }
    }

    //To get exam end time(countdown time) start
    function startTimer(examDurationInMinutes, display) {

        var endTime = new Date($scope.ExamEndTime).getTime();

        var x = setInterval(function () {
            var currentDate = $scope.CurrentDate;
            //var currentDate = new Date();
            var now = currentDate.getTime();

            //Find the distance between now and the end date
            var distance = endTime - now;

            //Time calculations for days, hours, minutes and seconds
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            display.textContent = hours + "h " + minutes + "m " + seconds + "s";

            if (hours == 0 && minutes == 5 && seconds == 0) {
                $scope.$apply(function () {
                    $scope.ExamEndBalanceTime = display.textContent;
                });
                $scope.LoadTimeAlert();
            }
            $scope.$apply(function () {
                $scope.ExamEndBalanceTime = display.textContent;
            });

            if (distance < 0) {
                clearInterval(x);
                display.textContent = "EXPIRED";

                //Exam End Alert open
                $scope.LoadExamEndAlert();

                //Automatically save answer
                $scope.AutoSaveAnswer();

                return true;
            }

        }, 1000);
    };

    window.onload = function () {
        var examDurationInMinutes = $scope.ExamDuration;
        display = document.querySelector('#endTime');
        startTimer(examDurationInMinutes, display);
    };
    //To get exam end time(countdown time) end

    $scope.LoadTimeAlert = function () {
        var toastElList = [].slice.call(document.querySelectorAll('.toast'))
        var toastList = toastElList.map(function (toastEl) {
            return new bootstrap.Toast(toastEl)
        })
        toastList.forEach(toast => toast.show())
    };

    $scope.LoadExamEndAlert = function () {
        $('#examTimeEndAlert').modal('show');
    };

}]);