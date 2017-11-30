using Microsoft.Bot.Builder.FormFlow;
using System;

namespace Headhunter.Core.Bot.Models
{
    [Serializable]
    public class HumanResourcesService
    {
        [Template(TemplateUsage.EnumSelectOne, "Por favor, selecione um tipo de serviço {||}")]
        public TypeOfService TypeOfService { get; set; }
        public string PropriedadeNovaParaTeste { get; set; }

        public static IForm<HumanResourcesService> BuildForm()
        {
            return new FormBuilder<HumanResourcesService>()
                .Field(nameof(TypeOfService))
                .Field(nameof(PropriedadeNovaParaTeste))
                .Confirm("", o => false)
                .Build();
        }
    }
}
