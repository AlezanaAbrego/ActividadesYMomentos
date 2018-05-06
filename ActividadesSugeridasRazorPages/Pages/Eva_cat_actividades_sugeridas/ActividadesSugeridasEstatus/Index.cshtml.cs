using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ActividadesSugeridasRazorPages.Models;

namespace ActividadesSugeridasRazorPages.Pages.ActividadesSugeridasEstatus
{
    public class IndexModel : PageModel
    {
        private readonly ActividadesSugeridasRazorPages.Models.ApplicationDbContext _context;

        public IndexModel(ActividadesSugeridasRazorPages.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ActividadSugeridaEstatus> ActividadSugeridaEstatus { get; set; }
        public IList<Eva_cat_tipo_actividad_sugerida> Eva_cat_tipo_actividad_sugerida { get; set; }
        public IList<Eva_cat_actividad_sugerida> Eva_cat_actividad_sugerida { get; set; }


        public string message;
        public string description;
        public string tipo;
        public int Tipos;
        public int IdDescripcion;
        public short? IdActividad;


        public async Task OnGetAsync(short? id)
        {
            ActividadSugeridaEstatus = await _context.ActividadesSugeridasEstatus
                .Include(a => a.Eva_cat_actividades_sugeridas)
                .Include(a => a.Eva_cat_tipo_actividades_sugeridas)
                .Include(a => a.TiposEstatus).ToListAsync();

            Eva_cat_actividad_sugerida = await _context.Eva_cat_actividades_sugeridas
                .Include(a => a.Eva_cat_tipo_actividades_sugeridas).ToListAsync();

            Eva_cat_tipo_actividad_sugerida = await _context.Eva_cat_tipo_actividades_sugeridas.ToListAsync();


            IdActividad = id;
 

            foreach (var value in Eva_cat_actividad_sugerida)
            {
                if(id == value.IdActividadSugerida)
                {
                    message = value.Tema.ToString();
                    description =  value.DesActividad.ToString();
                    IdDescripcion = value.IdTipoActividadSug;

                    foreach (var val in Eva_cat_tipo_actividad_sugerida)
                    {
                        if (IdDescripcion == val.IdTipoActividadSug)
                        {
                            tipo = val.DesTipoActividadSug.ToString();
                            Tipos = (short)val.IdTipoActividadSug;
                        }
                    }

                }


            }
        }

        
    }
}
