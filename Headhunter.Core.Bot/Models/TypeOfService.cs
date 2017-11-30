using Microsoft.Bot.Builder.FormFlow;

namespace Headhunter.Core.Bot.Models
{
    public enum TypeOfService
    {
        [Describe("Carreiras")]
        Carreiras = 1,
        [Describe("Outros Servicos")]
        OutrosServicos = 2
    }
}
