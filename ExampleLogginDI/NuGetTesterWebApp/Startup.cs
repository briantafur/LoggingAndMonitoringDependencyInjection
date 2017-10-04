using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Logger.Services;

namespace NuGetTesterWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Add the Logger services

            //Reading the key for azure in the appsettings.json
            //var key = Convert.ToString(Configuration.GetSection("AzureKey").GetValue<String>("App_Insights_Logging_Key"));
            //services.AddTransient<LoggerInterface, AppInsight>((_) => new AppInsight(key));

            //Reading the route for the log in the appsettings.json
            //var ruta = Convert.ToString(Configuration.GetSection("LogRoute").GetValue<String>("LocalRoute"));
            //services.AddTransient<ILoggerInterface, Logger.Services.Serilog>((_) => new Logger.Services.Serilog(ruta));
            //services.AddTransient<ILoggerInterface, Log4Net>((_) => new Log4Net(ruta));

            //Reading values to azure blob storage from appsettings.json
            /*var storageAccountName = Configuration.GetSection("AzureStorage").GetValue<String>("Storage_Account_Name");
            var azureKey = Configuration.GetSection("AzureStorage").GetValue<String>("Azure_Storage_Key");
            var containerName = Configuration.GetSection("AzureStorage").GetValue<String>("Container_Name");
            var fileSize = Configuration.GetSection("AzureStorage").GetValue<float>("File_Size");
            services.AddTransient<ILoggerInterface, AzureBlobStorage>((_) => new AzureBlobStorage(storageAccountName, azureKey, containerName, fileSize));*/

            //Reading values to amazon s3 from appsettings.json
            services.AddTransient<ILoggerInterface, AmazonS3Storage>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
