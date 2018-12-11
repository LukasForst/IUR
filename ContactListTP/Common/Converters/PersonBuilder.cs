using System.Collections.Generic;
using Common.Dto;
using Common.Extensions;
using Google.Apis.PeopleService.v1.Data;

namespace Common.Converters
{
    public class PersonBuilder
    {
        private readonly IPersonDto dto;
        private readonly Person person;

        private PersonBuilder(IPersonDto dto)
        {
            this.dto = dto;
            person = new Person();
        }

        public static Person Build(IPersonDto dto)
        {
            return new PersonBuilder(dto)
                .SetName()
                .SetPhoneNumber()
                .SetEmailAddress()
                .SetBirthDay()
                .SetAddress()
                .person;
        }

        private PersonBuilder SetName()
        {
            var name = new Name();
            var updated = false;
            if (!dto.FirstName.IsNullOrEmpty())
            {
                name.GivenName = dto.FirstName;
                updated = true;
            }

            if (!dto.LastName.IsNullOrEmpty())
            {
                name.FamilyName = dto.LastName;
                updated = true;
            }

            if (updated)
            {
                person.Names = new List<Name> {name};
            }

            return this;
        }

        private PersonBuilder SetPhoneNumber()
        {
            if (!dto.PhoneNumber.IsNullOrEmpty())
            {
                var phoneNumber = new PhoneNumber {Value = dto.PhoneNumber};
                person.PhoneNumbers = new List<PhoneNumber> {phoneNumber};
            }

            return this;
        }

        private PersonBuilder SetEmailAddress()
        {
            if (!dto.EmailAddress.IsNullOrEmpty())
            {
                var value = new EmailAddress {Value = dto.PhoneNumber};
                person.EmailAddresses = new List<EmailAddress> {value};
            }

            return this;
        }

        private PersonBuilder SetBirthDay()
        {
            if (!dto.BirthDayFormated.IsNullOrEmpty())
            {
                var value = new Birthday {Date = dto.BirthDayFormated.ParseDate()};
                person.Birthdays = new List<Birthday> {value};
            }

            return this;
        }

        private PersonBuilder SetAddress()
        {
            if (!dto.Address.IsNullOrEmpty())
            {
                var value = new Address {FormattedValue = dto.Address};
                person.Addresses = new List<Address> {value};
            }

            return this;
        }
    }
}