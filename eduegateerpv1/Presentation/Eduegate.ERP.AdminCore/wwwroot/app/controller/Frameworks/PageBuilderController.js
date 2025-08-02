app.controller('PageBuilderController', ['$scope', '$http', '$compile', '$window', '$timeout', '$location', '$route', '$controller', '$q',
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $q) {
        console.log('PageBuilderController')
        var editor;

        $scope.init = function () {
            $timeout(function () {
                editor = grapesjs.init({
                    container: '#gjs',
                    components: '',
                    storageManager: false,
                    style: '.txt-red{color: red}',
                    selectorManager: {
                        custom: true,
                    },
                    styleManager: {
                        sectors: []
                    }
                });

                $scope.GetAllBoilerplates().then(function (boilerplates) {
                    var bm = editor.Blocks;

                    boilerplates.forEach(boilerplate => {
                        if (boilerplate.BoilerPlateID) {
                            var template = boilerplate.Template;

                            editor.Components.addType('non-editable-block', {
                                model: {
                                    defaults: {
                                        traits: [], // Remove all traits to make it non-editable
                                        editable: false,
                                        draggable: false,
                                        removable: false,
                                        copyable: false,
                                    },
                                },
                            });

                            bm.add('non-editable-block' + boilerplate.BoilerPlateID, {
                                label: boilerplate.Name,
                                attributes: { class: 'non-editable-block' }, // Optionally, add a class to style it
                                media: `<svg fill="#000000" width="30px" height="30px" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M4 11h6a1 1 0 0 0 1-1V4a1 1 0 0 0-1-1H4a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm0 10h6a1 1 0 0 0 1-1v-6a1 1 0 0 0-1-1H4a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm10 0h6a1 1 0 0 0 1-1v-6a1 1 0 0 0-1-1h-6a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm7.293-14.707-3.586-3.586a.999.999 0 0 0-1.414 0l-3.586 3.586a.999.999 0 0 0 0 1.414l3.586 3.586a.999.999 0 0 0 1.414 0l3.586-3.586a.999.999 0 0 0 0-1.414z"/>
                                    </svg>`,
                                content: template,
                            });                           


                            //bm.add('BLOCK-ID' + boilerplate.BoilerPlateID, {
                            //    label: boilerplate.Name,
                            //    media: `<svg fill="#000000" width="30px" height="30px" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            //            <path d="M4 11h6a1 1 0 0 0 1-1V4a1 1 0 0 0-1-1H4a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm0 10h6a1 1 0 0 0 1-1v-6a1 1 0 0 0-1-1H4a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm10 0h6a1 1 0 0 0 1-1v-6a1 1 0 0 0-1-1h-6a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1zm7.293-14.707-3.586-3.586a.999.999 0 0 0-1.414 0l-3.586 3.586a.999.999 0 0 0 0 1.414l3.586 3.586a.999.999 0 0 0 1.414 0l3.586-3.586a.999.999 0 0 0 0-1.414z"/>
                            //        </svg>`,
                            //    content: template,
                            //    model: {
                            //        defaults: {
                            //            traits: [], // Remove all traits to make it non-editable
                            //            editable: false,
                            //        },
                            //    },
                            //});
                        }
                    });
                });
            });
        }

        $scope.GetAllBoilerplates = function () {
            return $q(function (resolve, reject) {
                $.ajax({
                    url: 'CMS/BoilerPlate/GetDesignerBoilerPlates',
                    type: 'GET',
                    success: function (result) {
                        resolve(result);
                    }
                });
            });
        }
    }]);
