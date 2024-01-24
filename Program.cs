using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Provider.Areas.Identity.Data;
using Provider.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProviderContextConnection") ?? throw new InvalidOperationException("Connection string 'ProviderContextConnection' not found.");
ConfigurationManager configuration = builder.Configuration;


object value = builder.Services.AddDbContext<ProviderContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ProviderUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProviderContext>();

builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties†.
    //options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "XSRF-TOKEN";
    options.SuppressXFrameOptionsHeader = false;

});
// Add services to the container.   
builder.Services.AddRazorPages();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
//builder.Services.AddMvc().AddRazorPagesOptions(options =>
//    {
//        options.Conventions.AddPageRoute("/Account/Login", "");
//    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseForwardedHeaders();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
