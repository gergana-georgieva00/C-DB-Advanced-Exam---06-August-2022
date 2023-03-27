using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Coach
    {
        public Coach() 
        { 
            Footballers = new HashSet<Footballer>();
        }  

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public ICollection<Footballer> Footballers { get; set; }
    }
}
