$(document).ready(function () {

    (function() {
        var search = $("#search");
        search.on("focus", function () {
            search.parent().parent()
                .addClass("active")
                .removeClass("no-active");
        }).on("blur", function () {
            search.parent().parent()
                .addClass("no-active")
                .removeClass("active");
        })
    })();
})