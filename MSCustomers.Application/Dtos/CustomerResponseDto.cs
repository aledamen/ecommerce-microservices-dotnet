using MSCustomers.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCustomers.Application.Dtos
{
    public record CustomerResponseDto(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, Address Address);
}
