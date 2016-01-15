$(document).ready(function () {

    (function() {
    	var formContainer; // search.parent.parent

    	formContainer = $("#search").on("focus", function() {
		    formContainer
			    .addClass("active")
			    .removeClass("no-active");
	    }).on("blur", function() {
	    	formContainer
			    .addClass("no-active")
			    .removeClass("active");
	    }).parent().parent();
    })();
})