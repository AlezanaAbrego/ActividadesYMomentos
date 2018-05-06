using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ActividadesSugeridasRazorPages.Models;

namespace ActividadesSugeridasRazorPages.Pages.ActividadesSugeridasEstatus
{
    public class EditModel : PageModel
    {
        private readonly ActividadesSugeridasRazorPages.Models.ApplicationDbContext _context;
        public short? idAct;
        public int idacti;

        public EditModel(ActividadesSugeridasRazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActividadSugeridaEstatus ActividadSugeridaEstatus { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            idacti = Convert.ToInt32(Request.Query["idacti"]);
           
            idAct = id;
            if (id == null)
            {
                return NotFound();
            }

            ActividadSugeridaEstatus = await _context.ActividadesSugeridasEstatus
                .Include(a => a.Eva_cat_actividades_sugeridas)
                .Include(a => a.Eva_cat_tipo_actividades_sugeridas)
                .Include(a => a.TiposEstatus).SingleOrDefaultAsync(m => m.IdEstatusDet == id);

            if (ActividadSugeridaEstatus == null)
            {
                return NotFound();
            }
            ViewData["IdActividadSugerida"] = idacti;  // new SelectList(_context.ActividadesSugeridas, "IdActividadSugerida", "DesActividad");
            ViewData["IdTipoActividadSug"] = idAct; // new SelectList(_context.TipoActividadesSugeridas, "IdTipoActividadSugerida", "DesTipoActividadSugerida");
           ViewData["IdTipoEstatus"] = new SelectList(_context.Set<TipoEstatus>(), "IdTipoEstatus", "DesTipoEstatus");
            return Page();
        }

        /* public async Task<IActionResult> OnPostAsync()
         {
             if (!ModelState.IsValid)
             {
                 return Page();
             }

             _context.Attach(ActividadSugeridaEstatus).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!ActividadSugeridaEstatusExists(ActividadSugeridaEstatus.IdEstatusDet))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return RedirectToPage("./Index", new { id = idAct });
         }*/

        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.ActividadesSugeridasEstatus.Add(ActividadSugeridaEstatus);
            await _context.SaveChangesAsync();

        

            return RedirectToPage("./Index", new { id = idAct });
            //return Redirect("./Index"+idAct.ToString());
        }

        private bool ActividadSugeridaEstatusExists(int id)
        {
            return _context.ActividadesSugeridasEstatus.Any(e => e.IdEstatusDet == id);
        }
    }
}
