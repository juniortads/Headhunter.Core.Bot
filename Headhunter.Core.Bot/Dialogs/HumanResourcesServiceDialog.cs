using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class HumanResourcesServiceDialog : IBaseDialogForm<HumanResourcesService>
    {
        private readonly IBaseDialogForm<Contact> contactDialogForm;
        private readonly IBaseDialogCard careersDialog;

        public HumanResourcesServiceDialog(
            IBaseDialogForm<Contact> contactDialogForm,
            IBaseDialogCard careersDialog)
        {
            this.contactDialogForm = contactDialogForm;
            this.careersDialog = careersDialog;
        }

        public IDialog<HumanResourcesService> Build()
        {
            return new FormDialog<HumanResourcesService>(new HumanResourcesService(), HumanResourcesService.BuildForm, FormOptions.PromptInStart);
        }

        public async Task ResumeAfter(IDialogContext context, IAwaitable<HumanResourcesService> model)
        {
            var humanResourcesService = await model;

            switch (humanResourcesService.TypeOfService)
            {
                case TypeOfService.Carreiras:
                    await context.Forward(
                            this.careersDialog,
                            new ResumeAfter<DialogResponse>(careersDialog.MessageResumeAfter),
                            model,
                            CancellationToken.None);
                    break;
                case TypeOfService.FaleConosco:
                    context.Call(contactDialogForm.Build(), contactDialogForm.ResumeAfter);
                    break;
            }
        }
    }
}
