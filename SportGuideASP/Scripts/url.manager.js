/**
 * @author  Sagid S.M.
 * @date    2015.05.03
 * 
 * Work with get paramenter (location.search) without reload of page
 * Nothing method reload page!!!
 */

"use strict";

var GET = function(_search){

  if (typeof _search !== "string")
    _search = location.search;

  // put value in key or add "&key=value"
  this.put = function(key, value) {

      var match = _search.match(new RegExp(  // example (in [0] index): ?id=1 or &id=1
        "(?:\\?|\\&)" + key + "((=[^&]*)|(?=&|$))")
      );
      var remove = match === null ? null : match[0];

      // mark key value or empty
      var replace;
      if (value == null){  // or undefined (not using ===)
        replace = "";
      } else {
        var mark = remove === null ?
					(_search.length > 1 ? "&" : "" ) : remove[0];
        replace = mark + key + "=" + value;
      }
      
      
      var newSearch;
      
      
      
      if (remove === null) {   // not found
        // add
        newSearch = _search + replace;
      } else {
        // change
        if (remove[0] === "?" && replace[0] !== "?")
          replace = "?" + replace;

        newSearch = _search.replace(remove, replace)
                    .replace(/^\?&+/, "?");  // ?& -> ?
      }
      
      return this.set(newSearch);
    };

  // @return Array of {key: value}
  this.all = function(){
    var search = _search.substr(1);
    if (search == "")
      return null;

    var result = search.split("&");
    for (var i = 0; i < result.length; ++i){
      var keyIndex = result[i].indexOf("=");

      var json = {};  // .key and .value
      if (keyIndex === -1){
        json.key = result[i];
        json.value = undefined;
      } else {
        json.key = result[i].substr(0, keyIndex);
        json.value = result[i].substr(keyIndex + 1);
      }

      result[i] = json;
    }

    return result;
  };

  // @return value from key
  this.take = function(key) {
    if (!_search) return null;//***

    var params = _search.substr(1).split("&");	// substr - ? (sign)
    for (var i = 0; i < params.length; i++) {
      var couple = params[i].split("=");  // couple is each "key=value"

      if (couple.shift() == key) {    // if key is finding
        return (couple.length === 0) ? "" : couple.join("");
      }
    };

    return undefined;
  };

  // @return
  this.val = function(){
    return _search;
  };

  // remove key and value
  this.remove = function(key) {
    return this.put(key, undefined);
  };

  // set location.search
  // @data: string OR JSON
  // string: id=1
  // JSON: {id:1}
  this.set = function(data){

    if (typeof data === "string") {
      _search = data;
    }
    else {
      // else Array

      // Create location.search (without question mark ("?"))
      // exapmle: id=1&name=John

      var params = new Array();
      for (var i in data) {
        params.push(i + "=" + data[i]);
      }
      _search = params.join("&");
    }


    // Insert mark "?" if not exist
    if (_search.length > 0)
      if (_search[0] !== "?")
        _search = "?" + _search;

    // Change search
    return this;
  };

  // @return string: help
};

GET.help = function(){
    return "Example of use: \n\
window.location.href = http://example.com?id=1/\n\
GET.val('id');      // return 1\n\
GET.put('id', 2);      // location.href = example.com?id=2\n\
GET.put('sex', 'm');   // location.href = example.com?id=2&sex=m\n\
\n\
GET.remove('id')           // or\n\
GET.put('id', undefined);  // location.href = example.com?id=2&sex=m\n\
\n\
//The same (location.search will be = '?id=1&test=2')\n\
GET.set('id=1&test=2');\n\
GET.set('?id=1&test=2');\n\
GET.set({id:1,test:'2'});\n\
";
  };
