using JobApplicationScreenerAttributeProject.Models;
using JobApplicationScreenerAttributeProject.Validation;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace JobApplicationScreenerAttributeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicant = new Applicant();

            Type type = applicant.GetType();

            string applicantName = "";
            string applicantEmail = "";
            int applicantYearsOfExperience;
            int applicantExpectedSalary;
            string applicantPhoneNumber;


            List<Applicant> applicants = new List<Applicant>();

            if (GetStringInput(type, "Please enter your full name: ", "FullName", out applicantName))
            {
                applicant.FullName = applicantName;
            }
            if (GetStringInput(type, "Please enter your email address: ", "EmailAddress", out applicantEmail))
            {
                applicant.EmailAddress = applicantEmail;
            }
            if (GetIntInput(type, "Please enter your years of experience in digit: ", "YearsOfExperience", out applicantYearsOfExperience))
            {
                applicant.YearsOfExperience = applicantYearsOfExperience;
            }
            if (GetStringInput(type, "Please enter your phone number: ", "PhoneNumber", out applicantPhoneNumber))
            {
                applicant.PhoneNumber = applicantPhoneNumber;
            }
            if (GetIntInput(type, "Please enter your expected salary amount: ", "ExpectedSalary", out applicantExpectedSalary))
            {
                applicant.ExpectedSalary = applicantExpectedSalary;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Hired!");
            Console.ResetColor();

            applicants.Add(applicant);

            GetHiredApplicants(applicants);

            Console.ReadKey();
        }

        private static void GetHiredApplicants(List<Applicant> applicants)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Applicants.txt");
            bool fileExists = File.Exists(filePath) && new FileInfo(filePath).Length > 0;

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {

                if (!fileExists)
                {
                    sw.WriteLine($"{"Applicants Information",75}");

                    sw.WriteLine();

                    string header = $"{"Full name",-10} {"Email Address",-5} {"Phone Number",25} {"Years of Experience",30} {"Expected salary",30}";
                    sw.WriteLine(header);
                    sw.WriteLine(new string('-', header.Length));

                }

                foreach (var applicant in applicants)
                {
                    var applicantJson = JsonSerializer.Serialize<Applicant>(applicant);
                    var item = JsonSerializer.Deserialize<Applicant>(applicantJson);

                    sw.WriteLine($"{item.FullName,-10} {item.EmailAddress,-15} {item.PhoneNumber,22} {item.YearsOfExperience,19} {item.ExpectedSalary,33}");
                }
            }
        }

        private static bool GetStringInput(Type type, string prompt, string propertyName, out string fieldValue)
        {
            string errorMessage = "";
            do
            {
                Console.Write(prompt);
                string enteredValue = Console.ReadLine() ?? string.Empty; 

                if(!Screener.ValidateParticipant(type, propertyName, enteredValue, out errorMessage))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Rejected— here's why: {errorMessage}");
                    Console.ResetColor();
                }
                else
                {
                    fieldValue = enteredValue;
                    break;
                }
            }while(true) ;

            return true;
        }
        private static bool GetIntInput(Type type, string prompt, string propertyName, out int fieldValue)
        {
            string errorMessage = "";
            do
            {
                Console.Write(prompt);
                string enteredValue = Console.ReadLine() ?? string.Empty;


                if (!Screener.ValidateParticipant(type, propertyName, enteredValue, out errorMessage))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Rejected— here's why: {errorMessage}");
                    Console.ResetColor();
                }
                else
                {
                    bool valid = int.TryParse(enteredValue, out int value);
                    if (valid)
                    {
                        fieldValue = value;
                        break;
                    }
                   
                }
            } while (true);

            return true;
        }
    }
}
