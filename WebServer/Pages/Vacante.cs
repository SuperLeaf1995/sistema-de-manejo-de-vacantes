// Modelo de form para vacante

using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Configuration;
using WebServer.Database;

namespace RazorPagesWebServer.Pages.Vacante
{
    namespace Index
    {
        public class BaseModel : PageModel
        {
            public IEnumerable<WebServer.Models.Vacante> VacanteLista { get; set; }
            
            public IActionResult OnGet(string? format)
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                VacanteLista = ctx.Vacantes;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Vacantes.FirstOrDefault(e => e.Id.Equals(id));
                ctx.Vacantes.DeleteOnSubmit(tmp);
                ctx.SubmitChanges();
                return RedirectToPage("/Vacante/Index");
            }
        }
    }

    namespace Crear
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public WebServer.Models.Vacante Vacante { get; set; }

            public IActionResult OnGet()
            {
                return Page();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                ctx.Vacantes.InsertOnSubmit(Vacante);
                ctx.SubmitChanges();
                System.Console.WriteLine("New vacante {0}", Vacante.Id);
                return RedirectToPage("/Vacante/Index");
            }
        }
    }

    namespace Editar
    {
        public class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public WebServer.Models.Vacante Vacante { get; set; }

            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                Vacante = ctx.Vacantes.FirstOrDefault(e => e.Id.Equals(Vacante.Id));
                return Page();
            }
            
            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Vacantes.FirstOrDefault(e => e.Id.Equals(Vacante.Id));
                tmp.Sueldo = Vacante.Sueldo;
                tmp.Area = Vacante.Area;
                tmp.Activo = Vacante.Activo;
                ctx.SubmitChanges();
                return RedirectToPage("/Vacante/Index");
            }
        }
    }
}