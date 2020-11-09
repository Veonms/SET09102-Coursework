using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Email : Message
    {
        private static Dictionary<string, Email> emails = new Dictionary<string, Email>();

        public static Dictionary<string, Email> GetEmails()
        {
            return emails;
        }

        public Email(string header, string body) : base(header, body)
        {
            emails.Add(header, this);
        }

        private ArrayList urls = new ArrayList();
        private ArrayList sirList = new ArrayList();

        public ArrayList URLs
        {
            get { return urls; }
            set { urls = value; }
        }

        public ArrayList SIRList
        {
            get { return sirList; }
            set { sirList = value; }
        }
    }
}
