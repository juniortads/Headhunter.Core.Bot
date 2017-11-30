using Headhunter.Core.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs.Interfaces
{
    public interface IBaseDialogCard : IDialog<DialogResponse>
    {
        Task MessageReceivedAsyncCardAction(IDialogContext context, IAwaitable<IMessageActivity> result);
        Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result);
        Task MessageResumeAfter(IDialogContext context, IAwaitable<DialogResponse> result);
        IList<Attachment> GetCardsAttachments();
    }
}
