using Autofac;
using Autofac.Extensions.DependencyInjection;
using Headhunter.Core.Bot.Dialogs;
using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Infrastructure.Modules;
using Headhunter.Core.Bot.Models;
using Headhunter.Core.Bot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Reflection;

namespace Headhunter.Core.Bot
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
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<GreetingDialog>()
                            .InstancePerDependency();

            containerBuilder.RegisterType<HumanResourcesServiceDialog>()
                            .As<IBaseDialogForm<HumanResourcesService>>()
                            .InstancePerDependency();

            containerBuilder.RegisterType<ContactUsDialog>()
                            .As<IBaseDialogForm<Contact>>()
                            .InstancePerDependency();

            containerBuilder.RegisterType<CareersDialog>()
                            .As<IBaseDialogCard>()
                            .InstancePerDependency();

            containerBuilder.RegisterType<IntentFromLuisService>()
                            .As<IIntentFromLuisService>()
                            .InstancePerDependency();
            
            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));

                var uriServer = new Uri(Configuration["AzureModule:DocumentDb:Endpoint"]);

                var storeDocument = new DocumentDbBotDataStore(uriServer,
                    Configuration["AzureModule:DocumentDb:Key"],
                    "HeadhunterCoreBot",
                    "_logs");

                builder.Register(c => storeDocument)
                                .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                                .AsSelf()
                                .SingleInstance();

                var storageAccount = CloudStorageAccount.Parse(Configuration["AzureModule:CloudStorageAccount:ConnectionString"]);

                builder.RegisterModule(new QueueActivityModule(storageAccount, "activity-logger"));
            });

            containerBuilder.Update(Conversation.Container);
            ApplicationContainer = Conversation.Container;
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            lifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

            app.UseMvcWithDefaultRoute();
        }
    }
}
