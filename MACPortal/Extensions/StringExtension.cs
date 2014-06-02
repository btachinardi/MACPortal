using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MACPortal.Extensions
{
    public static class StringExtension
    {
        public static string ShortName(this string value, int maxLength = 31)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            var substs = value.Split(new[] { ' ' });
            var shortName = substs[0] + (substs.Length > 1 ? " " + substs[1] : "") + (substs.Length > 2 ? " " + substs[2] : "");
            return shortName.Length > maxLength ? shortName.Substring(0, maxLength) : shortName;
        }
    }
}