using Headhunter.Core.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class HumanResourcesServiceDialog
    {
        public IDialog<HumanResourcesService> Maker()
        {
            return new FormDialog<HumanResourcesService>(new HumanResourcesService(), HumanResourcesService.BuildForm, FormOptions.PromptInStart);
        }

        public async Task AfterContinuation(IDialogContext context, IAwaitable<HumanResourcesService> result)
        {
            var humanResourcesService = await result;
            
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
