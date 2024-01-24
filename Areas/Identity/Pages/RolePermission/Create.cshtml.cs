using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Provider.Areas.Identity.Data;
using Provider.Data;

namespace Provider.Areas.Identity.Pages.RolePermission
{
    public class CreateModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public CreateModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProviderRolePermission ProviderRolePermission { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ProviderRolePermission == null || ProviderRolePermission == null)
            {
                return Page();
            }

            _context.ProviderRolePermission.Add(ProviderRolePermission);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
