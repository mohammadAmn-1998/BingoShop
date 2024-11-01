﻿$(document).ready(function() {

    var result = getCookie("SystemAlert");
    if (result) {
        result = JSON.parse(result);
        if (result.Status === 200) {
            Success(result.Title, result.Message, result.isReloadPage);
        } else if (result.Status === 600) {
            ErrorAlert(result.Title, result.Message);
        } else if (result.Status === 404) {
            NotFound(result.Title, result.Message);
        } else if (result.Status === 10) {
            Info(result.Title, result.Message);
        }
        deleteCookie("SystemAlert");
    }

    loadCkeditor4();
});

function loadCkeditor4() {
    if (!document.getElementById("CkEditor4"))
        return;

    $("body").append('<script src="/ckeditor4/ckeditor/ckeditor.js"></script>');

    CKEDITOR.replace('CkEditor4', {
        customConfig: '/ckeditor4/ckeditor/config.js'
    });

}

function getCookie(cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return decodeURIComponent(c.substring(name.length, c.length));
            }
        }
        return "";
    }
function deleteCookie(cookieName) {
        document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970;path=/`;
    }

//sweetAlert2 functions
    function Success(Title, description, isReload = false) {
        if (Title == null || Title == "undefined") {
            Title = "!انجام شد";
        }
        if (description == null || description == "undefined") {
            description = "";
        }
        Swal.fire({
            title: Title,
            text: description,
            icon: "success",
            confirmButtonText: "باشه",
        }).then((result) => {
            if (isReload === true) {
                location.reload();
            }
        });
    }

    function Info(Title, description) {
        if (Title == null || Title == "undefined") {
            Title = "توجه";
        }
        if (description == null || description == "undefined") {
            description = "";
        }
        Swal.fire({
            title: Title,
            text: description,
            icon: "info",
            confirmButtonText: "باشه"
        });
    }

    function NotFound(Title, description) {

        if (Title == null || Title == "undefined") {
            Title = "پیدا نشد!";
        }
        if (description == null || description == "undefined") {
            description = "";
        }
        Swal.fire({
            title: Title,
            text: description,
            icon: "info",
            confirmButtonText: "باشه"
        });

    }
    function ErrorAlert(Title, description, isReload = false) {
        if (Title == null || Title == "undefined") {
            Title = "مشکلی در عملیات رخ داده است";
        }
        if (description == null || description == "undefined") {
            description = "";
        }
        Swal.fire({
            title: Title,
            text: description,
            icon: "error",
            confirmButtonText: "باشه"
        }).then((result) => {
            if (isReload === true) {
                location.reload();
            }
        });
    }

    function Warning(Title, description, isReload = false) {
        if (Title == null || Title == "undefined") {
            Title = "مشکلی در عملیات رخ داده است";
        }
        if (description == null || description == "undefined") {
            description = "";
        }
        Swal.fire({
            title: Title,
            text: description,
            icon: "warning",
            confirmButtonText: "باشه"
        }).then((result) => {
            if (isReload === true) {
                location.reload();
            }
        });
    }

    function deleteItem(url, errorTitle, errorText) {
        if (errorTitle == null || errorTitle == "undefined") {
            errorTitle = "عملیات ناموفق";
        }
        if (errorText == null || errorText == "undefined") {
            errorText = "";
        }
        Swal.fire({
            title: "هشدار !!",
            text: "آیا از حذف اطمینان دارید ؟",
            icon: "warning",
            confirmButtonText: "بله",
            showCancelButton: true,
            cancelButtonText: "خیر",
            preConfirm: () => {
                $.ajax({
                    url: url,
                    type: "get",
                    beforeSend: function() {
                        $(".loading").show();
                    },
                    complete: function() {
                        $(".loading").hide();
                    },
                    error: function(data) {
                        ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                    }
                }).done(function(data) {
                    data = JSON.parse(data);
                    if (data.Status === 200) {
                        Swal.fire({
                            title: data.Title,
                            text: data.Message == null ? "اعملیات با موفقیت انجام شد" : data.Message,
                            icon: "success",
                            confirmButtonText: "باشه",
                        }).then(function(res) {
                            if (data.IsReloadPage === true) {
                                location.reload();
                            }
                        });
                    } else if (data.Status === 10) {
                        ErrorAlert(data.Title, data.Message);
                    } else if (data.Status === 404) {
                        Warning(data.Title, data.Message);
                    }
                });
            }
        });
    }

    function Question(url, QuestionTitle, QuestionText, successText, callBack) {
        if (QuestionTitle == null || QuestionTitle == "undefined") {
            QuestionTitle = "آیا از انجام عملیات اطمینان دارید؟";
        }
        if (QuestionText == null || QuestionText == "undefined") {
            QuestionText = "";
        }

        Swal.fire({
            title: QuestionTitle,
            text: QuestionText,
            icon: "question",
            confirmButtonText: "بله",
            showCancelButton: true,
            cancelButtonText: "خیر",
            preConfirm: () => {
                $.ajax({
                    url: url,
                    type: "get",
                    beforeSend: function() {
                        $(".loading").show();
                    },
                    complete: function() {
                        $(".loading").hide();
                    },
                    error: function(data) {
                        ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                    }
                }).done(function(data) {
                    try {
                        data = JSON.parse(data);
                        if (data.Status === 200) {
                            Swal.fire({
                                title: data.Title,
                                text: successText == null ? data.Message : successText,
                                icon: "success",
                                confirmButtonText: "باشه",
                            }).then(function(res) {
                                if (data.IsReloadPage === true) {
                                    location.reload();
                                } else {
                                    if (callBack) {
                                        callBack(data.Status);
                                    }
                                }
                            });
                        } else if (data.Status === 10) {
                            ErrorAlert(data.Title, data.Message);
                        } else if (data.Status === 404) {
                            Warning(data.Title, data.Message);
                        }
                    } catch (ex) {
                        ErrorAlert();
                    }
                });


            }
        });
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
                    if (response.success) {
                        Success(response.title,response.message);
                    } else {
                        ErrorAlert( response.title,response.message);
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
function sweetAlertConfirmSubmit(event) {

    event.preventDefault();

    Swal.fire({
        title: "هشدار !!",
        text: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "warning",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
    }).then((willConfirm) => {
        if (willConfirm.value) {
            console.log("hello");
            event.target.submit();
        } else {
            return;
        }
    });
}
function sweetAlertConfirmLink(event, text) {

    
    event.preventDefault();
    if (text == "")
        text = "آیا از انجام عملیات اطمینان دارید ؟";
    Swal.fire({
        title: "هشدار !!",
        text: text,
        icon: "warning",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
    }).then((willConfirm) => {
        if (willConfirm.value) {
            window.location.href = event.target.href;
        } else {
            return;
        }
    });
}


//success
//question
//info
//warning
//error
function AlertSweet(title, message, icon) {
    Swal.fire(title, message, icon)
}
function AlertSweetTimer(title, icon, time) {
    Swal.fire({
        position: 'top-end',
        icon: icon,
        title: title,
        showConfirmButton: true,
        confirmButtonText: 'باشه',
        timerProgressBar: true,
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger",
           

        },
        timer: time,
       
    })
}



function AddEmailUser() {

    var mail = $("input#inputEmailUser").val();
    var emailValidate = $("span#inputEmailUserValid");

    if (mail === null || mail === "" || ValidateEmail(mail) === false) {

        emailValidate.text('لطفا ایمیل معتبر وارد کنید!');
        AlertSweetTimer("لطفا ایمیل معتبر وارد کنید", 'error', 3000);
    } else {
        Loading();
        $.ajax({
            
            url: "/Home/AddUserEmail",
            type: 'Post',
            data: { email: mail },
            beforeSend: function () {
                $(".loading").show();
            },
            complete: function () {
                $(".loading").hide();
            },
            error: function (data) {
                ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
            }

        }).done(function(result) {
            EndLoading();
            if (result.ok ) {
                AlertSweetTimer("شما عضو خبرنامه شدید!", 'success', 3000);
                emailValidate.text('');
                mail.val('');
            } else {
                AlertSweetTimer(result.message, 'error', 3000);
                emailValidate.text(result.message);

            }
           
        });

    }

    function ValidateEmail(email) {
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailRegex.test(email);
    }


    function Loading() {
        $(".loading").show();
    }
    function EndLoading() {
        $(".loading").hide();
    }

  
}
