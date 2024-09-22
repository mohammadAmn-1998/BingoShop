
    function confirmSubmission() {
			return confirm("آیا مطمعنید؟");
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

    //event listener for input type file for preventing choosing any file except image file before user submit the form
    document.getElementById('file_input').addEventListener('change', function(event) {
			const file = event.target.files[0];
    if (validateImageFile(file)) {
				if (file && file.type.startsWith('image/')) {
					const reader = new FileReader();
    reader.onload = function(e) {
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

