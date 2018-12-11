using Common.Dto;
using Google.Apis.PeopleService.v1.Data;

namespace Common.Converters
{
    public static class Extensions
    {
        public static string FormatToString(this Date date) => new DateConverter().Convert(date);
        
        public static Date ParseDate(this string date) => new DateConverter().Convert(date);

        public static Person ToGoogleObject(this IPersonDto dto) => new PersonDtoPersonConverter().Convert(dto);
        
        public static IPersonDto ToDto(this Person person) => new PersonDtoPersonConverter().Convert(person);
    }
}