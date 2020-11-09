using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Message
    {
        private string header;
        private string body;

        public Message(string header, string body) // Creates message with attributes
        {
            Header = header;
            Body = body;
        }

        // Getters and setters
        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
    }
}
