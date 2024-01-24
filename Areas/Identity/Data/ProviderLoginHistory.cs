using Microsoft.AspNetCore.Identity;
using Provider.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Areas.Identity.Data
{
	public class ProviderLoginHistory : LoginHistory
	{
		[ForeignKey("RoleId")]
		public virtual string? RoleID { get; set; }
		public virtual IdentityRole? IdentityRole { get; set; }

		[ForeignKey("UserId")]
		public virtual string? UserID { get; set; }
	}
}
