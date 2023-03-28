namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            throw new NotImplementedException();
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
