using Autofac;
using Autofac.Extensions.DependencyInjection;
using Headhunter.Core.Bot.Dialogs;
using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Infrastructure.Logger;
using Headhunter.Core.Bot.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.History;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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

            containerBuilder.RegisterType<ConversationLogger>()
                            .Keyed<ConversationLogger>(FiberModule.Key_DoNotSerialize)
                            .As<IActivityLogger>()
                            .InstancePerDependency();

            //containerBuilder.RegisterType<TestService>()
            //                .Keyed<ITestService>(FiberModule.Key_DoNotSerialize)
            //                .As<ITestService>();

            containerBuilder.Update(Conversation.Container);
            ApplicationContainer = Conversation.Container;
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            lifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
            app.UseMvc();
        }
    }
}
