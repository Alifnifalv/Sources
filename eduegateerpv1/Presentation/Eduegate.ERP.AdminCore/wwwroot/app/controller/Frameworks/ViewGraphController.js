app.controller('ViewGraphController', ['$scope', '$http', 'moment', '$compile','$timeout',
    function ($scope, $http, moment, $compile, $timeout) {

       

        $scope.init = function (nodes, edges) {            
            loadGraph(nodes, edges);
        }

        function loadGraph(data, edges) {
            $timeout(function () {
                var cy = window.cy = cytoscape({
                    container: document.getElementById('cy'),

                    style: [
                        {
                            selector: 'node',
                            style: {
                                'content': 'data(label)',
                                'background-color': '#ad1a66'
                            }
                        },
                        {
                            selector: ':parent',
                            style: {
                                'background-opacity': 0.333
                            }
                        },
                        {
                            selector: 'edge',
                            style: {
                                'width': 3,
                                'line-color': '#ad1a66',
                                'curve-style': 'straight'
                            }
                        },
                        {
                            selector: 'edge.meta',
                            style: {
                                'width': 2,
                                'line-color': 'red'
                            }
                        },
                        {
                            selector: ':selected',
                            style: {
                                'overlay-color': "#6c757d",
                                'overlay-opacity': 0.3,
                                'background-color': "#999999"
                            }
                        },
                        {
                            selector: "node.cy-expand-collapse-collapsed-node",
                            style: {
                                "background-color": "darkblue",
                                "shape": "rectangle"
                            }
                        },
                    ],
                    layout: {
                        name: 'dagre',
                        rankDir: 'TR',
                        nodeDimensionsIncludeLabels: true
                    },

                    elements: {
                        nodes: data,
                        edges: edges
                        //    [
                        //    { data: { source: '1', target: '6' } },
                        //    { data: { source: '1', target: '3' } },
                        //]
                    },
                }).on('cxttap', function (event) {
                    if (allSelected('node')) {
                        contextMenu.hideMenuItem('select-all-nodes');
                        contextMenu.showMenuItem('unselect-all-nodes');
                    }
                    else {
                        contextMenu.hideMenuItem('unselect-all-nodes');
                        contextMenu.showMenuItem('select-all-nodes');
                    }
                    if (allSelected('edge')) {
                        contextMenu.hideMenuItem('select-all-edges');
                        contextMenu.showMenuItem('unselect-all-edges');
                    }
                    else {
                        contextMenu.hideMenuItem('unselect-all-edges');
                        contextMenu.showMenuItem('select-all-edges');
                    }
                });

                var allSelected = function (type) {
                    if (type == 'node') {
                        return cy.nodes().length == cy.nodes(':selected').length;
                    }
                    else if (type == 'edge') {
                        return cy.edges().length == cy.edges(':selected').length;
                    }
                    return false;
                }

                var selectAllOfTheSameType = function (type) {
                    if (type == 'node') {
                        cy.nodes().select();
                    } else if (type == 'edge') {
                        cy.edges().select();
                    }
                };
                var unselectAllOfTheSameType = function (type) {
                    if (type == 'node') {
                        cy.nodes().unselect();
                        ;
                    } else if (type == 'edge') {
                        cy.edges().unselect();
                    }
                };

                // demo your core ext
                var contextMenu = cy.contextMenus({
                    menuItems: [
                        {
                            id: 'List',
                            content: 'List',
                            tooltipText: 'List',
                            image: { src: "assets/remove.svg", width: 12, height: 12, x: 6, y: 4 },
                            selector: 'node, edge',
                            onClickFunction: function (event) {
                                //var target = event.target || event.cyTarget;
                                //removed = target.remove();

                                //contextMenu.showMenuItem('undo-last-remove');
                            },
                            hasTrailingDivider: true
                        },
                        {
                            id: 'Create',
                            content: 'Create',
                            tooltipText: 'Create',
                            //selector: 'node, edge',
                            image: { src: "assets/remove.svg", width: 12, height: 12, x: 6, y: 4 },
                            //show: true,
                            //coreAsWell: true,
                            onClickFunction: function (event) {
                                //if (removed) {
                                //    removed.restore();
                                //}
                                //contextMenu.hideMenuItem('undo-last-remove');
                            },
                            hasTrailingDivider: true
                        },                                   
                        {
                            id: 'select-all-nodes',
                            content: 'select all nodes',
                            selector: 'node',
                            coreAsWell: true,
                            show: true,
                            onClickFunction: function (event) {
                                selectAllOfTheSameType('node');

                                contextMenu.hideMenuItem('select-all-nodes');
                                contextMenu.showMenuItem('unselect-all-nodes');
                            }
                        },
                        {
                            id: 'unselect-all-nodes',
                            content: 'unselect all nodes',
                            selector: 'node',
                            coreAsWell: true,
                            show: false,
                            onClickFunction: function (event) {
                                unselectAllOfTheSameType('node');

                                contextMenu.showMenuItem('select-all-nodes');
                                contextMenu.hideMenuItem('unselect-all-nodes');
                            }
                        },
                        {
                            id: 'select-all-edges',
                            content: 'select all edges',
                            selector: 'edge',
                            coreAsWell: true,
                            show: true,
                            onClickFunction: function (event) {
                                selectAllOfTheSameType('edge');

                                contextMenu.hideMenuItem('select-all-edges');
                                contextMenu.showMenuItem('unselect-all-edges');
                            }
                        },
                        {
                            id: 'unselect-all-edges',
                            content: 'unselect all edges',
                            selector: 'edge',
                            coreAsWell: true,
                            show: false,
                            onClickFunction: function (event) {
                                unselectAllOfTheSameType('edge');

                                contextMenu.showMenuItem('select-all-edges');
                                contextMenu.hideMenuItem('unselect-all-edges');
                            }
                        }
                    ]
                });

                setTimeout(function () {
                    cy.reset();
                });
            });
        }
    }])
