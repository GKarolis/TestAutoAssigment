using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEB.Test.NewEmployee
{
    class Employee
    {
        private string firstName;
        private string lastName;
        private string userName;

        Random random = new Random();

        public string FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(firstName))
                {
                    firstName = RandomString();
                }
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                if(string.IsNullOrEmpty(lastName))
                {
                    lastName = RandomString();
                }
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(userName))
                {
                    userName = RandomString();
                }
                return userName;
            }
            set
            {
                userName = value;
            }
        }        

        public string RandomString()
        {
            var letters = "abcdefghijklmnopqrstuvwxyz";
            var stringLetters = new char[8];
            for (int i = 0; i < stringLetters.Length; i++)
            {
                stringLetters[i] = letters[random.Next(letters.Length)];
            }
            return new string(stringLetters); 
        }
    }
}
