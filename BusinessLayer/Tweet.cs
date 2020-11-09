using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Tweet : Message
    {
        private static Dictionary<string, Tweet> tweets = new Dictionary<string, Tweet>();

        public static Dictionary<string, Tweet> GetTweet()
        {
            return tweets;
        }

        public Tweet(string header, string body) : base(header, body) // Creates Tweet with attributes
        {
            tweets.Add(header, this);
        }

        private ArrayList hashtag = new ArrayList();
        private ArrayList mentions = new ArrayList();

        public ArrayList Hashtag
        {
            get { return hashtag; }
            set { hashtag = value; }
        }
        public ArrayList Mentions
        {
            get { return mentions; }
            set { mentions = value; }
        }


    }
}
