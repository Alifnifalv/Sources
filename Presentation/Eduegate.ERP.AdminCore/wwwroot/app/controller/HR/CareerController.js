app.controller("CareerController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $root) {
    angular.extend(this, $controller('ViewController', { $scope: $scope, $http: $http, $compile: $compile, $timeout: $timeout, $window: $window, $location: $location, $route: $route }));
    $controller('CRUDController', { $scope: $scope, $compile: $compile, $http: $http, $timeout: $timeout, $window: $window, $location: $location, $route: $route });

    $scope.ClearShortList = function ($event) {
        showOverlay();
        $scope.CRUDModel.ViewModel.ApplicantFilter = null;
        hideOverlay();
    };

    $scope.FilterShortList = function ($event, crudModel) {
        showOverlay();
        if (crudModel != undefined) {
            if ($scope.CRUDModel.ViewModel.ShortListLog == null || $scope.CRUDModel.ViewModel.ShortListLog == undefined
                || $scope.CRUDModel.ViewModel.ShortListLog.length <= 0)
            {
                $scope.CRUDModel.ViewModel.ShortListLog = $scope.CRUDModel.ViewModel.ShortList;
            }
            $scope.CRUDModel.ViewModel.ShortList = $scope.CRUDModel.ViewModel.ShortList.filter(x =>
                x.TotalYearOfExperience === Number(crudModel?.TotalYearOfExperience) ||
                (x.ApplicantName && x.ApplicantName.toLowerCase().includes(crudModel?.Name?.toLowerCase())) ||
                (x.Education && x.Education.toLowerCase().includes(crudModel?.Education?.toLowerCase())) 
            );
        }
        hideOverlay();
    };

    $scope.ReloadShortList = function ($event, crudModel) {
        showOverlay();
        $scope.CRUDModel.ViewModel.ShortList = $scope.CRUDModel.ViewModel.ShortListLog;
        hideOverlay();
    };

    $scope.CreateMeetingLink = function ($event, crudModel) {

        var interviewLink = 'https://www.google.com/calendar/render?action=TEMPLATE&text=Interview+for+{JobTitle}';

        if (crudModel.JobTitle == null || crudModel.JobTitle == undefined) {
            $().showGlobalMessage($root, $timeout, true, "Please select job tile !");
            return false;
        }

        // Replace the placeholders  
        var meetingCreateLink = interviewLink
            .replace('{JobTitle}', encodeURIComponent(crudModel.JobTitle))

        console.log(meetingCreateLink);
        window.open(meetingCreateLink, '_blank');

    };


    $scope.FilterSelectionList = function ($event, crudModel) {

        if (!$scope.ActualList || $scope.ActualList == undefined) {
            $scope.ActualList = $scope.CRUDModel.ViewModel.SelectionList || [];
        }

        var filterList = $scope.ActualList.filter(x => {
            const roundsFilter = crudModel.FilterBy?.FilterByRounds;
            const ratingsFilter = crudModel.FilterBy?.FilterByRatings;

            return (roundsFilter == null || Number(x.RoundsCompleted) === Number(roundsFilter)) &&
                (ratingsFilter == null || Number(x.TotalRatingGot) >= Number(ratingsFilter));
        });
 
        const filterByCount = Number(crudModel.FilterBy?.FilterByCount) || null;

        if (filterByCount) {
            crudModel.SelectionList = filterList.length > filterByCount
                ? filterList.slice(0, filterByCount)
                : filterList;
        }
        else {
            crudModel.SelectionList = filterList;
        }
    };

    $scope.HideShortListCheckBox = function ($event,$element, crudModel) {

        //Backup shortlist before filter
        if (!$scope.BackupList || $scope.BackupList == undefined) {
            $scope.BackupList = $scope.CRUDModel.ViewModel.ShortList || [];
        }

        if ($scope.BackupList == null || $scope.BackupList <= 0) {
            $().showGlobalMessage($root, $timeout, true, "No records found !");
            return false;
        }
        else {
            if (crudModel.HideShortListedData == true) {
                $scope.CRUDModel.ViewModel.ShortList = $scope.BackupList.filter(x => x.IsShortListed == false);
            }
            else {
                $scope.CRUDModel.ViewModel.ShortList = $scope.BackupList;
            }
        }

    }


    $scope.JDTitleChanges = function ($event, $element, model) {
        showOverlay();

        if (model.JDReference?.Key != null) {
            $.ajax({
                type: "GET",
                url: "HR/JobOpening/GetJobDescriptionByJDMasterID?JDMasterID=" + model.JDReference.Key,
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        $scope.CRUDModel.ViewModel.Department = result?.DepartmentID != null ? result?.DepartmentID.toString() : null;
                        $scope.CRUDModel.ViewModel.Designation = result?.DesignationID != null ? result?.DesignationID.toString() : null;
                        $scope.CRUDModel.ViewModel.JobDescription = result?.JDDetail && result?.JDDetail?.length > 0
                                                                    ? result?.JDDetail?.map(item => item.Description).join('\n')
                                                                    : null;
                        hideOverlay();
                    });
                    hideOverlay();
                }
            });
        }
        hideOverlay();
    }

    function showOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).attr('style', 'display:block');
    }

    function hideOverlay() {
        $('.preload-overlay', $($scope.CrudWindowContainer)).hide();
    }

}]);

