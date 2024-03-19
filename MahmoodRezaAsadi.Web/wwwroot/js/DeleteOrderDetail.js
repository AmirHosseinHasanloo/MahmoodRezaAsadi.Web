function DeleteCourse(id, orderId) {
    $.ajax({
        url: "/DeleteCourse/" + orderId + "?courseId=" + id,
    }).done(function (result) {
        $("#order_" + id).hide("slow");
        $("#order_Sum").html(result);
    });
}