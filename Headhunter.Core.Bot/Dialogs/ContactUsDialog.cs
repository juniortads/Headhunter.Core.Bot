using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Models;
using System;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class ContactUsDialog : IBaseDialogForm<Contact>
    {
        public IDialog<Contact> Build()
        {
            return new FormDialog<Contact>(new Contact(), Contact.BuildForm, FormOptions.PromptInStart);
        }

        public async Task ResumeAfter(IDialogContext context, IAwaitable<Contact> model)
        {
            var contact = await model;
        }
    }
}
