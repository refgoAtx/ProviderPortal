using Microsoft.AspNetCore.Identity;
using Provider.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Model
{
	public class LoginHistory
	{
		[Key]
		public int Id { get; set; }
		public string? Browser { get; set; }
		public string? Ip { get; set; }
        public DateTime LoginTime { get; set; }

        public DateTime Createdat { get; set; }
		public string? Createdby { get; set; }
		public DateTime Updatedat { get; set; }
		public string? Updatedby { get; set; }
		//public string? UserId { get; set; }
		////[ForeignKey("UserId")]
		////public virtual IdentityUser? ProviderUser { get; set; }
	}
}
