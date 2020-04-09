using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Web;

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

                return qs["screen_name"];
            } else
            {
                return "Auth error: Couldn't get oauth token";
            }
        }
    }
}