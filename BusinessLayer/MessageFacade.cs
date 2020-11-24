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
            string message = ""; // Creates empty string

            string[] temp = body.Split(null); // Creates empty array

            // Checks every word to see if it is an abbriviation (abb.). If so adds the expanded version in <>
            foreach (string s in temp)
            {
                if (s.ToUpper().Equals(s)) // Checks if the abb. is uppercase if not add message to string
                {
                    try
                    {
                        FacadeSingleton fs = FacadeSingleton.GetInstance();
                        Dictionary<string, string> abb = fs.GetAbbreviations();
                        if (abb.ContainsKey(s))
                        {
                            message += s + " <" + abb[s] + "> "; // Adds abb. and expanded abb. to string
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                    message += s + " "; // Adds word to string
            }
            return message.Trim(); // Returns message with any leading or trailing whitespace removed
        }

        public List<string> GetURLList()
        {
            List<string> URLs = new List<string>();

            foreach(KeyValuePair<string, Email> pair in Email.GetEmails()) // Checks every email
            {
                List<string> temp = GetUrl(pair.Value.Body); // Uses method GetUrl to find urls within the body

                foreach (string s in temp)
                {
                    URLs.Add(s); // Adds URL to list
                }
            }
            return URLs; // Returns list
        }

        public List<string> GetSIRList()
        {
            List<string> SIRList = new List<string>();

            foreach (KeyValuePair<string, Email> pair in Email.GetEmails()) // Checks every email
            {
                string temp = ""; // Creates empty string

                temp = GetSIR(pair.Value.Body); // Uses the GetSIR method to get all the Significant Incident Reports

                if (!temp.Equals("")) // If SIR exists add to list
                {
                    SIRList.Add(temp);
                }

            }
            return SIRList; // Returns list
        }
        public List<string> GetMentionsList()
        {
            List<string> mentions = new List<string>();

            foreach(KeyValuePair<string, Tweet> pair in Tweet.GetTweet()) // Checks every Tweet
            {
                string[] values = pair.Value.Body.ToString().Split(null); // Removes the body from the tweet and adds each word to array

                List<string> temp = new List<string>();

                for (int i=0; i<values.Length-1; i++) // Checks every word to see if the word begins with "@"
                {
                    if (values[i].StartsWith("@") && // If the word begins with "@" and does not already exist in the list
                        !mentions.Contains(values[i]))
                    {
                        temp.Add(values[i]); // Adds word
                    }
                }
                if (temp.Count > 1)
                {
                    for (int i=1; i<temp.Count; i++)
                    {
                        mentions.Add(temp[i]); // Adds all the mentions except the first as that is the user how sent the Tweet
                    }
                }
            }

            return mentions; // Returns list
        }

        public Dictionary<string, int> GetHashtagList()
        {
            Dictionary<string, int> hashtags = new Dictionary<string, int>();

            foreach(KeyValuePair<string, Tweet> pair in Tweet.GetTweet()) // Goes through every Tweet
            { 
                string[] values = pair.Value.Body.ToString().Split(null); // Adds all words from body to array

                for (int i=0; i<values.Length-1; i++)
                {
                    if (values[i].StartsWith("#")) // If the word begins with "#"
                    {
                        if (!hashtags.ContainsKey(values[i])) // Checks if hashtag doesnt exists
                        {
                            hashtags.Add(values[i],1); // Adds hashtag to dictionary with thee value of 1
                        }
                        else
                        {
                            hashtags[values[i]]++; // Adds 1 to the value of the hashtag, stored in the dictionary
                        }

                    }
                }
            }

            return hashtags; // returns dictionary
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
            return newBody.Trim();
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
