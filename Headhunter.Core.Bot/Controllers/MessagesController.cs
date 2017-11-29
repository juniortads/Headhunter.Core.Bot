using Headhunter.Core.Bot.Dialogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly GreetingDialog greetingDialog;

        public MessagesController(GreetingDialog greetingDialog)
        {
            this.greetingDialog = greetingDialog;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => this.greetingDialog);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
            }
            else if (message.Type == ActivityTypes.Typing)
            {
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
            return null;
        }
    }
}
