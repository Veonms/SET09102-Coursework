using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Email : Message // Inherits from Message
    {
        // Creates private Dictionary
        private static Dictionary<string, Email> emails = new Dictionary<string, Email>();

        // Allows the dictionary to be accessed
        public static Dictionary<string, Email> GetEmails()
        {
            return emails;
        }

        // Adds email to the dictionary
        public Email(string header, string body) : base(header, body)
        {
            emails.Add(header, this);
        }
    }
}
