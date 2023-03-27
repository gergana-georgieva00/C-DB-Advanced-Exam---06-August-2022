using Footballers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Client")]
    public class ImportXmlCoach
    {
        [XmlElement("Name")]
        [Required, MinLength(2), MaxLength(40)]
        public string Name { get; set; }
        [XmlElement("Nationality")]
        [Required]
        public string Nationality { get; set; }
        public List<Footballer> Footballers { get; set; }
    }
}
