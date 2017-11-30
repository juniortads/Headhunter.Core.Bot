using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs.Interfaces
{
    public interface IBaseDialogForm<TModel>
    {
        IDialog<TModel> Build();
        Task ResumeAfter(IDialogContext context, IAwaitable<TModel> model);
    }
}
