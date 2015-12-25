using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportGuideASP
{
    public static class HelperExtentions
    {
        public static MvcHtmlString ToJSString(this string str)
        {
            if (str == null || str.Length == 0)
                return MvcHtmlString.Create("''");
            return MvcHtmlString.Create(str);
        }
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            var anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
}