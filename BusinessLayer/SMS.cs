using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class SMS : Message // Inherits from Message
    {
        // Creates private dictionary
        private static Dictionary<string, SMS> texts = new Dictionary<string, SMS>();

        // Returns dictionary
        public static Dictionary<string, SMS> GetText()
        {
            return texts;
        }

        // Adds SMS to dictionary
        public SMS(string header, string body) : base(header, body)
        {
            texts.Add(header, this);
        }
    }
}
