using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationScreenerAttributeProject.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PhonePatternAttribute : Attribute
    {
        public string Pattern { get; set; }
        public string ErrorMessage { get; set; }

        public PhonePatternAttribute(string pattern)
        {
            Pattern = pattern;
            ErrorMessage = "The provided input for {0} does not match the standard {1} pattern";
        }
    }
}
