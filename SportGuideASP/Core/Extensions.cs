using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SportGuideASP.Core
{
    public static class Extensions
    {
        public static bool CanGoToAdminPanel(this HtmlHelper html)
        {
            return ClaimsPrincipal.Current.Claims
                .Where(t => t.Type == ClaimTypes.Role)
                .Select(t => t.Value)
                .Where(t => t == "Admin" || t == "Moder")
                .Count() > 0;
        }
    }
}