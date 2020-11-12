using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class MessageFacade
    {
        public List<string> GetUrl(string body)
        {
            List<string> urls = new List<string>();

            string[] values = body.Split(null);

            foreach (string s in values)
            {
                if (s.StartsWith("http:") || s.StartsWith("https:"))
                {
                    urls.Add(s);
                }

                else
                    continue;
            }

            return urls;
        }

        public List<string> GetSIR(string body)
        {
            List<string> SIR = new List<string>();

            //

            return SIR;
        }

        public List<string> GetMentions(string body)
        {
            List<string> mentions = new List<string>();
            string[] values = body.Split(null);

            foreach (string s in values)
            {
                if (s.StartsWith("@"))
                {
                    mentions.Add(s);
                }

                else
                    continue;
            }
            return mentions;
        }

        public List<string> GetHashtag(string body)
        {
            List<string> hashtag = new List<string>();
            string[] values = body.Split(null);

            foreach (string s in values)
            {
                if (s.StartsWith("#"))
                {
                    hashtag.Add(s);
                }

                else
                    continue;
            }
            return hashtag;
        }

        public List<string> DisplayData()
        {
            List<string> messages = new List<string>();

            foreach (KeyValuePair<string,SMS> pair in SMS.GetText())
            {
                messages.Add(pair.Value.Header);
                messages.Add(pair.Value.Body);
            }
            foreach (KeyValuePair<string,Tweet> pair in Tweet.GetTweet())
            {
                messages.Add(pair.Value.Header);
                messages.Add(pair.Value.Body);
            }
            foreach (KeyValuePair<string,Email> pair in Email.GetEmails())
            {
                messages.Add(pair.Value.Header);
                messages.Add(pair.Value.Body);
            }
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
