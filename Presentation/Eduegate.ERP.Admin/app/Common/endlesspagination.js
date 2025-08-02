//First load we will do a force load, for supporting the dyanmic table structure
//(all our grids are populating dynamically using datatable js library)
// we attach way point after the first load both the scenarios (static/dyamic)
endlessPagination = function () {
    var pageSize = 100;
    var lastRecord = 0;
    var $waypointDiv;
    var _waypointDivElement;
    var $container;
    var _waypointscrollcontainerClass;
    var waypointInstance;
    var dataCallback;
    var _isWaypointDynamic = false;
    var _pagedData = [];
    var _uniqueColumnName;
    var _currentScrollTop = 0;
    var _windowContainer = null;
    var _wayPointElementID = null;

    function initialize(waypointscrollcontainerClass, isWaypointDynamic, uniqueColumnName, windowContainer, waypointElement,pageLimit) {
        if (pageLimit) {
            pageSize = pageLimit;
        }
        _waypointscrollcontainerClass = waypointscrollcontainerClass;
        _uniqueColumnName = uniqueColumnName;
        lastRecord = 0;
        _windowContainerID = windowContainer;
        _windowContainer = $(_windowContainerID);
        if(_windowContainer.length == 0) {
            console.log("Pagination container is not abilable, cannot generate endless pagination.");
            return;
        }else if(_windowContainer.length > 1) {
            console.log("Pagination container is not abilable, cannot generate endless pagination.");
            return;
        }

        if (isWaypointDynamic) {
            _isWaypointDynamic = isWaypointDynamic;
            _wayPointElementID = 'wayPoint_'+ Utility.generateUUID();
        }

        //if it's a data table the container will be created on the fly else it will be static
        //if static the DOM will be available and store it
        if (!_isWaypointDynamic) {
            $container = $('.' + _waypointscrollcontainerClass, _windowContainer);
            _wayPointElementID = waypointElement;

            if(waypointElement) {
                _waypointDivElement = document.getElementById(waypointElement, $container);
            }else {
                _waypointDivElement = document.getElementById('waypoint', $container);
            }
            $waypointDiv = $(_waypointDivElement);
            showWayPoint();
        }        
    }

    // get the current page
    function getCurrentPage() {
        return lastRecord;
    }

    function getNumberOfRecords() {

        return _pagedData.length;
    }

    function updatePagedDataCount(noOfRows) {
        lastRecord = lastRecord + noOfRows;
        return lastRecord;
    }

    function resetPaginationParams() {
        _pagedData = [];
        lastRecord = 0;
        return lastRecord;
    }

    function updatePagedData(datas) {
        if (_pagedData.length == 0) {
            _pagedData = datas;
        } else {
            $.each(datas, function (index, value) {
                _pagedData.push(value);
            });
        }
        
        return _pagedData;
    }

    function deletePagedData(ids) {
        $.each(ids, function (idIndex, id) {
            $.each(_pagedData, function (pagedIndex, pageValue) {
                if (pageValue != undefined) {
                    if (id != undefined && pageValue[_uniqueColumnName] == id) {
                        _pagedData.splice(pagedIndex, 1);
                    }
                }
            });
        });
        return _pagedData;
    }

    function resolveDataDuplications(dataReceived) {
        var newdata=[];
        $.each(dataReceived, function(receivedIndex, receivedValue) {
            var hasDuplicated = false;
            $.each(_pagedData, function(pagedIndex, pageValue){
                if (receivedValue != undefined && pageValue[_uniqueColumnName] == receivedValue[_uniqueColumnName]){   
                    hasDuplicated = true;
                    return false;     
                }
              });          
              if(hasDuplicated == false) {
                  newdata.push(dataReceived[receivedIndex]);
              }               
        });
        return newdata;
    }


    function attachWaypoint(callback) {
        // if(_waypointDivElement == null || _waypointDivElement == undefined) {
        //     console.log("No way point element, cannot generate endless pagination.");
        //     return;
        // }

        dataCallback = callback;
        showWayPoint();
        //first call always without waypoint
        if (lastRecord == 0) {
            if(postRender) {
                callback(postRender);
            } 
        } else {
            setWayPoint();
        }
    }

    function reLoadPagedData() {
        resetPaginationParams();
        attachWaypoint(dataCallback);
    }

    function generateWaypoint() {
        //if window container not set try again (widget is creating on the fly, it's fully dynamic)
        if(_windowContainer.length == 0) {
            _windowContainer = $(_windowContainerID);
        }

        $container = $('.' + _waypointscrollcontainerClass, _windowContainer);     
        
        if($container.length > 1) {
            $container = $($container[$container.length - 1]);
        }

        if ($('#' + _wayPointElementID,  $container)) {     
            //make sure there is no duplication exists inside the window
            $('#' + _wayPointElementID, _windowContainer).each(function() { $(this).hide();});

            $container.append('<div id="'+ _wayPointElementID + '" class="waypointLoading">' +
                '<center><img src="../../images/loading.gif"/></center></div>');
            _waypointDivElement = document.getElementById(_wayPointElementID);
        } else {
            _waypointDivElement = document.getElementById(_wayPointElementID);
        }

        $waypointDiv = $(_waypointDivElement);
    }

    function setWayPoint() {
        if (_isWaypointDynamic) {
            generateWaypoint();
        };

        showWayPoint();

        //Destroying the previous waypoint, if any
        if(waypointInstance != undefined) {
            waypointInstance.destroy();
        }

        var wayPointScrollableContext = document.getElementsByClassName(_waypointscrollcontainerClass, $container);

        if(wayPointScrollableContext.length > 1) {
            wayPointScrollableContext = wayPointScrollableContext[wayPointScrollableContext.length - 1];
        }else
        {
            wayPointScrollableContext = wayPointScrollableContext[0];
        }

        if (!_waypointDivElement) {
            return false;
        } else {
           if(!document.getElementById(_wayPointElementID)) {
               return false;
           }
        }

        waypointInstance = new Waypoint({
            element: _waypointDivElement,
            handler: function (direction) {
                if (direction != 'down') {
                    return;
                }
                waypointInstance.destroy();
                if (dataCallback) {
                    //retain the scroll position for the dynamic waypoint
                    if (_isWaypointDynamic && $container[0]) {
                        _currentScrollTop = $container[0].scrollTop;
                    }
                    if (postRender) {
                        dataCallback(postRender);
                    }
                }
            },
            context: wayPointScrollableContext,
            offset: '98%'
        })
    }

    function renderNextPage() {
        if (postRender) {
            postRender(pageSize);
        }
    }

    function postRender(resultCount) {
        lastRecord = lastRecord + pageSize;

        if(resultCount != 0) {
            showWayPoint();
            attachWaypoint(dataCallback);
        } else {
            hideWayPoint();
        }

        if (_isWaypointDynamic && $container != undefined) {
            $container.scrollTop(_currentScrollTop);
        }
    }

    function showWayPoint() {
        if ($waypointDiv) {
            $waypointDiv.show();
        }
    }

    function hideWayPoint() {
        if ($waypointDiv) {
            $waypointDiv.hide();
        }
    }

    function getOptions(options) {
        if (!options) {
            options = {};
        }

        options.$SKIP = lastRecord;
        options.$LIMIT = pageSize;
        return options;
    }

    function getPagedData() {
        return _pagedData;
    }

    function setPagedData(data) {
        _pagedData = data;
    }

    function refreshWayPoint() {
        Waypoint.refreshAll();      
    }

    return {
        initialize,
        attachWaypoint,
        getOptions,
        getCurrentPage,
        updatePagedDataCount,
        resetPaginationParams,
        hideWayPoint,
        reLoadPagedData,
        updatePagedData,
        resolveDataDuplications,
        getPagedData,
        setPagedData,
        setWayPoint,
        deletePagedData,
        renderNextPage,
        getNumberOfRecords,
        refreshWayPoint
    };
}