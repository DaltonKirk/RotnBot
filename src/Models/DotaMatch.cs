namespace RotnBot.Models
{
    public class DotaMatch
    {
        public string kills { get; set; }
        public string deaths { get; set; }
        public string assists { get; set; }
        public int player_slot { get; set; }
        public bool radiant_win { get; set; }
        public double start_time { get; set; }
        public int hero_id { get; set; }
    }
}