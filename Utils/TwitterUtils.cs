using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Web;
using Newtonsoft.Json;

namespace ResourcehubApiDotNet.Utils
{
    public class TwitterUtils
    {
        public string GetRequestToken(string apiKey, string apiKeySecret, string accessKey, string accessKeySecret, string callBackUrl)
        {
            var client = new RestClient("https://api.twitter.com")
            {
                Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForProtectedResource(
                    apiKey, 
                    apiKeySecret, 
                    accessKey, 
                    accessKeySecret
                )
            };
            var request = new RestRequest("/oauth/request_token", Method.POST);
            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            var qs = HttpUtility.ParseQueryString(response.Content);

            Console.WriteLine(response);

            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];

            request = new RestRequest("oauth/authorize?oauth_token=" + oauthToken);
            string url = client.BuildUri(request).ToString();

            return url;
        }

        public string GetAccessToken(string key, string secret, string oauthToken, string oauthTokenSecret, string oauthVerifier)
        {
            var client = new RestClient("https://api.twitter.com");

            var request = new RestRequest("/oauth/access_token", Method.POST);

            client.Authenticator = OAuth1Authenticator.ForAccessToken(key, secret, oauthToken, oauthTokenSecret, oauthVerifier);

            var response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var qs = HttpUtility.ParseQueryString(response.Content);
                Console.WriteLine(qs);

                return response.Content;
            } else
            {
                return "Auth error: Couldn't get oauth token";
            }
        }

        // fetch user profile from twitter
        public Object GetUser(string twitterScreenName, string apiKey, string apiKeySecret)
        {
            var client = new RestClient("https://api.twitter.com");
            var request = new RestRequest($"/1.1/users/show.json?screen_name={twitterScreenName}", Method.GET);
            // request at "/users/show.json" only requires CONSUMER KEY(apiKey) and CONSUMER SECRET(apiKeySecret)
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(apiKey, apiKeySecret, "", "");
            var response = client.Execute(request);
            // var qStr = new JavaScript
            // Console.WriteLine(response.Content);
            TwitterUser twitterUser = JsonConvert.DeserializeObject<TwitterUser>(response.Content);
            Console.WriteLine(twitterUser);
            Console.WriteLine(twitterUser.name);
            Console.WriteLine(twitterUser.profile_image_url_https);
            return response.Content;
        }

    }

    public class TwitterUser {
        public string id_str { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public string profile_image_url_https { get; set; }
    }

}