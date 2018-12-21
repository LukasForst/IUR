namespace Common.Dto
{
    public interface IPersonDto
    {
        string FirstName { get; }

        string LastName { get; }

        string PhoneNumber { get; }

        string EmailAddress { get; }

        string PhotoUrl { get; }

        string BirthDayFormatted { get; }

        string Address { get; }

        IGoogleId GoogleId { get; }
    }
}