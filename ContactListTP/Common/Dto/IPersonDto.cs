namespace Common.Dto
{
    public interface IPersonDto
    {
        /// <summary>
        ///     Identification of contact
        /// </summary>
        string ResourceName { get; }

        string FirstName { get; }

        string LastName { get; }

        string PhoneNumber { get; }

        string EmailAddress { get; }

        string PhotoUrl { get; }

        string BirthDayFormatted { get; }

        string Address { get; }
    }
}