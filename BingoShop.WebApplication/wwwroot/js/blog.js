function GetComments(ownerId, commentFor, pageId) {

    console.log(commentFor);
    $.ajax({
        type: "Get",
        url: `/Comments/${ownerId}/${commentFor}?pageId=${pageId}`
    }).done(function (res) {
        console.log(res);
        var model = [];
        model = JSON.parse(res);
        var h3CommentCount = $("h3#h3CommentCount");
        h3CommentCount.text(`${model.CommentsCount} نظر`);
        var iCommentCount = $("i#iCommentCount");
        iCommentCount.text(` ${model.Comments.length} `);
        var CommentParentList = $("ul#CommentParentList");
        for (var i = 0; i < model.Comments.length; i++) {
            var avatar = model.Comments[i].Avatar;
            if (avatar === "undefined" || avatar === null)
                avatar = "Default.png";
            var commentParemt = `
					 <li class="comment" id="liParentComment_${model.Comments[i].Id}">
									<div class="comment-body">
										<div class="comment-avatar">
											<img alt="${model.Comments[i].FullName}" src="/assets/images/user_img100/${model.Comments[i].Avatar}">
										</div>
										<div class="comment-text">
											<h6 class="comment-author">${model.Comments[i].FullName}</h6>
											<div class="comment-metadata">
												<a href="#" class="comment-date">${model.Comments[i].CreationDate}</a>
											</div>
											<p>
											${model.Comments[i].Text}
											</p>
											<a onclick="AnswerComment(${model.Comments[i].Id},'${model.Comments[i].FullName}')" style="cursor:pointer;float:left"
											class="comment-reply">پاسخ</a>
										</div>
									</div>
								</li>
										`;
            CommentParentList.append(commentParemt);
            if (model.Comments[i].Childs.length > 0) {
                console.log("has Childs");
                var parent = $(`li#liParentComment_${model.Comments[i].Id}`);
                for (var j = 0; j < model.Comments[i].Childs.length; j++) {

                    avatar = model.Comments[i].Childs[j].Avatar;
                    if (avatar === "undefined" || avatar === null)
                        avatar = "Default.png";

                    var childComment = `
						<ul class="children px-5">
										<li class="comment">
											<div class="comment-body">
												<div class="comment-avatar">
													<img alt="${model.Comments[i].Childs[j].FullName}" src="/assets/images/user_img100/${model.Comments[i].Childs[j].Avatar}">
												</div>
												<div class="comment-text">
													<h6 class="comment-author">${model.Comments[i].Childs[j].FullName}</h6>
													<div class="comment-metadata">
														<a href="#" class="comment-date">${model.Comments[i].Childs[j].CreationDate}</a>
													</div>
													<p>
													${model.Comments[i].Childs[j].Text}
													</p>
												</div>
											</div>
										</li>
									</ul>
						`;
                    parent.append(childComment);
                }
            }

        }

        var getMoreComment = $("div#getMoreCommentDiv");
        getMoreComment.html("");
        if (model.TotalPages > model.PageId) {
            var getMore = `

							<a class="btn btn-lg btn-color px-4 py-2" onclick="GetComments( '${model.OwnerId}', '${commentFor}' , ${model.PageId + 1})">
								ادامه نظرات
							</a>

					`;
            getMoreComment.append(getMore);
        }
    });
}
function AnswerComment(id, fullName) {
    $("#labalFullNameComment").text(`پاسخ برای  ${fullName}`);
    var parent = $("input#parentIdComment");
    parent.val(id);
    ScroolToEleman('respond');
    $("textarea#textComment").select();
    return;
}
function submitComment() {
    var fullName = $("input#fullNameComment").val();
    var email = $("input#emailComment").val();
    var text = $("textarea#textComment").val();
    if (fullName === null || fullName === "") {
        $("span#fullNameCommentValid").text("نام کامل اجباری است .");
    }
    else {
        $("span#fullNameCommentValid").text("");
        if ((email == null && email == "")) {
            $("span#emailCommentValid").text("یک ایمیل معتبر وارد کنید .");
        }
        else {
            $("span#emailCommentValid").text("");
            if (text === "" || text === null) {
                $("span#textCommentValid").text("متن نظر اجباری است .");
            }
            else {
                $("span#textCommentValid").text("");
                $("form#formComment").submit();
            }
        }
    }
}

function validateEmail(email) {
    const re = /^[a-zA-Z0-9._%+-]+[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return re.test(String(email).toLowerCase());
}
function ScroolToEleman(id) {
    $('html, body').animate({
        scrollTop: $(`#${id}`).offset().top
    }, 1000);
}