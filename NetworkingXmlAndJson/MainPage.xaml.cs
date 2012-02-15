using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string RSS_URL = "http://search.twitter.com/search.rss?q=windowsphone&show_user=true&include_entities=true";
            string JSON_URL = "http://search.twitter.com/search.json?q=windowsphone&show_user=true&include_entities=true";

            Uri uri = new Uri(RSS_URL, UriKind.Absolute);
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (source, args) =>
            {
                ItemsListBox.ItemsSource = GetTwitterItemsFromRSS(args.Result);
                // ItemsListBox.ItemsSource = GetTwitterItemsFromJSON(args.Result);
            };
            client.DownloadStringAsync(uri);

            //HttpWebRequest request = HttpWebRequest.Create(JSON_URL) as HttpWebRequest;
            //request.BeginGetResponse((iar) =>
            //{
            //    string data;
            //    var response = request.EndGetResponse(iar);
            //    using (var stream = response.GetResponseStream())
            //    using (var reader = new System.IO.StreamReader(stream))
            //        data = reader.ReadToEnd();
            //    var items = GetTwitterItemsFromJSON(data);
            //    Dispatcher.BeginInvoke(() => ItemsListBox.ItemsSource = items);
            //}, null);
        }

        private IEnumerable<Tweet> GetTwitterItemsFromRSS(string p)
        {
            XNamespace google = "http://base.google.com/ns/1.0";
            XDocument doc = XDocument.Parse(p);
            var tweets = from tweet in doc.Root.Element("channel").Elements("item")
                         select new Tweet()
                         {
                             Text = tweet.Element("title").Value,
                             ImageUrl = tweet.Element(google + "image_link").Value,
                         };
            return tweets;
        }

        private IEnumerable<Tweet> GetTwitterItemsFromJSON(string p)
        {
            var response = JsonConvert.DeserializeObject<JsonTweetResponse>(p);
            var tweets = from tweet in response.results
                         select new Tweet()
                         {
                             ImageUrl = tweet.profile_image_url,
                             Text = String.Format("{0}: {1}", tweet.from_user, tweet.text),
                         };
            return tweets;
        }
    }
}