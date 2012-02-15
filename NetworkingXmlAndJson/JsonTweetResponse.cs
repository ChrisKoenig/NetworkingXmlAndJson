using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneApp2
{
    public class JsonTweetResponse
    {
        public JsonTweet[] results { get; set; }
    }

    public class JsonTweet
    {
        public string from_user { get; set; }

        public string profile_image_url { get; set; }

        public string text { get; set; }
    }
}