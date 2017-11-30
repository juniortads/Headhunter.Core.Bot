using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Headhunter.Core.Bot.Dialogs.Interfaces;
using Headhunter.Core.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Headhunter.Core.Bot.Dialogs
{
    [Serializable]
    public class CareersDialog : IBaseDialogCard
    {

        /// <summary>
        /// 1
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        /// <summary>
        /// 2
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachments();

            await context.PostAsync(reply);
            context.Wait(this.MessageReceivedAsyncCardAction);
        }
        /// <summary>
        /// 3 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task MessageReceivedAsyncCardAction(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Contains("button_1"))
            {
                await context.PostAsync($"Estamos processando o pedido do button {message.Text}");

                var response = new DialogResponse
                {
                    Data = Guid.NewGuid().ToString(),
                    Message = "Pedido realizado com sucesso!!!"
                };
                context.Done(response);
            }
        }
        /// <summary>
        /// acabou tudo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task MessageResumeAfter(IDialogContext context, IAwaitable<DialogResponse> result)
        {
            var message = await result;
            await context.PostAsync($":) {message.Message} {message.Data}");
        }

        public IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Analista Programador PHP",
                    "Atuação no desenvolvimento de um sistema estratégico.",
                    "Em um cliente do seguimento de seguros, equipe Sênior, com profissionais acima dos 35 anos, mais reservados e bastante tecnológicos. Metodologia Scrum. Necessidades técnicas: Experiência com AngularJS, Phonegap ou ReactJS; Experiência em frameworks PHP MVC: Preferencialmente CakePHP",
                    new CardImage(url: "https://upload.wikimedia.org/wikipedia/commons/thumb/2/27/PHP-logo.svg/1200px-PHP-logo.svg.png"),
                    new CardAction(ActionTypes.ImBack, "Learn more", value: "button_1")),
                GetHeroCard(
                    "Analista de Suporte a Servidores",
                    "Trata-se de uma alocação no cliente para atuar junto com a equipe de InfraEstrutura dentro das atividades diárias de atendimento de chamados e execução de projetos (quando houver)",
                    "",
                    new CardImage(url: "https://www.yogh.com.br/blog/wp-content/uploads/2016/04/Super-Servidor.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/functions/"))
            };
        }
        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
    }
}
