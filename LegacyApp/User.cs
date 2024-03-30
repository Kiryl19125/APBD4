using System;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public User(Client client, DateTime dateOfBirth, string emailAddress, string firstName, string lastName)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;

            ProcessClient(client);
        }

        private void ProcessClient(Client client)
        {
            if (client.Type == ClientType.VeryImportantClient)
            {
                HasCreditLimit = false;
            }
            else if (client.Type == ClientType.ImportantClient)
            {
                int creditLimit = UserCreditService.GetCreditLimit(LastName, DateOfBirth);
                creditLimit *= 2;
                CreditLimit = creditLimit;
            }
            else
            {
                HasCreditLimit = true;
                int creditLimit = UserCreditService.GetCreditLimit(LastName, DateOfBirth);
                CreditLimit = creditLimit;
            }
        }

        public bool CheckCreditLimit()
        {
            if (HasCreditLimit && CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
    }
}