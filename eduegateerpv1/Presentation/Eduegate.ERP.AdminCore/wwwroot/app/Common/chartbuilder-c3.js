function ChartBuilder() {
    function createbarchart(bindto, columns, colors, axis, grid, legend, tooltip, title) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                type: 'bar'
            },
            bar: {
                width: {
                    ratio: 0.9
                }
            },
            title: title,
            axis: axis,
            grid: grid,            
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            title: title,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        }
    }

    function createhorizonalbarchart(bindto, columns, colors, axis, grid, legend, tooltip, title) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                type: 'bar'
            },
            bar: {
                width: 10
            },
            padding: {
                left: 30
            },
            title: title,
            axis: {
                rotated: true,
                x: axis.x,
                y: axis.y
            },
            grid: grid,
            legend: {
                show: false
            },
            tooltip: {
                show: tooltip
            },
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            title: title,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        }
    }

    function createAreaSplineChart(bindto, columns, colors, axis, grid, legend, tooltip, type) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                type: type
            },
            bar: {
                width: {
                    ratio: 0.5
                }
            },
            axis: axis,
            grid: grid,
            legend: {
                show: legend
            },
            tooltip: {
                show: tooltip
            },
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        }
    }

    function createlinechart(bindto, columns, colors, axis, grid, legend, tooltip) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                x: 'x',
                columns: columns,
                type: 'line',
                colors: colors
            },

            axis: axis,
            grid: grid,
            legend: {
                show: legend
            },
            tooltip: {
                show: tooltip
            },
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        }
    }

    function createDonutchart(bindto, columns, colors, order, title, label, width) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: [],
                type: 'donut',
                colors: colors,
                order: order
            },
            donut: {
            },
            labels: true,
            tooltip: {
                format: {
                    title: function (d) { return (!d ? null : 'Data ' + d); },
                    value: function (value, ratio, id) {                        
                        var format = (id === 'data1' ? d3.format(',') : d3.format(''));
                        return format(value);
                    }
                }
            }
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            order: order,
            title: title,
            label: label,
            width: width,
            chartobject: chart
        }
    }

    function createPiechart(bindto, columns, colors, order, title, label, width) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: [],
                type: 'pie',
                colors: colors,
                order: order
            },
            pie: {
            },
            labels: true,
            tooltip: {
                format: {
                    title: function (d) { return (!d ? null : 'Data ' + d); },
                    value: function (value, ratio, id) {
                        var format = (id === 'data1' ? d3.format(',') : d3.format(''));
                        return format(value);
                    }
                }
            }
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            order: order,
            title: title,
            label: label,
            width: width,
            chartobject: chart
        }
    }

    function createGaugechart(bindto, columns, colors, order, title, label, width) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: [],
                type: 'gauge',
                colors: colors,
                order: order
            },
            gauge: {
                title: title,
                label: {
                    show: label
                },
                width: width
            },
            labels: true,
            tooltip: {
                format: {
                    title: function (d) { return (!d ? null : 'Data ' + d); },
                    value: function (value, ratio, id) {
                        var format = (id === 'data1' ? d3.format(',') : d3.format(''));
                        return format(value);
                    }
                }
            }
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            order: order,
            title: title,
            label: label,
            width: width,
            chartobject: chart
        }

    }

    function createAreaStepChart(bindto, columns, colors, axis, type, legend, tooltip) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                types: type
            },
            axis: axis,
            tooltip: tooltip,
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            type: type,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        }
    }

    function createStackBarchart(bindto, columns, colors, axis, grid, legend, tooltip, group, thickness) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                type: 'bar',
                groups: [
                    group
                ]
            },
            bar: {
                width: thickness
            },
            axis: axis,
            grid: grid,
            legend: {
                show: legend
            },
            tooltip: {
                show: tooltip
            },
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            group: group,
            thickness: thickness,
            chartobject: chart
        }
    }

    function createAreachart(bindto, columns, colors, axis, grid, legend, tooltip, point, zoom) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                columns: columns,
                colors: colors,
                type: 'area'
            },
            axis: axis,
            grid: grid,
            legend: {
                show: legend
            },
            tooltip: {
                show: tooltip
            },
            point: {
                r: point
            },
            zoom: {
                enabled: zoom
            },
            labels: true
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            point: point,
            zoom: zoom,
            chartobject: chart
        }
    }

    function showlegend(chart, legendnames) {
        chart.chartobject.legend.show(legendnames);
    }  

    function reloaddata(chart, chartSeriesData) {
        chart.chartobject.load({
            bindto: chart.bindto,
            columns: chartSeriesData,
        });
    }

    function reloaddataForXaxisBarchart(chart, chartSeriesData, bindto, category, amount, color) {
        // latter it  will be  change to load data  //
        c3.generate({
            bindto: bindto,
            data: {
                x: 'x',
                columns: chartSeriesData,
                colors: color,
                type: 'bar',

                labels: {
                    format: function (value, ratio, id) {
                        var accurateValue = (Math.round((value * 100) / amount))
                        return accurateValue + '%'
                    }
                }
            },
            bar: {
                width: 20

            },
            axis: {
                x: {
                    type: category // this needed to load string x value
                },
                y: { show: false }
            }
        });
    }

    function unloadData(chart, keyValue) {
        chart.chartobject.unload({
            ids: keyValue
        });
    }

    function unloadAll(chart) {
        chart.chartobject.unload();
    }

    function hideColumnChart(chart, columnName) {
        chart.chartobject.hide(columnName);
    }

    function drawaverageline(chart, averageTotalAmount) {
        chart.chartobject.ygrids([{
            value: averageTotalAmount
        }]);
    }

    return {
        createbarchart: createbarchart,
        createlinechart: createlinechart,
        createAreaStepChart: createAreaStepChart,
        showlegend: showlegend,
        hideColumnChart: hideColumnChart,
        reloaddata: reloaddata,
        drawaverageline: drawaverageline,
        createAreachart: createAreachart,
        createStackBarchart: createStackBarchart,
        createDonutchart: createDonutchart,
        createAreaSplineChart: createAreaSplineChart,
        reloaddataForXaxisBarchart: reloaddataForXaxisBarchart,
        unloadData: unloadData,
        unloadAll: unloadAll,
        createhorizonalbarchart: createhorizonalbarchart,
        createPiechart: createPiechart,
        createGaugechart: createGaugechart
    }
};
