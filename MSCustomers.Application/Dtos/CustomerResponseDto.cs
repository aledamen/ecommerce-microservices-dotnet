using MSCustomers.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSCustomers.Application.Dtos
{
    public record CustomerResponseDto(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, Address Address);
}
