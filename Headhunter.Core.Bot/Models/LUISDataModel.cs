namespace Headhunter.Core.Bot.Models
{
    public class LUISDataModel
    {
        public string query { get; set; }
        public topScoringIntent topScoringIntent { get; set; }
        public Entity[] entities { get; set; }
    }


    public class topScoringIntent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }
    public class Entity
    {
        public string entity { get; set; }
        public int strartIndex { get; set; }
        public int endIndex { get; set; }
        public string type { get; set; }
        public float score { get; set; }
        public Resolution resolution { get; set; }
    }

    public class Resolution
    {
        public string date { get; set; }
    }
}
