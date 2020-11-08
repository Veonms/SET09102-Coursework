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
            messHeader = header;
            messBody = body;
        }

        // Getters and setters
        public string messHeader
        {
            get { return header; }
            set { messHeader = value; }
        }

        public string messBody
        {
            get { return body; }
            set { messBody = value; }
        }
    }
}
