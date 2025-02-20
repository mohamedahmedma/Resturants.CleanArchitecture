using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Users
{
	public record CurrentUser
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
		public bool IsInRole(string role) => Roles.Contains(role);
		public string? Nationality { get; set; }
		public DateOnly? DateOfBirth {  get; set; }
		public CurrentUser(string id , string email , IEnumerable<string> roles  , string? nationality ,
            DateOnly? dateOfBirth)
		{
			Id = id;
			Email = email;
			Roles = roles;
			Nationality = nationality;
			DateOfBirth = dateOfBirth;
		}
	}
}
