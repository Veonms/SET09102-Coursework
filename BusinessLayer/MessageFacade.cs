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

            string[] values = body.Split(null); // Adds every word from body to array

            if (values.Contains("SIR")) // Checks if any word is SIR (Checking if its an Significant Incident Report)
            {
                for (int i = 0; i < values.Length - 1; i++) // Will go through looking for the sort code and its value
                {
                    if (values[i].Contains("Sort") &&
                        values[i + 1].Contains("Code"))
                    {
                        SIR += "Sort Code: " + values[i + 2]; // Adds value to string
                    }

                    else
                        continue;
                }
                for (int i = 0; i < values.Length - 1; i++) // Will go through and check for the nature of incident and its value
                {
                    if (values[i].Contains("Nature") &&
                        values[i + 1].Contains("of") &&
                        values[i + 2].Contains("Incident"))
                    {
                        SIR += "\nNature of Incident: " + values[i + 3]; // Adds value to string
                    }

                    else
                        continue;
                }
            }

            return SIR; // Returns string
        }
        public string checkURL(string body)
        {
            string newBody = "";
            string[] values = body.Split(null); // Adds every word from the body to an array

            foreach (string s in values)
            {
                if (s.StartsWith("http:") || s.StartsWith("https:")) // Checks if the word begins with http: or https:
                {
                    newBody += "<URL Quarantined> "; // Adds <URL Quarantined> to string
                }

                else
                    newBody += s + " "; // Adds word to string
            }
            return newBody.Trim(); // Returns string with any leeading or trailing whitespace removed
        }

        public List<string> GetUrl(string body)
        {
            List<string> urls = new List<string>();

            string[] values = body.Split(null); // Adds all words to an array

            foreach (string s in values)
            {
                if (s.StartsWith("http:") || s.StartsWith("https:")) // Checks if the word begins with http: or https:
                {
                    urls.Add(s); // Adds url to list
                }

                else
                    continue;
            }

            return urls; // Returns list
        }

        public List<string> GetMentions(string body)
        {
            List<string> mentions = new List<string>();

            string[] values = body.Split(null); // Adds all words to array

            foreach (string s in values) // Goees through every word
            {
                if (s.StartsWith("@")) // Checks if word begins with "@"
                {
                    mentions.Add(s); // Adds word to list
                }

                else
                    continue;
            }
            return mentions; // Adds List
        }

        public List<string> GetHashtag(string body)
        {
            List<string> hashtag = new List<string>();

            string[] values = body.Split(null); // Adds every word from body to an array

            foreach (string s in values) // Goes through every word
            {
                if (s.StartsWith("#")) // Check if the word beings with "#"
                {
                    hashtag.Add(s); // Adds to list
                }

                else
                    continue;
            }
            return hashtag; // Returns list
        }

        public List<string> DisplayData()
        {
            List<string> messages = new List<string>();

            foreach (KeyValuePair<string, SMS> pair in SMS.GetText()) // Goes through each SMS
            {
                messages.Add(pair.Value.Header); // Adds header to list
                messages.Add(pair.Value.Body); // Adds body to list
            }
            foreach (KeyValuePair<string, Tweet> pair in Tweet.GetTweet()) // Goes through each Tweet
            {
                messages.Add(pair.Value.Header); // Adds header to list
                messages.Add(pair.Value.Body); // Adds body to list
            }
            foreach (KeyValuePair<string, Email> pair in Email.GetEmails()) // Goes through each Email
            {
                messages.Add(pair.Value.Header); // Adds header to list
                messages.Add(pair.Value.Body); // Adds body to list
            }
            return messages; // reeturns list
        }

        public Boolean AddMessage(string header, string body)
        {
            try
            {
                if (header.StartsWith("S") && // Checks if thee message is an SMS
                    !SMS.GetText().ContainsKey(header) && // Checks if message exists
                    header.Length == 10 && // Checks headre is 10 characters
                    body.Length <= 140) // checks if body is <= 140 characters
                {
                    SMS s = new SMS(header, body); // Adds meessage
                    return true;
                }
                if (header.StartsWith("E") && // Checks if the message is an email
                    !Email.GetEmails().ContainsKey(header) && // Checks if message exists
                    header.Length == 10 && // Checks headre is 10 characters
                    body.Length <= 1024) // checks if body is <= 1024 characters
                {
                    Email e = new Email(header, body); // Adds meessage
                    return true;
                }
                if (header.StartsWith("T") && // Checks is the message is a tweeet
                    !Tweet.GetTweet().ContainsKey(header) && // Checks if message exists
                    header.Length == 10 && // Checks headre is 10 characters
                    body.Length <= 140) // checks if body is <= 140 characters
                {
                    Tweet t = new Tweet(header, body); // adds message
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
                FacadeSingleton fs = FacadeSingleton.GetInstance(); // Creates singleton if one doesnt exist

                ArrayList data = new ArrayList();
                foreach (KeyValuePair<string, SMS> pair in SMS.GetText()) // Goes through each SMS
                {
                    data.Add(pair.Value); // Adds message
                }
                foreach (KeyValuePair<string,Tweet> pair in Tweet.GetTweet()) // Goes through each Tweet
                {
                    data.Add(pair.Value); // Adds message
                }
                foreach (KeyValuePair<string, Email> pair in Email.GetEmails()) // Goes through each Email
                {
                    data.Add(pair.Value); // Adds message
                }

                fs.SaveMessages(data); // Calls SaveMessages method in thee FacadeeSingleton

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
                FacadeSingleton fs = FacadeSingleton.GetInstance(); // Creates singleton if one doesnt exist

                List<string> data = fs.LoadMessage(filename);  // Calls LoadMessage from FacadeSingleton and stores the list

                if (data.Count() == 0)
                {
                    return false;
                }

                for (int i = 0; i < data.Count - 1; i += 2)
                {
                    AddMessage(data[i], data[i + 1]); // Adds all thee messages
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
