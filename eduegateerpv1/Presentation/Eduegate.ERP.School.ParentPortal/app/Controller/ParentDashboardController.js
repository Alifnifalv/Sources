//var app = angular.module('rzSliderDemo', ['rzSlider', 'ui.bootstrap'])
app.controller("ParentDashboardController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope, $uibModal) {

    //$controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("ParentDashboardController Loaded");
    $scope.Students = [];
    $scope.SelectedStudent = [];
    $scope.FeeMonthly = [];
    $scope.FeeMonthlyHis = [];
    $scope.FeeTypes = [];
    $scope.FeeTypeHis = [];
    $scope.StudentDetails = null;
    $scope.StudentLeaveApplication = null;
    $scope.FineDues = [];
    $scope.FineHis = [];

    $scope.WeekDay = {};
    $scope.ClassTime = {};
    $scope.StudentName = {};
    $scope.Class = {};
    $scope.Section = {};
    $scope.ClassId = {};
    $scope.StudentTransportDetails = [];
    $scope.AttendenceData = [];
    $scope.am4corepercent = 80;

    $scope.init = function () {
        $scope.getstudentTransportDetails = function () {
            //showOverlay();
            $scope.AssignmentList = [];
            $.ajax({
                type: "GET",
               // data: { studentId: studentId },
                url: utility.myHost + "/Home/GetStudentTransportDetails",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (!result.IsError && result !== null) {
                        $scope.StudentTransportDetails = result;
                        console.log($scope.StudentTransportDetails);
                    }
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });
        }
        $scope.getattendancePercentageByParentLoginid = function () {
            //showOverlay();
            $scope.AttendenceData = [];
            $.ajax({
                type: "GET",
                // data: { studentId: studentId },
                url: utility.myHost + "/Home/ToGetAttendancePercentageByParentLoginid",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    var resultData = {};
                    if (!result.IsError && result !== null) {
                        var i = 0;
                        result.forEach(function (data) {
                            resultData["category"] = data.StudentName + ' (' + data.PercentAttendance+' %)';
                            resultData["value"] = data.PercentAttendance;
                            resultData["full"] = 100;
                            $scope.AttendenceData[i]=resultData;
                            i = i + 1;
                            resultData = {};
                        });
                        if (result.length > 7) {
                            $scope.am4corepercent = 5;
                        }
                        else if (result.length > 5) {
                            $scope.am4corepercent = 20;
                        }
                        else if (result.length > 3) {
                            $scope.am4corepercent = 30;
                        }
                    }
                  
                    $scope.gaugeChart_001();
                },
                error: function () {

                },
                complete: function (result) {
                    //hideOverlay();
                }
            });
        }
        $scope.getstudentTransportDetails();
        $scope.getattendancePercentageByParentLoginid();
    };

    $scope.AttendanceChart = function() {
        var options = {
            series: [
                {
                    name: "Net Profit",
                    data: [44, 55, 57, 56, 61, 75],
                },
                
            ],
            chart: {
                type: "bar",
                height: 400,
                dropShadow: {
                    enabled: true,
                    color: "#000",
                    top: 18,
                    left: 7,
                    blur: 10,
                    opacity: 0.2,
                },
                toolbar: {
                    show: false,
                },
            },
            colors: ["#5C9FFB", "#AEAEAE"],
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: "30%",
                    endingShape: "rounded",
                },
            },
            dataLabels: {
                enabled: false,
            },
            stroke: {
                show: true,
                width: 2,
                colors: ["transparent"],
            },
            xaxis: {
                categories: ["jan", "Feb", "Mar", "Apr", "May", "Jun"],
                labels: {
                    style: {
                        colors: "#9aa0ac",
                    },
                },
            },
            yaxis: {
                title: {
                    text: "$ (thousands)",
                },
                labels: {
                    style: {
                        color: "#9aa0ac",
                    },
                },
            },
            fill: {
                opacity: 1,
            },
            tooltip: {
                theme: "dark",
                marker: {
                    show: true,
                },
                x: {
                    show: true,
                },
            },
        };

        var chart = new ApexCharts(document.querySelector("#home-data3_chart2_attendance"), options);
        chart.render();
    }
    $scope.AttendanceChart();

    $scope.gaugeChart_001=function() {
        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("gaugeChart_001", am4charts.RadarChart);

        // Add data
        chart.data = $scope.AttendenceData;
        // Make chart not full circle
        chart.startAngle = -90;
        chart.endAngle = 180;
        chart.innerRadius = am4core.percent($scope.am4corepercent);
        chart.height = 400;
        // Set number format
        chart.numberFormatter.numberFormat = "#.#'%'";

        // Create axes
        var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "category";
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.renderer.grid.template.strokeOpacity = 0;
        categoryAxis.renderer.labels.template.horizontalCenter = "right";
        categoryAxis.renderer.labels.template.fontWeight = 500;
        categoryAxis.renderer.labels.template.adapter.add("fill", function (
            fill,
            target
        ) {
            return target.dataItem.index >= 0
                ? chart.colors.getIndex(target.dataItem.index)
                : fill;
        });
        categoryAxis.renderer.minGridDistance = 10;

        var valueAxis = chart.xAxes.push(new am4charts.ValueAxis());
        valueAxis.renderer.grid.template.strokeOpacity = 0;
        valueAxis.min = 0;
        valueAxis.max = 100;
        valueAxis.strictMinMax = true;
        valueAxis.renderer.labels.template.fill = am4core.color("#9aa0ac");

        // Create series
        var series1 = chart.series.push(new am4charts.RadarColumnSeries());
        series1.dataFields.valueX = "full";
        series1.dataFields.categoryY = "category";
        series1.clustered = false;
        series1.columns.template.fill = new am4core.InterfaceColorSet().getFor(
            "alternativeBackground"
        );
        series1.columns.template.fillOpacity = 0.08;
        series1.columns.template.cornerRadiusTopLeft = 20;
        series1.columns.template.strokeWidth = 0;
        series1.columns.template.radarColumn.cornerRadius = 20;

        var series2 = chart.series.push(new am4charts.RadarColumnSeries());
        series2.dataFields.valueX = "value";
        series2.dataFields.categoryY = "category";
        series2.clustered = false;
        series2.columns.template.strokeWidth = 0;
        series2.columns.template.tooltipText = "{category}: [bold]{value}[/]";
        series2.columns.template.radarColumn.cornerRadius = 20;

        series2.columns.template.adapter.add("fill", function (fill, target) {
            return chart.colors.getIndex(target.dataItem.index);
        });

        // Add cursor
        chart.cursor = new am4charts.RadarCursor();
    }

    
}]);