using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MACPortal.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString CustomTextBox(this HtmlHelper htmlHelper, string id, string name)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", "form-group");
            builder.MergeAttribute("id", id);
            builder.MergeAttribute("style", "display:none");

            var label = new TagBuilder("label");
            label.MergeAttribute("class", "control-label vertical-form-label");
            label.InnerHtml = name;
            builder.InnerHtml += label.ToString();

            var mainDiv = new TagBuilder("div");
            mainDiv.MergeAttribute("class", "input-list-container btn-group");
            mainDiv.MergeAttribute("data-toggle", "buttons");

            var innerLabel = new TagBuilder("label");
            innerLabel.MergeAttribute("class", "btn btn-default");
            innerLabel.MergeAttribute("data-toggle", "buttons");

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}