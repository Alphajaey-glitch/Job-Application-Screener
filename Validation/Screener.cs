using JobApplicationScreenerAttributeProject.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace JobApplicationScreenerAttributeProject.Validation
{
    public static class Screener
    {

        public static bool ValidateParticipant(Type type, string propertyName, string enteredValue, out string errorMessage)
        {
            PropertyInfo? property = type.GetProperty(propertyName);

            var attributes = property?.GetCustomAttributes(false);

            errorMessage = "";

            foreach(var attribute in attributes)
            {
                switch (attribute)
                {
                    case NotBlankAttribute nb:
                        if (!CheckNotBlank(enteredValue))
                        {
                            errorMessage = nb.ErrorMessage;
                            errorMessage = string.Format(errorMessage, property?.Name, nb.MinLength, nb.MaxLength);

                            return false;
                        }
                        break;

                    case MinExperienceAttribute minExp:
                        bool valid = int.TryParse(enteredValue, out int year);

                        if (valid)
                        {
                            if (!CheckMinExperience(minExp, year))
                            {
                                errorMessage = minExp.ErrorMessage;
                                errorMessage = string.Format(errorMessage, property?.Name, minExp.MinYear);

                                return false;
                            }
                        }
                        
                        break;

                    case SalaryRangeAttribute salaryRangeAttribute:
                        valid = int.TryParse(enteredValue, out int amount);
                        if (valid)
                        {
                            if (!CheckSalaryRangeAttribute(salaryRangeAttribute, amount))
                            {
                                errorMessage = salaryRangeAttribute.ErrorMessage;
                                errorMessage = string.Format(errorMessage, property?.Name, salaryRangeAttribute.MinimumSalary, salaryRangeAttribute.MaximumSalary);
                                return false;
                            }
                            
                        }
                        
                        break;

                    case ValidEmailFormatAttribute emailFormatAttribute:
                        if(!CheckValidEmailFormatAttribute(emailFormatAttribute, enteredValue))
                        {
                            errorMessage = emailFormatAttribute.ErrorMessage;
                            errorMessage = string.Format(errorMessage, property?.Name, emailFormatAttribute.Pattern);

                            return false;
                        }
                        break;

                    case PhonePatternAttribute phoneAttribute:
                        if(!CheckPhonePatternAttribute(phoneAttribute, enteredValue))
                        {
                            errorMessage = phoneAttribute.ErrorMessage;
                            errorMessage = string.Format(errorMessage, property?.Name, phoneAttribute.Pattern);

                            return false;
                        }
                        break;
                }
            }
            return true;
        }


        private static bool CheckNotBlank(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return true;
            }
            return false;
        }
        private static bool CheckMinExperience(MinExperienceAttribute minYearAttribute, int enteredValue)
        {
            if(enteredValue >= minYearAttribute.MinYear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool CheckSalaryRangeAttribute(SalaryRangeAttribute salaryRangeAttribute, int enteredvalue)
        {
            if(enteredvalue >= salaryRangeAttribute.MinimumSalary && enteredvalue <= salaryRangeAttribute.MaximumSalary)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool CheckValidEmailFormatAttribute(ValidEmailFormatAttribute validEmailFormatAttribute, string enteredValue)
        {
            if(Regex.IsMatch(enteredValue, validEmailFormatAttribute.Pattern))
            {
                return true;
            }
            return false;
        }
        private static bool CheckPhonePatternAttribute(PhonePatternAttribute phonePaternAttribute, string enteredValue)
        {
            if (Regex.IsMatch(enteredValue, phonePaternAttribute.Pattern))
            {
                return true;
            }
            return false;
        }
    }
}
