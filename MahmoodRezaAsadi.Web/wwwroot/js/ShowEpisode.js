function GetEpisode(courseId, episodeId) {
    $("html, body").animate({ scrollTop: $("#episode").offset().top }, 1000);

    $.ajax({
        url: "/Course/ShowEpisode/" + courseId + "?episode=" + episodeId,
        type: "Get"
    }).done(function (result) {
        $("#episode").html(result);
    });
}
