app.controller('FaceDetectionController', [
    '$scope', '$http', '$state', 'rootUrl', '$location', '$rootScope', '$stateParams', 
    'GetContext', '$sce', '$timeout', '$translate', 'SignalRService', '$element', '$q',
    function ($scope, $http, $state, rootUrl, $location, $rootScope, $stateParams, 
              GetContext, $sce, $timeout, $translate, SignalRService, $element, $q) {
    
      var context = GetContext.Context();
  
      $scope.modelsLoaded = false;
  
      $scope.init = async function () {
        const baseUri = window.localServerUrl.replace(/\/$/, '') + '/assets/models/';

        await faceapi.nets.tinyFaceDetector.loadFromUri(baseUri);
        await faceapi.nets.faceRecognitionNet.loadFromUri(baseUri);
        await faceapi.nets.faceLandmark68Net.loadFromUri(baseUri);
        
        console.log("Models loaded");
        $scope.modelsLoaded = true;

          // Start camera
        const video = document.getElementById('video');
        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
          navigator.mediaDevices.getUserMedia({ video: { facingMode: { exact: "environment" } } })
          .catch(() => navigator.mediaDevices.getUserMedia({ video: true }))
          .then(stream => {
            video.srcObject = stream;
            video.play();
          })
          .catch(err => {
            console.error("Camera access error: ", err);
            alert("Cannot access camera");
          });
        
        } else {
            alert("Camera not supported");
        }
        $scope.startLiveRecognition();

      };
  
      $scope.init();
  
        // Button handler to capture frame and compare
        $scope.captureAndCompare = async function () {
          const video = document.getElementById('video');
        
          const detection = await faceapi
            .detectSingleFace(video, new faceapi.TinyFaceDetectorOptions())
            .withFaceLandmarks()
            .withFaceDescriptor();
        
          if (!detection) {
            alert("No face detected from camera");
            return;
          }
        
          $scope.compareDescriptor(detection.descriptor);
        };


        $scope.compareDescriptor = async function(descriptor) {
          if (!$scope.modelsLoaded) {
            console.error("Face API models not loaded yet");
            return;
          }
        
          const knownDescriptor = await $scope.getKnownFaceDescriptor();
          const faceMatcher = new faceapi.FaceMatcher(knownDescriptor, 0.6);
          const result = faceMatcher.findBestMatch(descriptor);
        
          if (result.label === 'student1') {
            alert("✅ Student Matched!");
            // API call to mark IN
          } else {
            alert("❌ Face does not match any student");
          }
        };
                

      $scope.compareFace = async function(imageElement) {
        if (!$scope.modelsLoaded) {
          console.error("Face API models not loaded yet");
          return;
        }
  
        const detection = await faceapi.detectSingleFace(imageElement, new faceapi.TinyFaceDetectorOptions())
          .withFaceLandmarks()
          .withFaceDescriptor();
  
        if (!detection) {
          alert("No face detected");
          return;
        }
  
        const knownDescriptor = await $scope.getKnownFaceDescriptor(); // load from DB/local storage
        const faceMatcher = new faceapi.FaceMatcher(knownDescriptor, 0.6);
        const result = faceMatcher.findBestMatch(detection.descriptor);
  
        if (result.label === 'student1') {
          alert("✅ Student Matched!");
          // Send API call to mark as IN
        } else {
          alert("❌ Face does not match any student");
        }
      };
  
      // Dummy: you should load this from IndexedDB/server
      $scope.getKnownFaceDescriptor = async function() {
        const img = document.getElementById('knownFace');
      
        await new Promise(resolve => {
          if (img.complete) {
            resolve();
          } else {
            img.onload = resolve;
          }
        });
      
      // ✅ Explicitly use TinyFaceDetector
      const detection = await faceapi
        .detectSingleFace(img, new faceapi.TinyFaceDetectorOptions())
        .withFaceLandmarks()
        .withFaceDescriptor();

      
        return new faceapi.LabeledFaceDescriptors("student1", [detection.descriptor]);
      };


      $scope.startLiveRecognition = async function () {
        if (!$scope.modelsLoaded) {
          alert("Models not loaded yet");
          return;
        }
      
        const video = document.getElementById('video');
        const canvas = faceapi.createCanvasFromMedia(video);
        canvas.setAttribute('id', 'overlay');
        document.getElementById('videoWrapper')?.appendChild(canvas);
      
        const displaySize = { width: video.videoWidth, height: video.videoHeight };
        faceapi.matchDimensions(canvas, displaySize);
      
        const knownDescriptor = await $scope.getKnownFaceDescriptor();
        const faceMatcher = new faceapi.FaceMatcher(knownDescriptor, 0.6);
      
        // Prevent multiple intervals
        if ($scope.recognitionInterval) clearInterval($scope.recognitionInterval);
      
        $scope.recognitionInterval = setInterval(async () => {
          const detections = await faceapi
            .detectAllFaces(video, new faceapi.TinyFaceDetectorOptions())
            .withFaceLandmarks()
            .withFaceDescriptors();
      
          const resizedDetections = faceapi.resizeResults(detections, displaySize);
          canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height);
      
          resizedDetections.forEach(detection => {
            const bestMatch = faceMatcher.findBestMatch(detection.descriptor);
            const box = detection.detection.box;
            const drawBox = new faceapi.draw.DrawBox(box, { label: bestMatch.toString() });
            drawBox.draw(canvas);
      
            if (bestMatch.label === "student1" && !$scope.matchFound) {
              $scope.matchFound = true;
              alert("✅ Student Matched in Live Video!");
              // Call API or trigger logic to mark student as IN
            }
          });
        }, 300); // Every 300ms
      };
      
      
    }
  ]);
  