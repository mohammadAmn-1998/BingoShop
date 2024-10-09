
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

 

