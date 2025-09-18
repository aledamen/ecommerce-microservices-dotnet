using Kernel;
using MSCustomers.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MSCustomers.Domain.Entities
{
    public class Customer: BaseEntity
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [MaxLength(50, ErrorMessage = "Customer FirstName max length is 50")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Customer Last Name is required")]
        [MaxLength(50, ErrorMessage = "Customer LastName max length is 50")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Customer Email is required")]
        [EmailAddress(ErrorMessage = "Customer Email must be a valid format")]
        public required string Email { get; set; }

        [Phone(ErrorMessage = "Customer Phone must be a valid format")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Customer Address is required")]
        public required Address Address { get; set; }


        public void UpdateDetails(string? firstName, string? lastName, string email, string phoneNumber, Address address)
        {
            bool updated = false;

            if (!string.IsNullOrWhiteSpace(firstName) && FirstName != firstName)
            {
                FirstName = firstName;
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(lastName) && LastName != lastName)
            {
                LastName = lastName;
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(email) && Email != email)
            {
                Email = email;
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber) && PhoneNumber != phoneNumber)
            {
                PhoneNumber = phoneNumber;
                updated = true;
            }

            if (address != null && Address != address)
            {
                if (!string.IsNullOrWhiteSpace(address.Street))
                    Address.Street = address.Street;
                if (!string.IsNullOrWhiteSpace(address.City))
                    Address.City = address.City;
                if (address.Number > 0)
                    Address.Number = address.Number;

                updated = true;
            }

            if (updated)
                MarkUpdated();
        }
    }
}
