using Headhunter.Core.Bot.Models;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Services
{
    public interface IIntentFromLuisService
    {
        Task<LUISDataModel> Get(string query);
    }
}
