using Footballers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Footballer
    {
        public Footballer()
        {
            TeamsFootballers = new HashSet<TeamFootballer>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ContractStartDate { get; set; }
        [Required]
        public DateTime ContractEndDate { get; set; }
        [Required]
        public PositionType PositionType { get; set; }
        [Required]
        public BestSkillType BestSkillType { get; set; }
        [Required]
        public int CoachId { get; set; }
        public Coach Coach { get; set; }
        public ICollection<TeamFootballer> TeamsFootballers { get; set; }
    }
}
