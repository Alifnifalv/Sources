app.controller("StudentTransferRequestController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {

    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });



    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

    //Function for LibraryTransaction View from view screen
    $scope.ViewTCApproval = function (StudentTransferRequestIID) {
        var windowName = 'TCApproval';
        var viewName = 'TCApproval';

        if ($scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName))
            return;

        $scope.AddWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        editUrl = 'Frameworks/CRUD/Create?screen=' + viewName + "&ID=" + StudentTransferRequestIID;

        $http({
            method: 'Get',
            url: editUrl
        }).then(function (result) {
            $('#Edit' + windowName, "#LayoutContentSection").replaceWith($compile(result.data)($scope)).updateValidation();
            $scope.ShowWindow("Edit" + windowName, "Edit " + viewName, "Edit" + windowName);
        }); 

        //$scope.GetStudentDetails(StudentTransferRequestIID);
    };

    $scope.ViewTransferCertificate = function (model) {

        var globalRightDrawer =
            `
  <div class="modal right" id="globalRightDrawer" tabindex="-1" aria-labelledby="rightModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg w-100">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Transfer Certificate</h5>
          <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close"></button>
        </div>
        <div>
        <iframe id="myFrame" src="Content/ReadContentsByIDWithoutAttachment?contentID=` + model.TCContentFileID + `" style="height: 720px; width: 1080px; frameborder="0"></iframe>            </div>
      </div>
    </div>
</div>`
            ;

        //var IID = StudentTransferRequestIID;

        //globalRightDrawer = globalRightDrawer.replace("{studentTransferRequestIID}", IID);

        $(globalRightDrawer).modal('show');
    };



    $scope.UploadTC = function (model) {

        showOverlay();
        var url = "Schools/School/GenerateTransferCertificate";
        $http({
            method: 'Post',
            url: url,
            data: model
        })
            .then(function (result) {
                if (result.IsError) {
                    $().showGlobalMessage($root, $timeout, true, result.Response);
                }
                else {
                    $().showGlobalMessage($root, $timeout, false, result.Response);
                }
                hideOverlay();
                return false;
            }, function () {
                hideOverlay();
            });
    };

    $scope.StudentChanges = function ($event, $element, crudModel) {
        showOverlay();
        var model = crudModel;

        var url = "Schools/School/GetStudentDetailFromStudentID?StudentID=" + model.Student.Key;
        $http({ method: 'Get', url: url })
            .then(function (result) {
                if (result == null || result.data.length == 0) {
                    $().showMessage($scope, $timeout, true, "There is no details available for this student");
                    $scope.FeeTypes = null;
                    hideOverlay();
                    return false;
                }

                $scope.CRUDModel.ViewModel.AdmissionNumber = result.data.AdmissionNumber;
                $scope.CRUDModel.ViewModel.Class = result.data.ClassName;
                $scope.CRUDModel.ViewModel.ClassID = result.data.ClassID;
                $scope.CRUDModel.ViewModel.Section = result.data.SectionName;
                $scope.CRUDModel.ViewModel.SectionID = result.data.SectionID;
                $scope.CRUDModel.ViewModel.AcademicYearID = result.data.AcademicYearID;
                $scope.CRUDModel.ViewModel.Academic = result.data.AcademicYear;
                $scope.CRUDModel.ViewModel.SchoolID = result.data.SchoolID;

            })
        hideOverlay();
    };

}]);