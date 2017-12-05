using Microsoft.Bot.Builder.History;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Bot.Builder.Azure;
using System.Text;
using System;

namespace Headhunter.Core.Bot.Infrastructure.Logging
{
    public class AzureQueueActivityLogger : IActivityLogger
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly CloudQueue _cloudQueue;
        private readonly QueueLoggerSettings _queueLoggerSettings;

        private readonly float _cutCoefficient;

        public AzureQueueActivityLogger(CloudQueue cloudQueue, QueueLoggerSettings queueSettings = null, JsonSerializerSettings settings = null)
        {
            _queueLoggerSettings = queueSettings ?? new QueueLoggerSettings();
            _jsonSerializerSettings = settings;
            _cutCoefficient = 1 - _queueLoggerSettings.MessageTrimRate;
            _cloudQueue = cloudQueue;
        }

        public async Task LogAsync(IActivity activity)
        {
            var message = activity.AsMessageActivity();

            var jsonMsg = JsonConvert.SerializeObject(message, _jsonSerializerSettings);
            var bytes = GetBytes(jsonMsg);

            if (_queueLoggerSettings.OverflowHanding == LargeMessageMode.Discard)
            {
                //if fails, do not do anything....
                try
                {
                    await _cloudQueue.AddMessageAsync(new CloudQueueMessage(bytes));
                }
                catch
                {
                    // ignored
                }
            }
            else if (_queueLoggerSettings.OverflowHanding == LargeMessageMode.Error)
            {
                //let it fail                    
                await _cloudQueue.AddMessageAsync(new CloudQueueMessage(bytes));
            }
            else if (_queueLoggerSettings.OverflowHanding == LargeMessageMode.Trim)
            {
                do
                {
                    try
                    {
                        await _cloudQueue.AddMessageAsync(new CloudQueueMessage(bytes));
                        return;
                    }
                    catch (Exception)
                    {
                        //cut off some of the text to fit
                        message.Text = message.Text.Substring(0, (int)(message.Text.Length * _cutCoefficient));
                        jsonMsg = JsonConvert.SerializeObject(message, _jsonSerializerSettings);
                        bytes = GetBytes(jsonMsg);
                    }
                } while (true);
            }
        }

        private byte[] GetBytes(string message)
        {
            return _queueLoggerSettings.CompressMessage ? message.Compress() : Encoding.UTF8.GetBytes(message);
        }
    }
}
