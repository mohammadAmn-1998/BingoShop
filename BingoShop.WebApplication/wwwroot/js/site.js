
//success
//question
//info
//warning
//error

    function AlertSweet(title, message, icon) {
        Swal.fire(title, message, icon);
    }
    function AlertSweetTimer(title, icon, time) {
        Swal.fire({
            position: 'center',
            icon: icon,
            title: title,
            showConfirmButton: true,
            timer: time
        });
    }


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

 

