using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            CheckNameAndSurname(firstName, lastName);
            // if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            // {
            //     return false;
            // }

            CheckEmail(email);
            // if (!email.Contains("@") && !email.Contains("."))
            // {
            //     return false;
            // }

            // var now = DateTime.Now;
            // int age = now.Year - dateOfBirth.Year;
            // if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            ValidateAge(CalculateAge(dateOfBirth));

            // if (CalculateAge(dateOfBirth) < 21)
            // {
            //     return false;
            // }

            // var clientRepository = new ClientRepository();
            var client = ClientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == ClientType.VeryImportantClient)
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == ClientType.ImportantClient)
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        
        //=====================================================================================================

        private bool CheckNameAndSurname(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }

        private bool CheckEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age;
        }

        private bool ValidateAge(int age)
        {
            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}