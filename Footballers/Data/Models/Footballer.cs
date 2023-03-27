using Footballers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data.Models
{
    public class Footballer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public PositionType Position { get; set; }
        public BestSkillType BestSkill { get; set; }
    }
}
