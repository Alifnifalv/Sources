app.controller("ScorePredictionController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", '$sce', function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $sce) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });
    $scope.ScorePredictionData = [];
    $scope.IsScorePredictionLoaded = false;
    $scope.IsScorePredictionLoading = false;
    $scope.SelectedStudent = {};
    $scope.SubjectNameMap = {};
    let chartInstance; 

    $scope.init = function () {
        $http({
            method: 'GET',
            url: "Mutual/GetDynamicLookUpData?lookType=ActiveStudents&defaultBlank=false",
        }).then(function (result) {
            $scope.Students = result.data;

            
        });
    };

    $scope.StudentChanges = function (Students) {
        $scope.ScorePrediction(Students.Key);
        $scope.LoadStudentProfile(Students.Key);
    }
    $scope.ScorePrediction = function (SelectedStudent) {
        $scope.IsScorePredictionLoading = true;

        var url = "AI/ScorePrediction/GetScorePrediction?studentID=" + SelectedStudent;

        $http({ method: 'Get', url: url })
            .then(function (result) {
                $scope.ScorePredictionData = JSON.parse(result.data.message);
                $scope.IsScorePredictionLoading = false;

                $scope.FetchAllSubjects();

                var predictedScores = $scope.ScorePredictionData.predicted_score;
                $scope.IsScorePredictionLoaded = true;

                var scores = Object.values(predictedScores);
                var totalScore = scores.reduce((acc, score) => acc + score, 0);
                var averageScore = totalScore / scores.length;

                setTimeout(function () {
                    var ctx = document.getElementById("myChart");

                    if (ctx) {
                        // Destroy existing chart instance if it exists
                        if (chartInstance) {
                            chartInstance.destroy();
                        }

                        var canvasContext = ctx.getContext('2d');
                        var gradient = canvasContext.createLinearGradient(0, 0, 400, 0);
                        gradient.addColorStop(0, "#8E1212");
                        gradient.addColorStop(0.25, "#C08F1E");
                        gradient.addColorStop(0.5, "#C9A420");
                        gradient.addColorStop(0.75, "#2FB73D");
                        gradient.addColorStop(1, "#2FB73D");

                        // Create new chart
                        chartInstance = new Chart(ctx, {
                            type: 'doughnut',
                            data: {
                                labels: ['Average Score', 'Remaining'],
                                datasets: [{
                                    data: [averageScore, 100 - averageScore],
                                    backgroundColor: [
                                        gradient,
                                        'rgba(200, 200, 200, 0.2)'
                                    ],
                                    borderColor: [
                                        'rgba(54, 162, 235, 1)',
                                        'rgba(200, 200, 200, 1)'
                                    ],
                                    borderWidth: 0
                                }]
                            },
                            options: {
                                cutoutPercentage: 40,
                                cutout: '65%',
                                rotation: -120,
                                circumference: 240,
                            
                                plugins: {
                                    datalabels: {
                                        formatter: (value, context) => {
                                            let percentage = (value / context.chart._metasets[context.datasetIndex].total * 100);
                                            return Math.round(percentage) + '%';
                                        },
                                        color: '#071437',
                                        font: {
                                            size: 14,
                                        }
                                    },
                                    tooltip: {
                                        callbacks: {
                                            label: function (tooltipItem) {
                                                if (tooltipItem.label === "Average Score") {
                                                    return `Average Score: ${averageScore.toFixed(2)}%`;
                                                } else {
                                                    return `Remaining: ${(100 - averageScore).toFixed(2)}%`;
                                                }
                                            }
                                        }
                                    },
                                    legend: {
                                        onClick: (e) => e.stopPropagation()
                                    }
                                }
                            },
                            plugins: [ChartDataLabels]
                        });
                    } else {
                        console.error("Canvas element with id 'myChart' not found.");
                    }
                }, 0);
                hideOverlay();

            }, function () {
                hideOverlay();
            });
    };


    $scope.LoadStudentProfile = function (studentId) {

        $.ajax({
            type: "GET",
            data: { studentId: studentId },
            url: "Schools/School/GetStudentDetailsWithProfile",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.StudentDetails = result.Response[0];
                    });
                }

            },
            error: function () {

            },
            complete: function (result) {

            }
        });
    }
    $scope.GetSubjectBySubjectID = function (SubjectID) {
        // Check if the subject name is already fetched
        if ($scope.SubjectNameMap[SubjectID]) {
            return; // Avoid duplicate calls for the same SubjectID
        }

        $.ajax({
            type: "GET",
            url: "Schools/School/GetSubjectBySubjectID?subjectTypeID=" + SubjectID,
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (!result.IsError && result !== null) {
                    $scope.$apply(function () {
                        $scope.SubjectNameMap[SubjectID] = result[0].Value; // Map the subject name
                    });
                }
            },
            error: function () {
                console.error("Error fetching subject name for ID:", SubjectID);
            },
        });
    };
    $scope.FetchAllSubjects = function () {
        angular.forEach($scope.ScorePredictionData.predicted_score, function (score, key) {
            if (!$scope.SubjectNameMap[key]) {
                $scope.GetSubjectBySubjectID(key); // Fetch subject name for each key
            }
        });
    };

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }
    $scope.trustHtml = function (text) {
        if (text) {
            let formattedText = text.replace(/\*\*(.*?)\*\*/g, "<strong>$1</strong>");
            return $sce.trustAsHtml(formattedText);
        }
        return "";
    };


    $scope.init()

}]);