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
    public class DetailsModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public DetailsModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

      public Members Members { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }
            else 
            {
                Members = members;
            }
            return Page();
        }
    }
}
