﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entites
{
	public class User : IdentityUser
	{
		public DateOnly? DateOfBirth {  get; set; }
		public string? Nationality { get; set; }

		public List<Restaurants> OwendRestaurants { get; set; } = [];

	}
}
