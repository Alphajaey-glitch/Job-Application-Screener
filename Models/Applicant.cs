using JobApplicationScreenerAttributeProject.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JobApplicationScreenerAttributeProject.Models
{
    [Serializable]
    public class Applicant
    {
        [NotBlank(4, 30)]
        public string FullName { get; set; }

        [NotBlank]
        [ValidEmailFormat(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string EmailAddress { get; set; }

        [NotBlank]
        [MinExperience(2)]
        public int YearsOfExperience { get; set; }

        [NotBlank]
        [SalaryRange(30000, "{0} can only fall within {1} and {2}", 150000)]
        public int ExpectedSalary { get; set; }

        [NotBlank]
        [PhonePattern(@"^\+?[0-9]{1,4}?[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")]
        public string PhoneNumber { get; set; }
    }
}
