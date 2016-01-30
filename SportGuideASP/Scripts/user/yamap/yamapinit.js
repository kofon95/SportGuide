"use strict"

var YaMap = {
	map:      null,  // map
	startMap: null,	 // promise
	coords:   null,  // coordinates
};
// coords makhachala = [42.979618, 47.490981]

(function () {
	var _resolve;
	var promise = new Promise(function (resolve) {
		_resolve = resolve;
	});

	var started = false;
	YaMap.startMap = function () {
		if (!started) {
			loadYandexMap();
			started = true;
		}
		return promise;
	};

	function loadYandexMap() {
		var hallPlacemark;
		ymaps.ready(function () {
			YaMap.map = new ymaps.Map('map', {
				// При инициализации карты обязательно нужно указать
				// её центр и коэффициент масштабирования.
				center: YaMap.coords,
				zoom: 13
			}, {
				searchControlProvider: 'yandex#search'
			});


			YaMap.map.events.add("click", function (e) {
				var coords = e.get("coords");

				if (hallPlacemark) {
					hallPlacemark.geometry.setCoordinates(coords);
				}
				else {
					hallPlacemark = createPlacemark(coords);
					YaMap.map.geoObjects.add(hallPlacemark);
					// Слушаем событие окончания перетаскивания на метке.
					hallPlacemark.events.add('dragend', function () {
						getAddress(hallPlacemark.geometry.getCoordinates());
					});
				}

				getAddress(coords);
			})

			_resolve();
		})

		// Создание метки
		function createPlacemark(coords) {
			return new ymaps.Placemark(coords, {
				iconContent: 'поиск...'
			}, {
				preset: 'islands#violetStretchyIcon',
				draggable: true
			});
		}

		var addressElem = document.getElementById("Address");
		var longitude = document.getElementById("LocationLongitude");
		var latitude = document.getElementById("LocationLatitude");

		// Определяем адрес по координатам (обратное геокодирование)
		function getAddress(coords) {
			hallPlacemark.properties.set('iconContent', 'поиск...');
			ymaps.geocode(coords).then(function (res) {
				var firstGeoObject = res.geoObjects.get(0);

				var address = firstGeoObject.properties.get('name');
				var fullAddress = firstGeoObject.properties.get('text');
				hallPlacemark.properties.set({
					iconContent: address,
					balloonContent: fullAddress
				});

				// setFields
				addressElem.value = address;
				latitude.value = coords[0];
				longitude.value = coords[1];
			});
		}
	};
})();