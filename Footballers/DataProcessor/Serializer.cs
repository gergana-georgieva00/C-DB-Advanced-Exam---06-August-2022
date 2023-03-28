namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ExportDto;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            ExportXmlCoach[] coachesDto = context
                .Coaches
                .Where(c => c.Footballers.Any())
                .ToArray()
                .Select(c => new ExportXmlCoach()
                {
                    FootballersCount = c.Footballers.Count(),
                    CoachName = c.Name,
                    Footballers = c.Footballers.Select(f => new ExportFootballerDto
                    {
                        Name = f.Name,
                        Position = f.PositionType.ToString()
                    })
                    .OrderBy(f => f.Name)
                    .ToArray()
                })
                .OrderByDescending(c => c.Footballers.Count())
                .ThenBy(c => c.CoachName)
                .ToArray();

            return Serialize<ExportXmlCoach[]>(coachesDto, "Coaches");
        }

        private static string Serialize<T>(T dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            StringBuilder sb = new StringBuilder();
            using var write = new StringWriter(sb);

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(write, dataTransferObjects, xmlNamespaces);

            return sb.ToString();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var startDate = DateTime.TryParseExact(date.ToString(), "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validStartDate);

            var teams = context.Teams
                .Where(t => t.TeamsFootballers
                .Any(tf => tf.Footballer.ContractStartDate >= validStartDate))
                .ToArray()
                .Select(t => new
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                    .Where(tf => tf.Footballer.ContractStartDate >= validStartDate)
                    .Select(tf => new
                    {
                        Name = tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate,
                        ContractEndDate = tf.Footballer.ContractEndDate,
                        BestSkillType = tf.Footballer.BestSkillType.ToString("d"),
                        PositionType = tf.Footballer.PositionType.ToString("d")
                    })
                    .OrderByDescending(f => f.ContractEndDate)
                    .ThenBy(f => f.Name)
                })
                .OrderByDescending(t => t.Footballers.Count())
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
