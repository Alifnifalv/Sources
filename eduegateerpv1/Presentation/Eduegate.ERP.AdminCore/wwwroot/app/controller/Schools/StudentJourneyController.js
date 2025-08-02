app.controller('StudentJourneyController', ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {
    console.log("StudentJourneyController Loaded");

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.searchQuery = '';
    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.Init = function (model, window) {
        hideOverlay();
        var data = JSON.parse(model);
        var referenceID = data.referenceID;
        var imageContentID = data.imageContentID;
        GetStudentJourney(95, referenceID, imageContentID);
    }
    $scope.GetStudentImage = function (studentId, imageContentID) {
        return utility.ImageUrl + "StudentProfile/" + studentId + "/Thumbnail/" + imageContentID;
    }

    function GetStudentJourney(chartID, studentID, imageContentID) {

        var url = "Schools/School/GetStudentJourney?chartID=" + chartID + "&studentID=" + studentID;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data == null || result.data.length == 0) {
                    return false;
                }
                let stdNodeId = null;
                const yIncrement = 150;
                const groupPositions = {};
                let groupYPositions = {};
                const specificGroupsYPositions = {};

                const groupXIncrement = 100;
                let currentXPosition = 0;
                const ctrl = this;
                ctrl.elementdata = [];
                result.data.ColumnDatas.forEach((row, index) => {

                    if (!ctrl.elementdata) {
                        ctrl.elementdata = [];
                    }
                    if (!$scope.elementdata) {
                        $scope.elementdata = [];
                    }
                    const values = JSON.parse(row);
                    const Lvl = values[0];
                    const id = values[1];
                    const parentId = values[2];
                    const Grp = values[3];  // Group (CLS, ATD, ACH, EXM)
                    const Label = values[4];
                    const Description = values[7];

                    if (!(Grp in groupPositions)) {
                        groupPositions[Grp] = currentXPosition;
                        groupYPositions[Grp] = 0;
                        currentXPosition += groupXIncrement;
                    }

                    let xPos = groupPositions[Grp] + Lvl * 100;
                    let yPos;

                    if (Grp === "STD") {
                        stdNodeId = id.toString();
                        xPos = 0;
                        yPos = 0;
                    } else if (Grp === "CLS") {

                        yPos = groupYPositions[Grp];
                        groupYPositions[Grp] += yIncrement;


                        specificGroupsYPositions[id] = {
                            classYPos: yPos,
                            currentYPos: yPos + 50
                        };
                    } else if (Grp === "ATD" || Grp === "EXM") {

                        const parentClassYPos = specificGroupsYPositions[parentId];


                        let xOffset = 0;

                        if (Grp === "EXM") {
                            xOffset = 250;
                            //yPos = parentClassYPos.currentYPos;
                            //specificGroupsYPositions[parentId].currentYPos += yIncrement / 2;  // Increase the y-spacing for EXM nodes
                        } else if (Grp === "ATD") {
                            xOffset = 50;
                            //yPos = parentClassYPos.currentYPos;
                            //specificGroupsYPositions[parentId].currentYPos += yIncrement / 4;
                        }

                        xPos = groupPositions["CLS"] + Lvl * 100 + xOffset;
                        yPos = parentClassYPos.currentYPos;

                        specificGroupsYPositions[parentId].currentYPos += yIncrement / 4;

                    }
                    else {

                        yPos = groupYPositions[Grp];
                        groupYPositions[Grp] += yIncrement;
                    }

                    /*   const popoverContent = `Group: ${Grp}<br>Label: ${Label}<br>Level: ${Lvl}`;*/
                    const popoverContent = `Description: ${Description}`;


                    ctrl.elementdata.push({
                        data: {
                            id: id.toString(),
                            label: Label,
                            grp: Grp,
                            popover: popoverContent
                        },
                        position: {
                            x: xPos,
                            y: yPos
                        },
                        locked: Grp === "STD",

                    });

                    if (parentId) {
                        ctrl.elementdata.push({
                            data: {
                                source: parentId.toString(),
                                target: id.toString()
                            }
                        });
                    }
                });
                $scope.cy = cytoscape({
                    container: document.getElementById('cy'),
                    elements: ctrl.elementdata,
                    wheelSensitivity: 0.1,
                    style: [ // Style for the graph
                        {
                            selector: 'node',
                            style: {
                                //'shape': 'polygon',  // Set shape for all nodes
                                'background-image': 'url(../Images/student.png)', // Default background image
                                'background-clip': 'none',
                                'background-fit': 'cover',
                                'background-position': 'center',
                                'background-width': '80px',
                                'background-height': '80px',
                                'label': 'data(label)',
                                'height': 60,
                                'width': 60,
                                "text-valign": 'center',
                                'text-margin-y': function (node) { return -node.height() + 12 },

                                "text-halign": "center",
                                'color': '#444444',
                                //'text-outline-width': 2,
                                //'text-outline-color': '#fff',
                                'text-background-opacity': 1,
                                'text-background-color': '#dbdfe9',
                                'text-background-shape': 'roundrectangle',
                                'text-border-color': '#dbdfe9',
                                'text-border-width': 8,
                                'text-border-radius': 10,
                                'text-border-opacity': 1,
                                'background-opacity': 0,
                                'font-size': '14px',
                                'font-weight': '600',
                                'border-width': 0,
                                'border-opacity': 0.5,   
                            },
                        },
                        {
                            selector: 'node[grp="STD"]',
                            style: {
                                'background-image': $scope.GetStudentImage(studentID, imageContentID),
                                //'background-image': function (ele) {
                                //    const studentID = ele.data('studentID');  // Replace with correct data field
                                //    const imageContentID = ele.data('imageContentID');  // Replace with correct data field
                                //    const image = $scope.GetStudentImage(studentID, imageContentID);

                                //    // If the image is not available, return a default image
                                //    return image ? image : 'url(../Images/student.png)'; // Use the default image if no value
                                //},
                                // Other styles specific to 'STD' group (if needed)
                                'text-background-color': '#D9D9D9',
                                'text-border-color': '#D9D9D9',
                            },
                        },
                        {
                            selector: 'node[grp="ACD"]',
                            style: {
                                'background-image': 'url(../Images/year.png)',
                                // Other styles specific to 'ACD' group (if needed)
                                'text-background-color': '#FFBFBF',
                                'text-border-color': '#FFBFBF',
                            },
                        },
                        {
                            selector: 'node[grp="CLS"]',
                            style: {
                                'background-image': 'url(../Images/class.png)',
                                // Other styles specific to 'CLS' group (if needed)
                                'text-background-color': '#DAF0D9',
                                'text-border-color': '#DAF0D9',
                            },
                        },
                        {
                            selector: 'node[grp="EXM"]',
                            style: {
                                'background-image': 'url(../Images/ecam.png)',
                                // Other styles specific to 'EXM' group (if needed)
                                'text-background-color': '#CAE9FF',
                                'text-border-color': '#CAE9FF',
                            },
                        },
                        {
                            selector: 'node[grp="ATD"]',
                            style: {
                                'background-image': 'url(../Images/attendance.png)',
                                // Other styles specific to 'ATD' group (if needed)
                                'text-background-color': '#CBF6FF',
                                'text-border-color': '#CBF6FF',
                            },
                        },
                        {
                            selector: 'node[grp="MRK"]',
                            style: {
                                'background-image': 'url(../Images/mark.png)',
                                // Other styles specific to 'ATD' group (if needed)
                                'text-background-color': '#DCD6A1',
                                'text-border-color': '#DCD6A1',
                            },
                        },
                        {
                            selector: 'node[grp="AVG"]',
                            style: {
                                'background-image': 'url(../Images/average.png)',
                                // Other styles specific to 'AVG' group (if needed)
                                'text-background-color': '#FFE8CD',
                                'text-border-color': '#FFE8CD',
                            },
                        },
                        {
                            selector: 'node[grp="ACH"]',
                            style: {
                                'background-image': 'url(../Images/Achievment.png)',
                                // Specific styles for 'ACH' group
                                'text-background-color': '#E7D4FF',
                                'text-border-color': '#E7D4FF',
                            },
                        },
                        {
                            selector: 'edge',
                            style: {
                                'width': 1,
                                'line-color': '#676767',
                                'target-arrow-color': '#676767',
                                'target-arrow-shape': 'chevron',
                                'target-arrow-size': 10,
                                'curve-style': 'bezier',
                            }
                        }
                    ],

                    layout: {
                        name: 'preset',
                        padding: 3
                    }
                });

                $scope.cy.on('mouseover', 'node', function (event) {
                    const node = event.target;
                    const popoverContent = node.data('popover');

                    if (popoverContent) {
                        showPopover(node, popoverContent);
                    } else {
                        console.warn('Popover content is undefined for node:', node.id());
                    }
                });


                $scope.cy.on('mouseout', 'node', function (event) {
                    hidePopover();
                });


            })
            .catch(function (error) {
                console.error('Error fetching data:', error);

            });
    }
    //function showPopover(x, y, content) {

    //    let popover = document.querySelector('.popover');

    //    if (!popover) {
    //        popover = document.createElement('div');
    //        popover.classList.add('popover');
    //        document.body.appendChild(popover);
    //    }


    //    popover.classList.remove('ng-hide');


    //    if (content) {
    //        popover.innerHTML = content;
    //        popover.style.display = 'block';


    //        popover.style.left = `${x + window.scrollX + 10}px`;
    //        popover.style.top = `${y + window.scrollY - 30}px`;

    //    } else {
    //        console.warn('Popover content is undefined.');

    //        popover.style.display = 'none';
    //    }
    //}
    function showPopover(node, content) {
        let popover = document.querySelector('.popover');

        if (!popover) {
            popover = document.createElement('div');
            popover.classList.add('popover');
            document.body.appendChild(popover);
        }

        popover.classList.remove('ng-hide');

        if (content) {
            popover.innerHTML = content;
            popover.style.display = 'block';
          
            const nodePosition = node.renderedPosition();

            const offsetX = 50; 
            const offsetY = -30; 

            popover.style.left = `${nodePosition.x + offsetX}px`;
            popover.style.top = `${nodePosition.y + offsetY}px`;

        } else {
            console.warn('Popover content is undefined.');
            popover.style.display = 'none';
        }
    }

    function hidePopover() {
        const popover = document.querySelector('.popover');
        if (popover) {
            popover.style.display = 'none';
        }
    }
    //$scope.searchNodes = function (query) {

    //    $scope.cy.nodes().style({
    //        'color': '#000',
    //        'border-color': '#000',
    //        'font-size': '18px',
    //        'display': 'element'
    //    });

    //    if (query) {
    //        const matchedNodes = $scope.cy.nodes().filter(function (ele) {
    //            return ele.data('label').toLowerCase().includes(query.toLowerCase());
    //        });

    //        matchedNodes.style({
    //            'color': 'red',
    //            'border-color': 'blue',
    //            'border-width': 4,
    //            'font-size': '22px'
    //        });
    //        $scope.cy.nodes().difference(matchedNodes).style('display', 'none');
    //    }
    //};

    $scope.searchNodes = function (query) {

        $scope.cy.nodes().style({
            'background-color': 'SteelBlue',
            'color': '#444444',           // Reset label color to black
            'font-size': '14px',       // Reset font size
            'text-shadow': 'none',     // Reset text shadow
            'border-width': 0,
            'border-color': '#000',
            'text-background-color': '#D9D9D9',
            'text-border-color': '#D9D9D9',

        });

        if (query) {
            const matchedNodes = $scope.cy.nodes().filter(function (ele) {
                return ele.data('label').toLowerCase().includes(query.toLowerCase());
            });

            // Highlight matched nodes by changing label color, border color, font size, and adding a glow effect
            matchedNodes.style({
                'background-color': 'SteelBlue',
                'color': 'red',          // Change label color to red
                'font-size': '14px',     // Increase font size for better visibility
                'text-shadow': '0 0 10px rgba(255, 0, 0, 0.75)', // Apply glow effect to text
         
            });
        }
    };


    $scope.clearSearch = function () {
        $scope.cy.nodes().style({
            'color': '#000',
            'border-color': '#000',
            'font-size': '16px',
            'display': 'element'
        });
    };

}]);