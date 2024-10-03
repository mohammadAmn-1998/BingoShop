$(document).ready(function () {
   
    loadCkeditor4();
});

function makeSlug(source, destination) {

    var titleStr = $('#' + source).val();
    titleStr = titleStr.replace(/^\s+|\s+$/g, '');
    titleStr = titleStr.toLowerCase();
    titleStr = titleStr.replace(/[^a-z0-9_\s-ءاأإآؤئبتثجحخدذرزسشصضطظعغفقكلمنهويةى]#u/, '')
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-');
    $('#' + destination).val(titleStr);
}



function loadAnswerMessageCKEditor() {

    var status = document.getElementById("message_status").value;
    console.log(status);
    if (status == 'پاسخ_داده_شد_email')
    {
        console.log('answer by email');
        document.getElementById("answer_email").classList.remove(`invisible`);
        document.getElementById("answer_sms").classList.add(`invisible`);
        loadCkeditor4();
        console.log('answer by email');
        console.log('input founded!');

        
        return;
    }
    if (status == 'پاسخ_داده_شد_sms')
    {
        console.log('answer by sms');
        document.getElementById("answer_sms").classList.remove(`invisible`);
        document.getElementById("answer_email").classList.add(`invisible`);
        unloadCkEditor('answer_email');
        console.log('answer by sms');
        return;
    }
    if (status == 'پاسخ_داده_شد') {
        console.log('answer by call');
        document.getElementById("answer_sms").classList.add(`invisible`);
        document.getElementById("answer_email").classList.add(`invisible`);
        unloadCkEditor('answer_email');
        console.log('answer by call');
        return;
    }

}

function loadCkeditor4() {

    $("body").append('<script src="/ckeditor4/ckeditor/ckeditor.js"></script>');

    if (document.getElementById("CkEditor4")) {
        CKEDITOR.replace('CkEditor4', {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
    }
    if (document.getElementById("answer_email")) {
        
        CKEDITOR.replace('answer_email', {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
    }
    

   

}

function unloadCkEditor(id) {

    if (CKEDITOR.instances[id]) {
        CKEDITOR.instances[id].destroy(true);
    }

}

//event listener for input type file for preventing choosing any file except image file before user submit the form
document.getElementById('file_input').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (validateImageFile(file)) {
        if (file && file.type.startsWith('image/')) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const img = document.getElementById('img_preview');
                img.src = e.target.result;
                img.style.display = 'block';
            };
            reader.readAsDataURL(file);
        }
    }

});


function validateImageFile(file) {
    const allowedImageTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/bmp', 'image/webp'];
    if (file && allowedImageTypes.includes(file.type)) {
        return true;
    } else {
        ErrorAlert('تصویر نامعتبر!', 'لطفا تصویر معتبر که پسوند های jpeg,png,bmp,webp داشته باشد انتخاب کنید');
        $("#file_input").val('');
        return false;
    }
}

function changeActivation(url, errorTitle, errorText) {
    if (errorTitle == null || errorTitle == "undefined") {
        errorTitle = "عملیات ناموفق";
    }
    if (errorText == null || errorText == "undefined") {
        errorText = "";
    }
    Swal.fire({
        title: "هشدار !!",
        text: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "warning",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "get",
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                error: function (data) {
                    ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                }
            }).done(function (result) {
                {
                    if (result.success) {
                        Success(result.title, result.message);
                    } else {
                        ErrorAlert(result.title, result.message);
                    }

                    setTimeout(function (parameters) {
                        location.reload();
                    },
                        2000);
                }
            });


        }
    });
}

function changeBan(url, errorTitle, errorText) {
    if (errorTitle == null || errorTitle == "undefined") {
        errorTitle = "عملیات ناموفق";
    }
    if (errorText == null || errorText == "undefined") {
        errorText = "";
    }
    Swal.fire({
        title: "هشدار !!",
        text: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "warning",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "get",
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                error: function (data) {
                    ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                }
            }).done(function (result) {
                {
                    if (result.success) {
                        Success(result.title, result.message);
                    } else {
                        ErrorAlert(result.title, result.message);
                    }

                    setTimeout(function (parameters) {
                        location.reload();
                    },
                        2000);
                }
            });


        }
    });
}

function ajaxSweetAlertRefresh(title,text,confirmText,cancelText,url) {

    if (title == null || title == "undefined") {
        title = "آیا مطمعنید؟";
    }
    if (text == null || text == "undefined") {
        text = "";
    }

    if (confirmText == null || confirmText == "undefined") {
        confirmText = "انجام شود؟";
    }

    if (cancelText == null || cancelText == "undefined") {
        cancelText = " همینطور باقی بماند؟";
    }

    Swal.fire({
        title: title,
        text: text,
        icon: "warning",
        confirmButtonText: confirmText,
        showCancelButton: true,
        cancelButtonText: cancelText,
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "get",
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                error: function (data) {
                    ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                }
            }).done(function(ok) {

               if (ok) {

                   Success('انجام شد!', '', true);
                   

               } else {
                   ErrorAlert('انجام نشد!', 'مشکلی در عملیات رخ داده لطفا در زمان دیگری امتحان کنید!', true);
               }

            });


        }
    });

}