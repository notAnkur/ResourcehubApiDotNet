using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ResourcehubApiDotNet.Models;

namespace ResourcehubApiDotNet
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // public Startup(IWebHostEnvironment env)
        // {
        //     var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
        //         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //         .AddEnvironmentVariables();

        //     if(env.IsDevelopment())
        //     {
        //         builder.AddUserSecrets<Startup>();
        //     }
        // }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>
                (opt => opt.UseNpgsql(Configuration["DATABASE_URL"]));
            services.AddMvc(option => option.EnableEndpointRouting=false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvc();
        }
    }
}
