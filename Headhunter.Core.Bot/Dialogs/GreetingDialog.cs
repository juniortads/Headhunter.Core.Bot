using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog<IMessageActivity>
    {
        const string key_client_user_name = "Name";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Olá, eu sou John Bot");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> messageActivity)
        {
            var message = await messageActivity;
            var userName = string.Empty;
            bool calledMethodReturnName = false;

            context.UserData.TryGetValue<bool>("GetName", out calledMethodReturnName);
            userName = GetUserName(context);

            if (calledMethodReturnName)
            {
                userName = message.Text;
                SetUserName(context, userName);
            }

            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Quero poder conhecer você melhor, qual é o seu nome?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync($"Oi {userName}. Como eu posso te ajudar hoje?");
            }
            context.Wait(MessageReceivedAsync);
        }

        private static void SetUserName(IDialogContext context, string value)
        {
            context.UserData.SetValue<string>(key_client_user_name, value);
        }

        private static string GetUserName(IDialogContext context)
        {
            var userName = String.Empty;
            context.UserData.TryGetValue<string>(key_client_user_name, out userName);
            return userName;
        }
    }
}
