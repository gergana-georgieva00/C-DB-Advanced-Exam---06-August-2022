namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            var coachDtos = Deserialize<ImportXmlCoach[]>(xmlString, "Coaches");
            var sb = new StringBuilder();
            var coaches = new List<Coach>();

            foreach (var dto in coachDtos)
            {
                if (!IsValid(dto) || string.IsNullOrEmpty(dto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var coach = new Coach()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality
                };

                var footballers = new List<Footballer>();
                foreach (var footballerDto in dto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var footballer = new Footballer()
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = footballerDto.ContractStartDate,
                        ContractEndDate = footballerDto.ContractEndDate,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType
                    };

                    footballers.Add(footballer);
                }

                coach.Footballers = footballers;
                coaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count()));
            }

            context.AddRange(coaches);
            context.SaveChanges();

            return sb.ToString();
        }

        private static T Deserialize<T>(string inputXml, string rootName)
        {
            var root = new XmlRootAttribute(rootName);
            var serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
