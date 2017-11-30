using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class HumanResourcesServiceDialog : IBaseDialogForm<HumanResourcesService>
    {
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

                    break;
                case TypeOfService.OutrosServicos:

                    break;
            }
        }
    }
}
