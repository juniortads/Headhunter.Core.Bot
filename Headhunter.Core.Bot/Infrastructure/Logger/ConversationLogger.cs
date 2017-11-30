using Microsoft.Bot.Builder.History;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Infrastructure.Logger
{
    public class ConversationLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            var message = activity.AsMessageActivity();

            //var a = message.GetMentions();

            //var b = message.GetStateClient();

            //var channel = message.ChannelId;
            //var clientid = message.From.Id;
            //var clientname = message.From.Name;

            Debug.WriteLine(message.Text);
        }
    }
}
