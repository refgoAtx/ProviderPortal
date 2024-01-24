using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Provider.Areas.Identity.Data;
using Provider.Data;

namespace Provider.Areas.Identity.Pages.RolePermission
{
    public class EditModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public EditModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProviderRolePermission ProviderRolePermission { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProviderRolePermission == null)
            {
                return NotFound();
            }

            var providerrolepermission =  await _context.ProviderRolePermission.FirstOrDefaultAsync(m => m.Id == id);
            if (providerrolepermission == null)
            {
                return NotFound();
            }
            ProviderRolePermission = providerrolepermission;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProviderRolePermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderRolePermissionExists(ProviderRolePermission.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProviderRolePermissionExists(int id)
        {
          return (_context.ProviderRolePermission?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
