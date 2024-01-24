using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Model;

namespace Provider.Areas.Identity.Pages.UserManagment
{
    public class IndexModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public IndexModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

        public IList<Members> Members { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Members != null)
            {
                Members = await _context.Members.ToListAsync();
            }
        }
    }
}
