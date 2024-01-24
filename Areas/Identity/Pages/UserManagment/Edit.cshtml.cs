﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Model;

namespace Provider.Areas.Identity.Pages.UserManagment
{
    public class EditModel : PageModel
    {
        private readonly Provider.Data.ProviderContext _context;

        public EditModel(Provider.Data.ProviderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Members Members { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var members =  await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }
            Members = members;
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

            _context.Attach(Members).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembersExists(Members.Id))
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

        private bool MembersExists(int id)
        {
          return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
