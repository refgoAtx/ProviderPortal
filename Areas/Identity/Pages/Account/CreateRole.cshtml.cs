using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using Provider.Areas.Identity.Data;
using Provider.Model;
using System;
using System.Text.Json;

namespace Provider.Areas.Identity.Pages.Account
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Provider.Data.ProviderContext _context;
        public CreateRoleModel(RoleManager<IdentityRole> roleManager , Provider.Data.ProviderContext context)
        {
            this.roleManager = roleManager;
            _context = context;
        }
        [BindProperty]
        public ProviderRolePermission ProviderRolePermissiond { get; set; } = default!;
        public IList<ProviderRolePermission> ProviderRolePermission { get; set; } = default!;
        public async Task OnGet(int? id)
        {
            if (_context.ProviderRolePermission != null)
            {
                ProviderRolePermission = await _context.ProviderRolePermission.ToListAsync();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnPostSend(string name)
        {
            PersonModel person = new PersonModel
            {
                Name = name,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(person);
        }
        //public async Task<IActionResult> OnPost(string Rolename )
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityRole role = new IdentityRole();
        //        {
        //            role.Name = Rolename;
        //        }
        //        IdentityResult result = await roleManager.CreateAsync(role);
        //        if (result.Succeeded)
        //        {
        //            return Page();
        //        }
        //    }
        //    return Page();
        //}
        public class PersonModel
        {
            ///<summary>
            /// Gets or sets Name.
            ///</summary>
            public string Name { get; set; }

            ///<summary>
            /// Gets or sets DateTime.
            ///</summary>
            public string DateTime { get; set; }
        }
        public class Person
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
        }

        public struct myStruct
        {
            public string name { get; set; }
            public int age { get; set; }
            public string location { get; set; }
        }   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnPostCreStudent([FromBody] Rootstring artist)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            Root? resultss = JsonConvert.DeserializeObject<Root>(Convert.ToString(artist));
            PersonModel person = new PersonModel
            {
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(person);
        }

    }
}
