using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationScreenerAttributeProject.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidEmailFormatAttribute : Attribute
    {
        public string Pattern { get; set; }
        public string ErrorMessage { get; set; }

        public ValidEmailFormatAttribute(string pattern)
        {
            Pattern = pattern;
            ErrorMessage = "The provided input for {0} does not match the standard {1} format";
        }
    }
}
