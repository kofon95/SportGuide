var test;
(function () {
    var categories = $("#sport-category");
    var kindOfSportContainer = $("#kind-of-sport-container");
	var globalSearch = document.getElementById("global-search");
	var genderSelect = document.getElementById("gender-select");
        
	$("#filter-type").on("change", function() {
	    switch (this.value) {
	        case "workouts":
	            globalSearch.action = "/Search";
	            break;
	        case "trainers":
	            globalSearch.action = "/GetTrainers";
	            break;
	        case "halls":
	            globalSearch.action = "/GetHalls";
	            break;
	    }
	});
    
	categories.on("change", function (e) {
        if (this.value.length === 0) {
            kindOfSportContainer.html("");
            return;
        }

        kindOfSportContainer.html('<select><option>Загрузка...</option></select>');
        $.get(getCategoryUrl(this.value), function (e) {
            kindOfSportContainer.html(e);
            kindOfSportContainer.children().change(function () {
                // on change of kind
            })
            // set select tag from "get" parameter
            var getParam = new GET().take("kindOfSportId");
            var selectInput = kindOfSportContainer.children().get(0);
            selectInput.value = getParam;
            if (selectInput.selectedIndex === -1)
                selectInput.selectedIndex = 0;

            kindOfSportContainer.children().change();
        })
    })

    function getCategoryUrl(param) {
        return location.origin + "/_Data/GetKindsOfSportByCategoryId/" + encodeURIComponent(param);
    }
})();