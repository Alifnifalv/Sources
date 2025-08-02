app.controller("RecruitmentController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$rootScope",
    function ($scope, $http, $compile, $window, $timeout, $location, $route, $rootScope) {
        console.log("RecruitmentController Loaded");
        $scope.CurrencyCode = '';
        $scope.acceptTerms = false;

        $scope.jobApplicationDTO = {
            CountryOfResidence: '',
            TotalYearOfExperience: '',
            ReferenceCode: '',
            CVContentID: '',
            JobID: '',
        };

        $scope.Init = function (model, window) {
            getCurrecncyCode();

            if (window === "jobList") {
                $scope.GetAvailableJobList();
                GetDynamicLookups("Departments", "Department");
                GetDynamicLookups("Designations", "Designation");
                GetDynamicLookups("TypeOfJob", "TypeOfJob");
            }
            else if (window === "myProfile") {
                $scope.GetUserProfile();

                GetDynamicLookups("Country", "Countries");
                GetDynamicLookups("Nationality", "Nationality");
                GetDynamicLookups("Gender", "Gender");
                GetDynamicLookups("BloodGroups", "BloodGroup");
                GetDynamicLookups("Qualifications", "Qualifications");
                GetDynamicLookups("AvailableJobSkills", "AvailableJobSkills");
            }
            else if (window === "appliedJobs") {
                $scope.GetAppliedJobList();
            }
            else if (window === "JobApply") {
                GetDynamicLookups("Country", "Countries");
            }
            else if (window === "JobDetails") {
                $scope.GetAvailableJobList();
            }
            else if (window === "interviewList") {
                $scope.GetMyInterviewList();
            }
            else if (window === "jdList") {
                $scope.GetJDListByLoginID();
            }
        };

        $scope.closeModal = function () {
            $('#jdModal').modal('hide');
        };

        $scope.openModal = function (index) {
            $scope.CurrentJDindex = index;
            $('#jdModal').modal('show');
        };

        $scope.Filter = function (filters) {

            $scope.filteredJobList = $scope.filteredJobList.filter(x =>
                (filters.Department != null ? String(x.DepartmentID) === filters.Department.Key : true) &&
                (filters.Designation != null ? String(x.DesignationID) === filters.Designation.Key : true)
            );  

            $scope.updateRows();
        }

        $scope.ClearFilter = function (filters) {
            filters.Department = null;
            filters.Designation = null;
            $scope.searchText = '';
            $scope.searchSchool = '';
            $scope.searchJobType = '';

            $scope.GetAvailableJobList();
        }

        function getCurrecncyCode()
        {
            GetSettingValue("DEFAULT_CURRENCY_CODE", "CurrencyCode").then(function ()
            {
                const style = document.createElement('style');
                style.innerHTML = `  
            .currSign:before {  
                content: '${$scope.CurrencyCode}'; /* Set new content */  
            }  
        `;
                document.head.appendChild(style);
            });
        };

        $scope.updateRows = function () {
            $scope.jobList = [];
            for (let i = 0; i < $scope.filteredJobList.length; i += 3) {
                $scope.jobList.push($scope.filteredJobList.slice(i, i + 3));
            }
        };


        $scope.filteredJobList = []; 
        $scope.searchText = '';
        $scope.searchSchool = '';
        $scope.searchJobType = '';

        // Pagination variables  
        $scope.currentPage = 1;
        $scope.itemsPerPage = 5; // Change this to the number of items you want per page  

        $scope.setPage = function (page) {
            $scope.currentPage = page;
        };

        // Watch for changes in filters and update the filtered list  
        $scope.$watchGroup(['searchText', 'searchSchool', 'searchJobType'], function () {
            //$scope.setPage(1);
            if ($scope.AvailableJobList != undefined) {
                $scope.filteredJobList = $scope.AvailableJobList.filter(job => {
                    return (!$scope.searchText != "" || job.JobTitle.toLowerCase().includes($scope.searchText.toLowerCase())) &&
                        (!$scope.searchSchool != "" || job.School.toLowerCase().includes($scope.searchSchool.toLowerCase())) &&
                        (!$scope.searchJobType != "" || job.TypeOfJob.toLowerCase().includes($scope.searchJobType.toLowerCase()));
                });
                $scope.updateRows();
                //$scope.totalPages = Math.ceil($scope.filteredJobList.length / $scope.itemsPerPage);
            }
        }); 

        $scope.GetAvailableJobList = function () {
            showSpinner()
            $.ajax({
                type: "GET",
                url: "/Home/GetAvailableJobList",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.AvailableJobList = result.Response;
                            $scope.filteredJobList = result.Response;
                            //$scope.totalPages = Math.ceil($scope.filteredJobList.length / $scope.itemsPerPage);
                            $scope.updateRows();  
                            hideSpinner()
                        } else {
                            $scope.AvailableJobList = null;
                            hideSpinner()
                        }
                    });
                }
            });
        }

        $scope.GetAppliedJobList = function () {
            showSpinner()
            $.ajax({
                type: "GET",
                url: "/Home/GetAppliedJobList",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.AppliedJobList = result.Response;
                            hideSpinner()
                        } else {
                            $scope.AppliedJobList = null;
                            hideSpinner()
                        }
                    });
                }
            });
        }

        $scope.GetMyInterviewList = function () {
            showSpinner()
            $.ajax({
                type: "GET",
                url: "/Home/GetMyInterviewList",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.MyInterviewList = result.Response;
                            hideSpinner()
                        } else {
                            $scope.MyIntervieList = null;
                            hideSpinner()
                        }
                    });
                }
            });
        }

        $scope.JobDetails = function (JobIID) { 
            window.location = '/Home/JobDetails?jobIID=' + JobIID;
        }

        $scope.JobApply = function (jobIID) { 
            window.location = '/Home/JobApply?jobIID=' + jobIID;
        }

        $scope.ConfirmAndApplyJob = function (jobApplicationDTO, jobID) {
            jobApplicationDTO.JobID = jobID;

            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;
            showSpinner();

            document.body.classList.add('disabled');

            $.ajax({
                url: utility.myHost + "Home/ApplyForJob",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(jobApplicationDTO),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        setTimeout(function () {
                            hideSpinner();
                            document.body.classList.remove('disabled');
                            submitBtn.disabled = false;
                        }, 1000);
                    } else {
                        toastr.error(result.ReturnMessage);
                        submitBtn.disabled = false;
                        document.body.classList.remove('disabled');
                        hideSpinner();
                    }
                }
            });
        }

        function GetDynamicLookups(scopeTo, lookUpType) {
            $scope[scopeTo] = [];
            $.ajax({
                type: 'GET',
                url: utility.myHost + "Mutual/GetDynamicLookUpData?lookType=" + lookUpType +"&defaultBlank=false",
                success: function (result) {
                    $scope[scopeTo] = result.filter(item => item.Key !== '' && item.Key !== null && item.Key !== '0');  
                    $timeout(function () {

                    }, 1000);
                }
            });
        }

        $scope.updateProfile = function (ProfileDetails) {

            if (ProfileDetails.ConfirmPassword && (ProfileDetails.ConfirmPassword !== ProfileDetails.Password)) {
                toastr.error("Passwords do not match. Please try again.");
                return false;
            }

            if (!ProfileDetails.Gender.Key) {
                toastr.error("Gender field is required");
                return false;
            }
            else if (!ProfileDetails.BloodGroup.Key) {
                toastr.error("Blood group is required");
                return false;
            }
            else if (!ProfileDetails.Nationality.Key) {
                toastr.error("Nationality is required");
                return false;
            }
            else if (!ProfileDetails.PassportIssueCountry.Key) {
                toastr.error("Passport Issued Country is required");
                return false;
            }
            else if (!ProfileDetails.Qualification.Key) {
                toastr.error("Qualification is required");
                return false;
            }

            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;

            document.body.classList.add('disabled');
            showSpinner();

            $.ajax({
                url: utility.myHost + "Home/UpdateUserProfile",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(ProfileDetails),
                success: function (result) {
                    if (result.IsError === false) {
                        toastr.success(result.ReturnMessage);
                        setTimeout(function () {
                            hideSpinner();
                            document.body.classList.remove('disabled');
                            submitBtn.disabled = false;
                        }, 1000);
                    } else {
                        toastr.error(result.ReturnMessage);
                        submitBtn.disabled = false;
                        document.body.classList.remove('disabled');
                        hideSpinner();
                    }
                }
            });
        };

        // Get the checkbox element  
        const switchCheckbox = document.getElementById('flexSwitchCheckChecked');
        // Add an event listener to the checkbox  
        switchCheckbox?.addEventListener('change', handleSwitchChange);

        function handleSwitchChange() {
            if (switchCheckbox.checked) {
                // Call GetUserProfile and chain the filterJobListWithProfile  
                $scope.GetUserProfile()
                    .then($scope.filterJobListWithProfile)
                    .catch(function (error) {
                        console.error("Error during profile retrieval or filtering:", error);
                    });
            } else {
                $scope.GetAvailableJobList();
            }
        }

        $scope.filterJobListWithProfile = function () {
            var filteredList = $scope.filteredJobList.filter(x =>
                x.AvailableJobCriteriaMapDTO.some(criteria => criteria.Qualification?.Key === $scope.ProfileDetails.Qualification?.Key)
            );
            $scope.$apply(function () {
                if (filteredList) {
                    $scope.filteredJobList = filteredList;
                    $scope.updateRows();
                    $scope.setPage(1);
                    $scope.totalPages = Math.ceil($scope.filteredJobList.length / $scope.itemsPerPage);
                }
            });
        }

        $scope.GetUserProfile = function () {
            showSpinner();
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: "GET",
                    url: "/Home/GetProfileDetails",
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        $scope.$apply(function () {
                            if (result) {
                                $scope.ProfileDetails = result.Response;
                                resolve(); // Resolve the promise when successful  
                            } else {
                                $scope.ProfileDetails = null;
                                resolve(); // Resolve even if there's no data to continue the flow  
                            }
                        });
                        hideSpinner();
                    },
                    error: function (error) {
                        console.error("Error retrieving user profile:", error);
                        hideSpinner();
                        reject(error); // Reject the promise in case of error  
                    }
                });
            });
        }


        $scope.UploadFile = function (file,toDto,field) {
            if (file) {
                var formData = new FormData();
                formData.append("file", file);

                var xhr = new XMLHttpRequest();
                xhr.open("POST", utility.myHost + "Content/UploadContents", true);

                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4) {
                        var response = JSON.parse(xhr.responseText);
                        if (response.Success) {
                            $scope.$apply(function () {
                                $scope[toDto][field] = response.FileInfo[0].ContentFileIID;
                            });
                        } else {
                            toastr.error("Failed to upload file. Status: " + xhr.status);
                        }
                    }
                };

                // Send the file data  
                xhr.send(formData);
            } else {
                toastr.error("No file selected.");
            }
        };

        function GetSettingValue(settingCode,scopeTo) {
            // Return a promise  
            return $http({
                method: 'GET',
                url: utility.myHost + "Mutual/GetSettingValueByKey?settingKey=" + settingCode,
            }).then(function (result) {
                if (result.data) {
                    $scope[scopeTo] = result.data;
                } else {
                    $scope[scopeTo] = null;
                }
            });
        }

        $scope.calculateAge = function () {
            if ($scope.ProfileDetails.DateOfBirthString) {
                // Split the DateOfBirthString to get day, month, year  
                const parts = $scope.ProfileDetails.DateOfBirthString.split("/");
                if (parts.length === 3) {
                    const day = parseInt(parts[0], 10);
                    const month = parseInt(parts[1], 10) - 1; // month is 0-indexed  
                    const year = parseInt(parts[2], 10);

                    // Create a new Date object using the parsed values  
                    const dob = new Date(year, month, day);

                    // Calculate age  
                    const now = new Date();
                    let age = now.getFullYear() - dob.getFullYear();
                    const monthDiff = now.getMonth() - dob.getMonth();

                    // Adjust age if the birthday hasn't occurred yet this year  
                    if (monthDiff < 0 || (monthDiff === 0 && now.getDate() < dob.getDate())) {
                        age--;
                    }

                    // Set the calculated age  
                    $scope.ProfileDetails.Age = age;

                    if ($scope.ProfileDetails.Age < 18) {
                        toastr.error("Warning: You must be at least 18 years old.");
                        $scope.ProfileDetails.Age = '';
                        $scope.ProfileDetails.DateOfBirthString = null;
                        return false;
                    }

                } else {
                    $scope.ProfileDetails.Age = ''; // Clear age if the format is incorrect  
                }
            } else {
                $scope.ProfileDetails.Age = ''; // Clear age if no DOB is provided  
            }
        }; 


        $scope.GetJDListByLoginID = function () {
            showSpinner()
            $.ajax({
                type: "GET",
                url: "/Home/GetJDListByLoginID",
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $scope.$apply(function () {
                        if (result) {
                            $scope.MyJDList = result.Response;
                            hideSpinner()
                        } else {
                            $scope.MyJDList = null;
                            hideSpinner()
                        }
                    });
                }
            });
        }

        $scope.confrimJobDescription = function (iid, acceptTerms) {
            if (acceptTerms != true) {
                toastr.error("Please tick 'I accept the terms and conditions' and confirm.");
                return false;
            }
            showSpinner();

            $.ajax({
                url: utility.myHost + "Home/MarkJDasAgreed?iid=" + iid,
                type: "POST",
                contentType: "application/json",
                success: function (result) {
                    $scope.$apply(function () {
                        hideSpinner();
                        $scope.closeModal();
                        $scope.GetJDListByLoginID();
                        if (result.IsError === false) {
                            toastr.success(result.UserMessage);
                        } else {
                            toastr.error(result.UserMessage);
                        }
                    });
                }
            });
            hideSpinner();
        }

        function showSpinner() {
            document.getElementById('spinner').style.display = 'block';
        }

        function hideSpinner() {
            document.getElementById('spinner').style.display = 'none';
        } 

    }
]);
