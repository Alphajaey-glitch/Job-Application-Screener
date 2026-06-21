using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationScreenerAttributeProject.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotBlankAttribute : Attribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string ErrorMessage { get; set; }

        public NotBlankAttribute()
        {
            ErrorMessage = "{0} cannot be left blank";
        }
        public NotBlankAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            ErrorMessage = "{0} should not be less than {1} and greater than {2}";
        }
    }
}
