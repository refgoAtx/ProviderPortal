using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Provider.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ProviderUser class
public class ProviderUser : IdentityUser
{

    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [ForeignKey("RoleId")]
    public virtual string? RoleID { get; set; }
    public virtual IdentityRole? IdentityRole { get; set; }

	public string? Ip { get; set; }
	public string? Browser { get; set; }
}

