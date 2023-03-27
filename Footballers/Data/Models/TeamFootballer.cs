using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }
    }
}
