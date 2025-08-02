app.controller('SmartTreeViewController', ['$scope', '$timeout', '$window', '$http', '$compile', '$sce', '$q',
    function ($scope, $timeout, $window, $http, $compile, $sce, $q) {
        console.log('SmartTreeViewController controller loaded.')

        $scope.runTimeParameter = null
        $scope.LazyLoadBoilerPlateNos = 0
        $scope.GetBoilerPlatesURL = 'Boilerplate/GetBoilerPlates'
        $scope.Model = null
        $scope.window = ''
        $scope.windowname = ''
        $scope.Nodes = []
        $scope.ViewType = null
        $scope.SearchText = '';

        $scope.init = function (model, window, windowname, viewType) {
            $scope.runTimeParameter = model.parameter;
            $scope.Model = model;
            $scope.window = window;
            $scope.ViewType = viewType;
            $scope.windowname = windowname;
            $scope.LoadTreeView();
        }

        $scope.FireEvent = function ($event, childnode) {
            $($event.target).closest("span").toggleClass("expand");

            if (childnode.Nodes.length > 0) {
                if ($($event.target).closest("span").hasClass('expand')) {
                    $($event.target).closest('li').find('ul:first').slideDown('fast');
                }
                else {
                    $($event.target).closest('li').find('ul:first').slideUp('fast');
                }
                return;
            }

            var promise = loadTreeNodes(childnode.NodeID);
            promise.then(function (result) {
                $timeout(function () {
                    $scope.$apply(function () {
                        childnode.Nodes = result.Nodes;

                        if (childnode.Nodes.length === 0) {
                            $($event.target).closest('li').addClass('leaf');
                        }
                        else {
                            $($event.target).closest('li').removeClass('leaf');
                        }

                        $timeout(function () {
                            if ($($event.target).closest("span").hasClass('expand')) {
                                $($event.target).closest('li').find('ul:first').slideDown('fast');
                            }
                            else {
                                $($event.target).closest('li').find('ul:first').slideUp('fast');
                            }
                        });
                    });
                });
            });
        };

        $scope.LoadTreeView = function (parentNodeId) {
            var promise = loadTreeNodes();
            promise.then(function (result) {
                $scope.Nodes = result.Nodes;
                $('#shoLoad').hide();
            });
        }

        function loadTreeNodes(parentNodeId) {
            return $q(function (resolve, reject) {
                var uri = 'Frameworks/SmartTreeView/GetSmartTreeView?type='
                    + $scope.ViewType + "&searchText=" + $scope.SearchText;

                if (parentNodeId) {
                    uri = uri + '&parentID=' + parentNodeId;
                }

                $.ajax({
                    type: 'GET',
                    url: uri,
                    success: function (result) {
                        resolve(result);
                    }
                });
            });
        }

        $scope.LoadBoilerPlates = function () {
            $timeout(function () {
                $.each($scope.Model.ViewModel.Boilerplates, function (index) {
                    var width = new JSLINQ(this.RuntimeParameters).Where(function (data) { return data.Key == 'Width' }).items[0]

                    if (width == null || width == undefined) {
                        width = {}
                        width.Value = '100'
                    }

                    $('#' + $scope.windowname + ' .main-page').append($compile("<div id='BoilerPlate" + this.BoilerplateMapIID + "' class='col-xl-" + width.Value + "'></div>")($scope))
                    $.ajax({
                        type: 'POST',
                        url: 'Boilerplate/Template',
                        data: this,
                        success: function (result) {
                            $('#' + $scope.windowname + ' .main-page #BoilerPlate' + result.BoilerplateMapIID).append($compile(result.Content)($scope))
                        }
                    })
                })
            })
        }

    }]);