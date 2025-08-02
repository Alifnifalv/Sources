function ChartBuilder() {
    function createbarchart(bindto, columns, colors, axis, grid, legend, tooltip,title) {
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
            legend: {
                item: {
                        onclick: function (d) { 
                            chart.focus(d);
                            chart.select(d);
                        }
                    }
            },
            tooltip: {
                show: tooltip
            }
        });

        return {
            bindto: bindto,
            columns: columns,
            title:title,
            colors: colors,
            axis: axis,
            grid: grid,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        };
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
                y : axis.y
            },
            grid: grid,
            legend: {
                show: false
            },
            tooltip: {
                show: tooltip
            }
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
        };
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
                show: legend,
            },
            tooltip: {
                show: tooltip
            }
        });

        return {
            bindto: bindto,
            columns: columns,
            colors: colors,
            axis: axis,
            legend: legend,
            tooltip: tooltip,
            chartobject: chart
        };
    }
   
    function createlinechart(bindto, columns, colors, axis, grid, legend, tooltip) {
        var chart = c3.generate({
            bindto: bindto,
            data: {
                x: 'x',
                columns: columns,
                type: 'line',
                colors: colors,
            },

            axis: axis,
            grid: grid,
            legend: {
                show: legend,
            },
            tooltip: {
                show: tooltip
            }
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
        };
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
                title: title,
                label: {
                    show: label
                },
                width: width,
            },
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
        };
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
            tooltip: tooltip

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
        };
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
                show: legend,
            },
            tooltip: {
                show: tooltip
            }
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
        };
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
                show: legend,
            },
            tooltip: {
                show: tooltip
            },
            point: {
                r: point,
            },
            zoom: {
                enabled: zoom,
            },
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
        };
    }

    function showlegend(chart, legendnames) {
        
        chart.chartobject.legend.show(legendnames);
    }

    function reloaddata(chart, chartSeriesData) {
        chart.chartobject.load({
            bindto: chart.bindto,
            columns: chartSeriesData,
            colors: chart.colors
        });

    }
    function reloaddataForXaxisBarchart(chart, chartSeriesData,bindto,category,amount,color) {
     
      //latter it  will be  change to load data  //
      c3.generate({
        bindto: bindto,
        data: {
            x : 'x',
            columns: chartSeriesData,
            colors: color,
            type: 'bar',
           
            labels:{
                format: function(value, ratio, id) {
                  var accurateValue = (Math.round( (value*100)/amount))
                  return accurateValue+ '%';
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
            y: {show:false}
        },
    });

       
    }
    
    function unloadData(chart, keyValue) {
        chart.chartobject.unload({
            ids: keyValue
        });
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
        createlinechart:createlinechart,
        createAreaStepChart: createAreaStepChart,
        showlegend: showlegend,
        hideColumnChart: hideColumnChart,
        reloaddata: reloaddata,
        drawaverageline: drawaverageline,
        createAreachart: createAreachart,
        createStackBarchart: createStackBarchart,
        createDonutchart: createDonutchart,
        createAreaSplineChart: createAreaSplineChart,
        reloaddataForXaxisBarchart:reloaddataForXaxisBarchart,
        unloadData: unloadData,
        createhorizonalbarchart: createhorizonalbarchart
    };
};