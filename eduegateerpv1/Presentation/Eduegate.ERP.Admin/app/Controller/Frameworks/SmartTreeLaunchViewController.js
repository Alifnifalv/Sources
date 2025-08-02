app.controller('SmartTreeLaunchViewController', ['$scope', '$timeout', '$window', '$http', '$compile', '$sce', '$q', '$rootScope',
    function ($scope, $timeout, $window, $http, $compile, $sce, $q, $root) {
        console.log('SmartTreeLaunchViewController controller loaded.');

        $scope.SmartTree = [];
        $scope.SelectedSmartTree = null;

        $scope.init = function () {
           
        }

        $scope.Close = function () {
            if (!$scope.SelectedSmartTree) {
                return;
            }

            var datatab = $('[data-tab=smartViewTab_' + $scope.SelectedSmartTree.TreeID + '].smartViewInner');
            if (datatab) {
                $('[data-tab=smartViewTab_' + $scope.SelectedSmartTree.TreeID + '].smartViewInner').toggleClass('show')
            }

            $('.smartviewTab').removeClass('showQuickViewPanel').removeClass('pinned');
            $('ul.tabMenu li.tabmenuItem ').removeClass('pinned');
            var index = $scope.SmartTree.findIndex(x => x.TreeID === $scope.SelectedSmartTree.TreeID);
            $scope.SmartTree.splice(index, 1);  
            $scope.SelectedSmartTree = null;
        }

        $root.ShowTreeView = function (controller, event, view, title, parameters) {            
            var index = $scope.SmartTree.findIndex(x => x.TreeID === view);

            if (index < 0) {
                $scope.SmartTree.push({ TreeID: view, Title: title });
                $timeout(function () {
                    bringSmartViewByTreeID(view);
                    $root.SmartTree(controller, event, view, title, parameters);
                });
            } else {
                bringSmartViewByTreeID(view);
            }
        }

        $scope.SetTreeView = function (tree) {
            $scope.SelectedSmartTree = tree;
        }

        $scope.PinnedSmartView = function (event) {
            var datatab = $(".smartViewInner.show").attr('data-tab');
            $(event.target).closest(".smartviewTab").toggleClass('pinned');
            $("ul.tabMenu li.tabmenuItem[data-tab="+datatab+"]").toggleClass('pinned');
        }      

        function bringSmartViewByTreeID(treeId) {
            $('.smartviewTab').addClass('showQuickViewPanel');
            var tree = $scope.SmartTree.find(x => x.TreeID === treeId);
            $scope.SelectedSmartTree = tree;
            var datatabContent = $('[data-tab=smartViewTab_' + treeId + '].smartViewInner');
            if (datatabContent) {
                $('.smartViewInner').removeClass('show')
                $('[data-tab=smartViewTab_' + treeId + '].smartViewInner').toggleClass('show')
            }

            var datatab = $('[data-tab=smartViewTab_' + treeId + '].tabmenuItem');
            if (datatab) {
                $('.tabmenuItem').removeClass('selected')
                $('[data-tab=smartViewTab_' + treeId + '].tabmenuItem').toggleClass('selected')
            }
        }

        $root.SmartTree = function (view, event, windowName, title, menuParameters) {
            var createUrl;
            if (menuParameters !== null) {
                if (view.includes('?')) { createUrl = view + '&parameters=' + menuParameters } else { createUrl = view + '?parameters=' + menuParameters }
            } else { createUrl = view }

            $('[data-tab=smartViewTab_' + windowName + '].smartViewInner').append("<div id='" + windowName +
                "' class='windowcontainer active' style='display:block;color:white;'><center><div class='bounce-wrapper' id='Load'><div class='three-bounce-inner'><div class='bounce-item bi-item1'></div><div class='bounce-item bi-item2'></div><div class='bounce-item bi-item3'></div></div></div></center></div>")

            $.ajax({
                url: createUrl,
                type: 'GET',
                success: function (result) {
                    $('#' + windowName)
                        .replaceWith($compile(result)($scope)).updateValidation();
                    $timeout(function () {
                        $('#' + windowName).slideDown();
                    });
                },
                error: function (request, status, message, b) {
                    $().showGlobalMessage($root, $timeout, true, request.responseText)
                }
            })
        }
    }]);
