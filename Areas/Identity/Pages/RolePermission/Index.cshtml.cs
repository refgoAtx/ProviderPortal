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
    public class IndexModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public IndexModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

        public IList<ProviderRolePermission> ProviderRolePermission { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ProviderRolePermission != null)
            {
                ProviderRolePermission = await _context.ProviderRolePermission.ToListAsync();
            }
        }
    }
}
