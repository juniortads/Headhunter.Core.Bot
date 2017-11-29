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
            Debug.WriteLine(message.Text);
        }
    }
}
