using System.ComponentModel.DataAnnotations;

namespace SportsAPI.Models
{
    public partial class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual List<Player>? Players { get; set; }
    }
}
