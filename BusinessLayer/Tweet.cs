using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Tweet : Message // Inherits from Message
    {
        // Creates private Dictionary
        private static Dictionary<string, Tweet> tweets = new Dictionary<string, Tweet>();

        // Allows the dictionary to be accessed. Returns dictionary
        public static Dictionary<string, Tweet> GetTweet()
        {
            return tweets;
        }

        public Tweet(string header, string body) : base(header, body) // Creates Tweet with attributes
        {
            tweets.Add(header, this);
        }
    }
}
