using Autofac;
using Headhunter.Core.Bot.Infrastructure.Logging;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System;

namespace Headhunter.Core.Bot.Infrastructure.Modules
{
    public class QueueActivityModule : Module
    {
        private readonly string _queueName;
        private readonly JsonSerializerSettings _settings;
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly QueueLoggerSettings _queueLoggerSettings;

        public QueueActivityModule(CloudStorageAccount account, string queueName, QueueLoggerSettings loggerSettings = null, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentException("queue name must be provided");
            else
                _queueName = queueName;
            _queueLoggerSettings = loggerSettings ?? new QueueLoggerSettings();
            _settings = settings;
            _cloudStorageAccount = account;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            RegisterStorageQueue(builder);
            builder.RegisterInstance(_queueLoggerSettings).AsSelf().SingleInstance();

            if (_settings != null)
                builder.RegisterInstance(_settings).AsSelf().SingleInstance();

            builder.RegisterType<QueueActivityModule>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterStorageQueue(ContainerBuilder builder)
        {
            var queue = _cloudStorageAccount.CreateCloudQueueClient().GetQueueReference(_queueName);

            queue.CreateIfNotExists();

            builder.RegisterInstance(queue)
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<AzureQueueActivityLogger>().AsImplementedInterfaces();
        }
    }
}
