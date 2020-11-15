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
        public string abbreviations(string body)
        {
            string message = "";

            string[] temp = body.Split(null);

            foreach (string s in temp)
            {
                if (s.ToUpper().Equals(s))
                {
                    try
                    {
                        FacadeSingleton fs = FacadeSingleton.GetInstance();
                        Dictionary<string, string> abb = fs.GetAbbreviations();
                        if (abb.ContainsKey(s))
                        {
                            message += s + " <" + abb[s] + "> ";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                    message += s + " ";
            }
            return message;
        }

        public List<string> GetURLList()
        {
            List<string> URLs = new List<string>();
            foreach(KeyValuePair<string, Email> pair in Email.GetEmails())
            {
                List<string> temp = GetUrl(pair.Value.Body);

                foreach (string s in temp)
                {
                    URLs.Add(s);
                }
            }
            return URLs;
        }

        public List<string> GetSIRList()
        {
            List<string> SIRList = new List<string>();
            foreach (KeyValuePair<string, Email> pair in Email.GetEmails())
            {
                string temp = "";
                temp = GetSIR(pair.Value.Body);
                if (!temp.Equals(""))
                {
                    SIRList.Add(temp);
                }

            }
            return SIRList;
        }
        public List<string> GetMentionsList()
        {
            List<string> mentions = new List<string>();

            foreach(KeyValuePair<string, Tweet> pair in Tweet.GetTweet())
            {
                string[] values = pair.Value.Body.ToString().Split(null);
                List<string> temp = new List<string>();

                for (int i=0; i<values.Length-1; i++)
                {
                    if (values[i].StartsWith("@") &&
                        !mentions.Contains(values[i]))
                    {
                        temp.Add(values[i]);
                    }
                }
                if (temp.Count > 1)
                {
                    for (int i=1; i<temp.Count; i++)
                    {
                        mentions.Add(temp[i]);
                    }
                }
            }

            return mentions;
        }

        public Dictionary<string, int> GetHashtagList()
        {
            Dictionary<string, int> hashtags = new Dictionary<string, int>();

            foreach(KeyValuePair<string, Tweet> pair in Tweet.GetTweet()){
                string[] values = pair.Value.Body.ToString().Split(null);

                for (int i=0; i<values.Length-1; i++)
                {
                    if (values[i].StartsWith("#"))
                    {
                        if (!hashtags.ContainsKey(values[i]))
                        {
                            hashtags.Add(values[i],1);
                        }
                        else
                        {
                            hashtags[values[i]]++;
                        }

                    }
                }
            }

            return hashtags;
        }

        public string GetSIR(string body)
        {
            string SIR = "";
            string[] values = body.Split(null);

            if (values.Contains("SIR"))
            {
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i].Contains("Sort") &&
                        values[i + 1].Contains("Code"))
                    {
                        SIR += "Sort Code: " + values[i + 2];
                    }

                    else
                        continue;
                }
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i].Contains("Nature") &&
                        values[i + 1].Contains("of") &&
                        values[i + 2].Contains("Incident"))
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

            foreach (KeyValuePair<string, SMS> pair in SMS.GetText())
            {
                messages.Add(pair.Value.Header);
                messages.Add(pair.Value.Body);
            }
            foreach (KeyValuePair<string, Tweet> pair in Tweet.GetTweet())
            {
                messages.Add(pair.Value.Header);
                messages.Add(pair.Value.Body);
            }
            foreach (KeyValuePair<string, Email> pair in Email.GetEmails())
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
                    header.Length == 10 &&
                    body.Length <= 140)
                {
                    SMS s = new SMS(header, body);
                    return true;
                }
                if (header.StartsWith("E") &&
                    !Email.GetEmails().ContainsKey(header) &&
                    header.Length == 10 &&
                    body.Length <= 1024)
                {
                    Email e = new Email(header, body);
                    return true;
                }
                if (header.StartsWith("T") &&
                    !Tweet.GetTweet().ContainsKey(header) &&
                    header.Length == 10 &&
                    body.Length <= 140)
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

                ArrayList data = new ArrayList();
                foreach (KeyValuePair<string, SMS> pair in SMS.GetText())
                {
                    data.Add(pair.Value);
                }
                foreach (KeyValuePair<string,Tweet> pair in Tweet.GetTweet())
                {
                    data.Add(pair.Value);
                }
                foreach (KeyValuePair<string, Email> pair in Email.GetEmails())
                {
                    data.Add(pair.Value);
                }

                fs.SaveMessages(data);

            }
            catch (Exception)
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

                if (data.Count() == 0)
                {
                    return false;
                }

                for (int i = 0; i < data.Count - 1; i += 2)
                {
                    AddMessage(data[i], data[i + 1]);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
