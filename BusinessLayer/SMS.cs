using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class SMS : Message
    {
        private static Dictionary<string, SMS> texts = new Dictionary<string, SMS>();

        public static Dictionary<string, SMS> GetText()
        {
            return texts;
        }

        public SMS(string header, string body) : base(header, body)
        {
            texts.Add(header, this);
        }
    }
}
