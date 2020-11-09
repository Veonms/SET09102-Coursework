using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class MessageFacade
    {
        public ArrayList DisplayData()
        {
            ArrayList messages = new ArrayList();

            messages.Add(SMS.GetText().Values);
            messages.Add(Tweet.GetTweet().Values);
            messages.Add(Email.GetEmails().Values);

            return messages;
        }

        public Boolean AddMessage(string header, string body)
        {
            try
            {
                if (header.StartsWith("S") &&
                    !SMS.GetText().ContainsKey(header) &&
                    header.Length == 10)
                {
                    SMS s = new SMS(header, body);
                    return true;
                }
                if (header.StartsWith("E") &&
                    !Email.GetEmails().ContainsKey(header) &&
                    header.Length == 10)
                {
                    Email e = new Email(header, body);
                    return true;
                }
                if (header.StartsWith("T") && 
                    !Tweet.GetTweet().ContainsKey(header) && 
                    header.Length == 10)
                {
                    Tweet t = new Tweet(header, body);
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Boolean SaveMessages()
        {
            try
            {
                if (true)
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                return true;
            }
        }

        public Boolean Load(string filename)
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            Boolean success = fs.Load(filename);
            return success;
        }

        public void Save()
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            fs.Save();
            

        }
    }
}
