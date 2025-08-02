app.controller("TicketController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    console.log("TicketController Loaded");

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    $scope.FillEmployees = function (model) {

        if (model.IsAutoCreation && model.OldAssignedEmployeeID) {
            return false;
        }

        showOverlay();

        $scope.LookUps.OldSupportAssignedEmployees = $scope.LookUps.SupportAssignedEmployees;

        var url = utility.myHost + "Home/GetEmployeesByDepartment?departmentID=" + model.Department;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                $scope.LookUps.SupportAssignedEmployees = result.data;

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }

    $scope.TicketReferenceTypeChanges = function (model) {
        var referenceTypeID = model.ReferenceType;
        if (!referenceTypeID) {
            return false;
        }
        showOverlay();
        model.Action = null;

        var url = utility.myHost + "Supports/Ticket/GetSupportActionsByReferenceTypeID?ticketReferenceTypeID=" + referenceTypeID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportActions = result.data;
                }
                else {
                    $().showMessage($scope, $timeout, true, "Something went wrong!");
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    }

    $scope.FacultyTypeChanges = function (viewModel) {

        $scope.IsError = false;
        $scope.ErrorMessage = "";

        var facultyTypeID = viewModel.FacultyType;
        if (!facultyTypeID) {
            return true;
        }

        var studentID = viewModel.Student != null ? viewModel.Student.Key : null;
        if (!studentID) {
            $scope.IsError = true;
            $scope.ErrorMessage = "Select a student!";
            return true;
        }

        var facultyType = $scope.LookUps.FacultyTypes.find(x => x.Key == facultyTypeID) != null ? $scope.LookUps.FacultyTypes.find(x => x.Key == facultyTypeID).Value : null;

        $scope.LookUps.SupportAssignedEmployees = [];

        if (facultyType.toLowerCase().includes("principal") && !facultyType.toLowerCase().includes("vice")) {
            $scope.FillPrincipal(viewModel);
        }
        else if (facultyType.toLowerCase().includes("vice") && facultyType.toLowerCase().includes("principal")) {
            $scope.FillVicePrincipal(viewModel);
        }
        else if (facultyType.toLowerCase().includes("head") && facultyType.toLowerCase().includes("mistress")) {
            $scope.FillHeadMistress();
        }
        else if (facultyType.toLowerCase().includes("class") && facultyType.toLowerCase().includes("teacher")) {
            $scope.FillClassTeachers(viewModel);
        }
        else if (facultyType.toLowerCase().includes("class") && facultyType.toLowerCase().includes("coordinator")) {
            $scope.FillClassCoordinator(viewModel);
        }
        else if (facultyType.toLowerCase().includes("associate") && facultyType.toLowerCase().includes("teacher")) {
            $scope.FillAssociateTeacher(viewModel);
        }
        else if (facultyType.toLowerCase().includes("other") && facultyType.toLowerCase().includes("teacher")) {
            $scope.FillOtherTeachers(viewModel);
        }
    };

    $scope.FillPrincipal = function (viewModel) {

        var url = utility.myHost + "Payroll/Employee/GetSchoolPrincipal?schoolID=" + viewModel.StudentSchoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees.push(result.data);

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillVicePrincipal = function (viewModel) {
        var url = utility.myHost + "Payroll/Employee/GetSchoolVicePrincipal?schoolID=" + viewModel.StudentSchoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees.push(result.data);

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillHeadMistress = function (viewModel) {

        var url = utility.myHost + "Payroll/Employee/GetSchoolHeadMistress?schoolID=" + viewModel.StudentSchoolID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees.push(result.data);

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillClassCoordinator = function (viewModel) {
        var classID = viewModel.StudentClassID;
        var sectionID = viewModel.StudentSectionID;
        var academicYearID = viewModel.StudentAcademicYearID;

        var url = utility.myHost + "Payroll/Employee/GetClassCoordinator?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees.push(result.data);

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillClassTeachers = function (viewModel) {
        var classID = viewModel.StudentClassID;
        var sectionID = viewModel.StudentSectionID;
        var academicYearID = viewModel.StudentAcademicYearID;

        var url = utility.myHost + "Payroll/Employee/GetClassTeachers?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees.push(result.data);

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillAssociateTeacher = function (viewModel) {
        var classID = viewModel.StudentClassID;
        var sectionID = viewModel.StudentSectionID;
        var academicYearID = viewModel.StudentAcademicYearID;

        var url = utility.myHost + "Payroll/Employee/GetClassAssociateTeachers?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees = result.data;

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.FillOtherTeachers = function (viewModel) {
        var classID = viewModel.StudentClassID;
        var sectionID = viewModel.StudentSectionID;
        var academicYearID = viewModel.StudentAcademicYearID;

        var url = utility.myHost + "Payroll/Employee/GetClassOtherTeachers?classID=" + classID + "&sectionID=" + sectionID + "&academicYearID=" + academicYearID;
        $http({ method: 'Get', url: url })
            .then(function (result) {

                if (result.data) {
                    $scope.LookUps.SupportAssignedEmployees = result.data;

                    $scope.EmployeesFilled();
                }

                hideOverlay();
            }, function () {
                hideOverlay();
            });
    };

    $scope.EmployeesFilled = function () {
        if ($scope.LookUps.SupportAssignedEmployees.length == 1) {
            viewModel.AssignedEmployee = $scope.LookUps.SupportAssignedEmployees[0];
        }
    }

    $scope.SupportCategoryChanges = function (model) {
        var categoryID = model.SupportCategory != null ? model.SupportCategory.Key : null;
        if (categoryID) {
            $scope.LookUps.SupportSubCategories = [];

            var url = utility.myHost + "Supports/Ticket/GetSupportSubCategoriesByCategoryID?supportCategoryID=" + categoryID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.LookUps.SupportSubCategories = result.data;

                    if ($scope.LookUps.SupportSubCategories.length == 1) {
                        model.SupportSubCategory = $scope.LookUps.SupportSubCategories[0];
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    };

    $scope.ParentChanges = function (model) {

        var parentID = model.Parent != null ? model.Parent.Key : null;
        if (parentID) {
            $scope.LookUps.Students = [];

            var url = utility.myHost + "Schools/School/GetActiveStudentsDetailsByParent?parentID=" + parentID;
            $http({ method: 'Get', url: url })
                .then(function (result) {

                    $scope.StudentsDetails = result.data;

                    if ($scope.StudentsDetails.length > 0) {
                        $scope.StudentsDetails.forEach(s => {
                            $scope.LookUps.Students.push({
                                "Key": s.StudentIID,
                                "Value": s.StudentFullName
                            });
                        });

                        if ($scope.LookUps.Students.length == 1) {
                            model.Student = $scope.LookUps.Students[0];
                        }
                    }

                    hideOverlay();
                }, function () {
                    hideOverlay();
                });
        }
    }

    $scope.StudentChanges = function (model) {
        var studentID = model.Student != null ? model.Student.Key : null;
        if (studentID) {
            var studentDet = $scope.StudentsDetails.find(s => s.StudentIID == studentID);
            if (studentDet) {
                model.StudentSchoolID = studentDet.SchoolID;
                model.StudentAcademicYearID = studentDet.AcademicYearID;
                model.StudentClassID = studentDet.ClassID;
                model.StudentSectionID = studentDet.SectionID;
            }
        }
    }

    $scope.TicketTypeChanges = function (model) {
        var ticketTypeID = model.TicketType;
        var ticketTypeKeyValue = $scope.LookUps.TicketTypes.find(t => t.Key == ticketTypeID);

        var ticketType = ticketTypeKeyValue.Value;

        //Clear selected values
        model.FacultyType = null;
        model.Department = null;
        model.AssignedEmployee = { "Key": null, "Value": null };

        if (ticketType) {
            if (ticketType.toLowerCase() == 'academic') {
                model.IsAcademicTicketType = true;
                model.IsNonAcademicTicketType = false;
                model.IsGeneralTicketType = false;
            }
            else if (ticketType.toLowerCase() == 'non academic') {
                model.IsAcademicTicketType = false;
                model.IsNonAcademicTicketType = true;
                model.IsGeneralTicketType = false;
            }
            else {
                model.IsAcademicTicketType = false;
                model.IsNonAcademicTicketType = false;
                model.IsGeneralTicketType = true;
            }
        }
    };

}]);