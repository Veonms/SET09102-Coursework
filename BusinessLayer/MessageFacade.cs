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
        public string GetSIR(string body)
        {
            string SIR = "";
            string[] values = body.Split(null);

            if (values.Contains("SIR"))
            {
                for (int i = 0; i < values.Length-1; i++)
                {
                    if (values[i] == "Sort" && 
                        values[i+1] == "Code")
                    {
                        SIR += "Sort Code: " + values[i+2];
                    }

                    else
                        continue;
                }
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i] == "Nature" &&
                        values[i + 1] == "of" &&
                        values[i+2] == "Incident")
                    {
                        SIR += "\nNature of Incident: " + values[i + 3];
                    }

                    else
                        continue;
                }
            }

            return SIR;
        }
        public string checkURL(string body)
        {
            string newBody = "";
            string[] values = body.Split(null);

            foreach (string s in values)
            {
                if (s.StartsWith("http:") || s.StartsWith("https:"))
                {
                    newBody += "<URL Quarantined> ";
                }

                else
                    newBody += s + " ";
            }
            newBody.Trim();
            return newBody;
        }

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
                FacadeSingleton fs = FacadeSingleton.GetInstance();

                List<string> data = DisplayData();
                fs.SaveMessages(data);

            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public Boolean LoadMessages(string filename)
        {
            try
            {
                // Allows acceess to DataLayer
                FacadeSingleton fs = FacadeSingleton.GetInstance();

                List<string> data = fs.LoadMessage(filename);
                for (int i=0; i<data.Count-1; i += 2)
                {
                    AddMessage(data[i], data[i+1]);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            // Allows acceess to DataLayer
            FacadeSingleton fs = FacadeSingleton.GetInstance();
            fs.Save();
            

        }
    }
}
