
$(function () {
    $("#listComment").load("/Course/ShowComment/" + $("#CourseId").val());
});

CKEDITOR.replace("comment_Comment", {
    customConfig: '/js/Config.js'
});

function Success() {
    var editor = CKEDITOR.instances['comment_Comment'];
    editor.setData('');
    Swal.fire({
        title: "موفقیت آمیز!",
        text: "کاربر گرامی نظر شما پس از تایید کارشناسان نمایش داده خواهد شد",
        icon: "success"
    });
}

function ReplyComment(id) {
    $("#ParentId").val(id);
    $("html, body").animate({ scrollTop: $("#myDiv").offset().top }, 1000);
}

function ShowPageComments(pageId) {
    $("#listComment").load("/Course/ShowComment/" + $("#CourseId").val() + "?pageId=" + pageId);
}