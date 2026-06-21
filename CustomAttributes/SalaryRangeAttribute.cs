using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationScreenerAttributeProject.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SalaryRangeAttribute : Attribute
    {
        public int MinimumSalary { get; set; }
        public string ErrorMessage { get; set; }
        public int MaximumSalary { get; set; }
        
       

        public SalaryRangeAttribute(int minimumSalary, string errorMessage, int maximumSalary)
        {
            SetSalaryproperties(maximumSalary, errorMessage, minimumSalary);
        }



        private void SetSalaryproperties(int maximumSalary, string errorMessage = "", int? minimumSalary = null)
        {
            if(minimumSalary == null)
            {
                errorMessage = "You cannot leave your {0} value empty";
            }
            else
            {
                errorMessage = "{0} can only fall within {1} and {2}";
            }
            
            MinimumSalary = minimumSalary == null ? 0 : minimumSalary.Value;
            MaximumSalary = maximumSalary;
            ErrorMessage = errorMessage;
        }
    }
}
