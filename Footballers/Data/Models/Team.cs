using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Team
    {
        public Team()
        {
            TeamsFootballers = new HashSet<TeamFootballer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int Trophies { get; set; }
        public ICollection<TeamFootballer> TeamsFootballers { get; set; }
    }
}
