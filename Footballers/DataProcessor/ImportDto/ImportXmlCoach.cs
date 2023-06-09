﻿using Footballers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportXmlCoach
    {
        [XmlElement("Name")]
        [Required, MinLength(2), MaxLength(40)]
        public string Name { get; set; }
        [XmlElement("Nationality")]
        [Required]
        public string Nationality { get; set; }
        [XmlArray("Footballers")]
        public FootballerDto[] Footballers { get; set; }
    }

    [XmlType("Footballer")]
    public class FootballerDto
    {
        [XmlElement("Name")]
        [Required, MinLength(2), MaxLength(40)]
        public string Name { get; set; }
        [XmlElement("ContractStartDate")]
        [Required]
        public string ContractStartDate { get; set; }
        [XmlElement("ContractEndDate")]
        [Required]
        public string ContractEndDate { get; set; }
        [XmlElement("BestSkillType")]
        [Required, Range(0, 4)]
        public int BestSkillType { get; set; }
        [XmlElement("PositionType")]
        [Required, Range(0, 3)]
        public int PositionType { get; set; }
    }
}
