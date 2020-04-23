
Dropzone.autoDiscover = false;
var app = angular.module('MyApp1', ['thatisuday.dropzone']);

app.config(function (dropzoneOpsProvider) {
    dropzoneOpsProvider.setOptions({
        url: '/upload_1.php',
        acceptedFiles: 'All Files/*.*',
        addRemoveLinks: true,
        dictDefaultMessage: 'Click to add or drop file',
        dictRemoveFile: 'حذف فایل',
        dictResponseError: 'Could not upload this file'
    });
});

app.controller('MyCtrl1', function ($scope, $compile, $http) {

    EmpID = 0;

    //DropZone:
    $scope.showBtns = false;
    $scope.lastFile = null;
    $scope.dz_PersImageOptions = {
        url: '/Panel/Ticket_Adding/GetPersImageFile',
        dictDefaultMessage: 'لطفا فایل ضمیمه خود را اینجا رها کنید',
        acceptedFiles: '',
        parallelUploads: 1,
        autoProcessQueue: false,
        uploadMultiple: true,
        maxFiles: 5,
        maxFilesize: 1000.00,
        //filesize: 500.0,
        //dictFileTooBig:500.0,
        //maxThumbnailFilesize: '500',
        //createImageThumbnails: true,    
        //timeout: 3600000, /*milliseconds*/
        //Send Parameter to c#

        init: function () {
            this.on("sending", function (file, xhr, formData) {
                formData.append("EmpID", EmpID);
            });
            this.on("complete", function (file) {
                $scope.dz_PersImageMethods.removeAllFiles();
                //Refresh Page
            });
        }
    };

    $scope.dz_PersImageMethods = {};
    $scope.dzCallbacks = {
        'addedfile': function (file) {
            $scope.showBtns = true;
            $scope.lastFile = file;
        },
        'error': function (file, xhr) {

            alert("file:" + file);
            alert("xhr:" + xhr);
        }

    };

    

   


    $scope.FunctionSave = function () {
        if (confirm("آیامایلید پیام شما ارسال شود؟")) {

            var VarNewRec = {
                UserID: sessionStorage.UserID,
                Brand: $scope.Brand,
                Model: $scope.Model,
                Year: $scope.Year,
                State: sessionStorage.State,
                CallNumber: $scope.PhoneNumber,
                Type: sessionStorage.Type,
                Description: $scope.Desc,
                Price: $scope.Price,
                
                
            };

            $.ajax({
                type: "post",
                url: "/Panel/Ticket_Adding/Save",
                data: {
                    ClassAtt: VarNewRec
                },
                dataType: 'json',
                async: false,

                success: function (data) {
                    alert('آگهی مورد نظر با موفقیت اضافه شد !')
                    var files = $('#dropzone2').get(0).dropzone.getAcceptedFiles();
                    if (files.length > 0) {
                        EmpID = data;

                        $scope.dz_PersImageMethods.processQueue();
                    }

                },
                complete: function () {
                    var files = $('#dropzone2').get(0).dropzone.getAcceptedFiles();
                    if (files.length == 0) {
                        $scope.TxtExp = "";
                        $scope.ShowDetailsMessageOnChat($scope.CodePers_Reciver, $scope.SelNameLnameReciver, '', $scope.SelImageReciver);
                    }


                }
            });
        }
    }

    function Show() {
        var getData = $http.get("/Panel/Ticket_Adding/Show")
        getData.then(function (VarMessage) {
            $scope.ListAllTickets = VarMessage.data
        }, function () {
        });
    };


















});