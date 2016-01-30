(function () {
	var form = document.getElementById("form-with-returnUrl");
	form.action += form.dataset.returnUrl;
})();