using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (CheckNameAndSurname(firstName, lastName) && CheckEmail(email) && ValidateAge(CalculateAge(dateOfBirth)))
            {
                var user = new User(ClientRepository.GetById(clientId), dateOfBirth, email, firstName, lastName);
                if (user.CheckCreditLimit())
                {
                    UserDataAccess.AddUser(user);
                    return true;
                }
            }

            return false;
        }

        public bool CheckNameAndSurname(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }

        public bool CheckEmail(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age;
        }

        public bool ValidateAge(int age)
        {
            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}