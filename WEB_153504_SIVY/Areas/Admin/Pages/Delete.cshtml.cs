using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WEB_153504_SIVY.API.Data.ApplicationDbContext _context;

        public DeleteModel(WEB_153504_SIVY.API.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CarModel CarModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CarModels == null)
            {
                return NotFound();
            }

            var carmodel = await _context.CarModels.FirstOrDefaultAsync(m => m.Id == id);

            if (carmodel == null)
            {
                return NotFound();
            }
            else 
            {
                CarModel = carmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CarModels == null)
            {
                return NotFound();
            }
            var carmodel = await _context.CarModels.FindAsync(id);

            if (carmodel != null)
            {
                CarModel = carmodel;
                _context.CarModels.Remove(CarModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
