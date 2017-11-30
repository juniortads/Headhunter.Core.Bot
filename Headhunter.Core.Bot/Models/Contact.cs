using Microsoft.Bot.Builder.FormFlow;
using System;

namespace Headhunter.Core.Bot.Models
{
    [Serializable]
    public class Contact
    {
        [Template(TemplateUsage.String, "Por favor, escreva a sua mensagem {||}")]
        public string Message { get; set; }

        [Template(TemplateUsage.String, "Agora, informe telefone Exemplo: (11)1234-5678 {||}")]
        [Pattern(@"^([0-9]{2})?(\([0-9]{2})\)([0-9]{3}|[0-9]{4}|[0-9]{5})-[0-9]{4}$")]
        public string Phone { get; set; }

        public static IForm<Contact> BuildForm()
        {
            return new FormBuilder<Contact>()
                .Build();
        }
    }
}
