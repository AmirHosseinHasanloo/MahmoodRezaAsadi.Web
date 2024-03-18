function Accept(id) {
    $.ajax({
        url: "/Admin/Comments/Accept/" + id,
    }).done(function () {
        $("#comment_" + id).hide("slow");
    })
}

function Reject(id) {
    $.ajax({
        url: "/Admin/Comments/Reject/" + id,
    }).done(function () {
        $("#comment_" + id).hide("slow");
    })
}