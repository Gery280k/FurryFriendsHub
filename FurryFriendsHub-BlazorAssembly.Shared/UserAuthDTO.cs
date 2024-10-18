using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurryFriendsHub_BlazorAssembly.Shared
{
	public class UserAuthDTO
	{
		public string Email { get; set; }
		public string passwordHash { get; set; }
	}
}
