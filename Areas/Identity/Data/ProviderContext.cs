using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Provider.Areas.Identity.Data;
using Provider.Model;
using System;

namespace Provider.Data;

public class ProviderContext : IdentityDbContext<ProviderUser>
{
	
	public DbSet<Members> Members { get; set; }
	public DbSet<LoginHistory> LoginHistory { get; set; }
	public DbSet<RolePermissions> RolePermissions { get; set; }
	public ProviderContext(DbContextOptions<ProviderContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		// Customize the ASP.NET Identity model and override the defaults if needed.
		// For example, you can rename the ASP.NET Identity table names and more.
		// Add your customizations after calling base.OnModelCreating(builder);
	}
	public DbSet<Provider.Areas.Identity.Data.ProviderMembers>? ProviderMembers { get; set; }

	public DbSet<Provider.Areas.Identity.Data.ProviderLoginHistory>? ProviderLoginHistory { get; set; }
	public DbSet<Provider.Areas.Identity.Data.ProviderRolePermission>? ProviderRolePermission { get; set; }

}
