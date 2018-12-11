using Common.Extensions;
using Google.Apis.PeopleService.v1.Data;

namespace Common.Converters
{
    /// <summary>
    /// Expected date format is dd/mm/yyyy
    /// </summary>
    public class DateConverter : IConverter<Date, string>
    {
        public Date Convert(string source)
        {
            var spited = source.Split('/');
            return new Date
            {
                Day = spited.Length > 0 ? spited[0].ParseIntOrNull() : null,
                Month = spited.Length > 1 ? spited[1].ParseIntOrNull() : null,
                Year = spited.Length > 2 ? spited[2].ParseIntOrNull() : null
            };
        }

        public string Convert(Date source)
        {
            return $"{source.Day}" + (source.Month == null ? "" : $"/{source.Month}") + (source.Year == null ? "" : $"/{source.Year}");
        }
    }
}