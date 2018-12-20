using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.common.LinqExt
{
    //public static class LinqExt
    //{
    //    public static string MyValidationSummary(this System.Web.Mvc.HtmlHelper helper, string validationMessage = "")
    //    {
    //        string retVal = "";
    //        if (helper.ViewData.ModelState.IsValid)
    //            return "";

    //        retVal += "<div class='notification-warnings'><span class='notification-icon'>";
    //        if (!String.IsNullOrEmpty(validationMessage))
    //            retVal += helper.Encode(validationMessage);
    //        retVal += "</span>";
    //        retVal += "<div class='text'>";
    //        foreach (var key in helper.ViewData.ModelState.Keys)
    //        {
    //            foreach (var err in helper.ViewData.ModelState[key].Errors)
    //                retVal += "<p>" + helper.Encode(err.ErrorMessage) + "</p>";
    //        }
    //        retVal += "</div></div>";
    //        return retVal.ToString();
    //    }
    //}
}