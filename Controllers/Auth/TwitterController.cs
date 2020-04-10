using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ResourcehubApiDotNet.Utils;
using System;
using ResourcehubApiDotNet.Models;
using System.Web;

namespace ResourcehubApiDotNet.Controllers.Auth
{

    [ApiController]
    public class TwitterController : Controller
    {

        public IConfiguration Configuration { get; }
        private readonly DatabaseContext _databaseContext;
        public TwitterController(IConfiguration configuration, DatabaseContext databaseContext)
        {
            Configuration = configuration;
            _databaseContext = databaseContext;
        }

        [Route("auth/[controller]")]
        [HttpGet]
        public ActionResult GetTwitterAuth()
        {
            Console.WriteLine(Configuration["DATABASE_URL"]);
            return Json(new { authType = "Twitter" });
        }

        [Route("auth/[controller]/login")]
        [HttpGet]
        public ActionResult GetTwitterAuthLogin()
        {
            var twitterUtils = new TwitterUtils();
            string twitterRedirectUrl = Configuration["REDIRECT_URL_BASE"] + "/twitter/callback";
            Console.WriteLine("twitter URL -> "+twitterRedirectUrl);
            Console.WriteLine(Configuration["ACCESS_KEY"]);

            var url = twitterUtils.GetRequestToken(
                Configuration["API_KEY"],
                Configuration["API_KEY_SECRET"],
                Configuration["ACCESS_KEY"],
                Configuration["ACCESS_KEY_SECRET"],
                twitterRedirectUrl
            );

            return Redirect(url);
        }

        [Route("auth/[controller]/callback")]
        [HttpGet]
        public ActionResult GetTwitterAuthCallback(string oauth_token, string oauth_verifier)
        {
            TwitterUtils twitterUtils = new TwitterUtils();

            string accessTokenResponse = twitterUtils.GetAccessToken(
                Configuration["API_KEY"],
                Configuration["API_KEY_SECRET"],
                oauth_token,
                "",
                oauth_verifier
            );
            var accessTokenQueryString = HttpUtility.ParseQueryString(accessTokenResponse);

            // TODO: check and save user in the db

            var user = twitterUtils.GetUser(
                accessTokenQueryString["screen_name"],
                Configuration["API_KEY"],
                Configuration["API_KEY_SECRET"]
            );

            // TODO: Redirect to frontend along with jwt
            return Json(new { test = "Sup "+accessTokenQueryString["screen_name"]});
        }
    }
}