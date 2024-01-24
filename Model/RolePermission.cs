using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Model
{
	public class RolePermissions
	{
		[Key]
		public int Id { get; set; }
        public string? ResourseName { get; set; }
		public Boolean Create { get; set; }
		public Boolean Read { get; set; }
		public Boolean Update { get; set; }
		public Boolean Delete { get; set; }
		[ForeignKey("RoleId")]
		public virtual string? RoleID { get; set; }
		public virtual IdentityRole? IdentityRole { get; set; }
	}
}
