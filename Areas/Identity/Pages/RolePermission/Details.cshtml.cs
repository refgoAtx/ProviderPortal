using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Provider.Areas.Identity.Data;
using Provider.Data;

namespace Provider.Areas.Identity.Pages.RolePermission
{
    public class DetailsModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public DetailsModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

      public ProviderRolePermission ProviderRolePermission { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProviderRolePermission == null)
            {
                return NotFound();
            }

            var providerrolepermission = await _context.ProviderRolePermission.FirstOrDefaultAsync(m => m.Id == id);
            if (providerrolepermission == null)
            {
                return NotFound();
            }
            else 
            {
                ProviderRolePermission = providerrolepermission;
            }
            return Page();
        }
    }
}
