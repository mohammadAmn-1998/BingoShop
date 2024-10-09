
//success
//question
//info
//warning
//error



    function Loding() {
        $(".loading").fadeIn();
    }
    function EndLoading() {
        $(".loading").fadeOut();
    }
    $("form").submit(
        function () {
            $(".loading").fadeIn();
        });


function AjaxCreateEmailUser(event) {

    event.preventDefault();

    var userEmail = $("input#inputEmailUser").val();

    if (userEmail === null || userEmail === "" ) {

        AlertSweetTimer('لطفا ایمیل معتبر وارد کنید', 'warning', 3000);
        userEmail.text('');
    } else {


        $.ajax({

            url: "/Home/AddUserEmail",
            type: 'Post',
            data: { email: userEmail },
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
            EndLoading();
            if (result.ok) {
                AlertSweetTimer("شما عضو خبرنامه شدید!", 'success', 3000);
                $("input#inputEmailUser").val('');
            } else {
                AlertSweetTimer(result.message, 'error', 3000);
                

            }

        });
    }

}
