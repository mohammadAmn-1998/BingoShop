$(document).ready(function () {
    console.log("sdfsdfsdfdsfdsfsdfsdfsdfsd");
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

function loadCkeditor4() {
    if (!document.getElementById("CkEditor4"))
        return;

    $("body").append('<script src="/ckeditor4/ckeditor/ckeditor.js"></script>');

    CKEDITOR.replace('CkEditor4', {
        customConfig: '/ckeditor4/ckeditor/config.js'
    });

}