﻿namespace ecommerce.frontend.Dtos.Customers
{
    public class CreateCustomerDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required Address Address { get; set; }
    }
}
