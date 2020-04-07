using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ResourcehubApiDotNet.Utils;
using System;

namespace ResourcehubApiDotNet.Controllers.Auth
{

    [ApiController]
    public class TwitterController : Controller
    {

        public TwitterController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
        public ActionResult GetTwitterAuthCallback()
        {
            return Json(new { test = "yolo"});
        }
    }
}