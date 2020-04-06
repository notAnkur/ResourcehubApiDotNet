using System;
using RestSharp;
using RestSharp.Authenticators;

namespace ResourcehubApiDotNet.Utils
{
    public class TwitterUtils
    {
        public string GetRequestToken(string access_key, string access_key_secret, string callBackUrl)
        {
            var client = new RestClient("https://api.twitter.com")
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(access_key, access_key_secret, callBackUrl)
            };
            var request = new RestRequest("/oauth/request_token", Method.POST);
            var response = client.Execute(request);

            var qs = System.Web.HttpUtility.ParseQueryString(response.Content);

            Console.WriteLine(response);

            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];

            request = new RestRequest("oauth/authorize?oauth_token=" + oauthToken);
            string url = client.BuildUri(request).ToString();

            return url;
        }
    }
}