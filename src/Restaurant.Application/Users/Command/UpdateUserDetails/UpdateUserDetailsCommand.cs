using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Users.Command.UpdateUserDetails
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? DateBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
