using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationScreenerAttributeProject.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false )]
    public class MinExperienceAttribute : Attribute
    {
        public int MinYear { get; set; }
        public string ErrorMessage { get; set; }

        public MinExperienceAttribute(int minYear)
        {
            SetProperties(minYear : minYear);
        }
        private void SetProperties(string errorMessage = "", int? minYear = null)
        {
            if (minYear == null)
            {
                errorMessage = "Field {0} cannot be left blank";

            }
            else
            {
                errorMessage = "{0} required for this is {1} or above";
            }
            MinYear = minYear == null ? 0 : minYear.Value;
            ErrorMessage = errorMessage;
            
        }

    }
}
