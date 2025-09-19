using System.ComponentModel.DataAnnotations;

namespace MSOrders.Domain.ValueObjects
{
    public class ShippingAddress
    {
        [Required(ErrorMessage = "Address Street is required")]
        [MaxLength(100, ErrorMessage = "Street max length is 100")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "Address City is required")]
        [MaxLength(50, ErrorMessage = "City max length is 50")]
        public required string City { get; set; }

        [Required(ErrorMessage = "Address Number is required")]
        [Range(0, 9999999, ErrorMessage = "Number must be between 0 and 9999999")]
        public int Number { get; set; }
    }
}
