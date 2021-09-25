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
using WebServer.Models;

namespace RazorPagesWebServer.Pages.Entrevista
{
    namespace Index
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public IEnumerable<WebServer.Models.Entrevista> EntrevistaLista { get; set; }

            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                EntrevistaLista = ctx.Entrevistas;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Entrevistas.FirstOrDefault(e => e.Id.Equals(id));
                ctx.Entrevistas.DeleteOnSubmit(tmp);
                ctx.SubmitChanges();
                return RedirectToPage("/Entrevista/Index");
            }
        }
    }

    namespace Crear
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public WebServer.Models.Entrevista Entrevista { get; set; }

            // Lista para facilitar la autocomplecion de datos
            public IEnumerable<WebServer.Models.Prospecto> ProspectoLista { get; set; }
            public IEnumerable<WebServer.Models.Vacante> VacanteLista { get; set; }

            public string currentDateTime;

            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());

                // No podemos crear una entrevista sin prospecto u vacante asi que redireccionamos
                // a la pagina de creacion de estos
                ProspectoLista = ctx.Prospectos;
                if(ProspectoLista.Count() == 0)
                {
                    return RedirectToPage("/Prospecto/Crear");
                }

                VacanteLista = ctx.Vacantes;
                if(VacanteLista.Count() == 0)
                {
                    return RedirectToPage("/Vacante/Crear");
                }
                
                currentDateTime = $"{ System.DateTime.Now }";
                return Page();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                var ctx = new MainDataContext(DatabaseManager.GetConnectionString());

                // Verificar que las IDs sean validas (de los prospectos y de las vacantes)
                ctx.Entrevistas.InsertOnSubmit(Entrevista);
                ctx.SubmitChanges();
                return RedirectToPage("/Entrevista/Index");
            }
        }
    }

    namespace Editar
    {
        public class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public WebServer.Models.Entrevista Entrevista { get; set; }

            // Usado para crear un select para autocompletar IDs de prospectos y vacantes
            public IEnumerable<WebServer.Models.Prospecto> ProspectoLista { get; set; }
            public IEnumerable<WebServer.Models.Vacante> VacanteLista { get; set; }

            public string currentDateTime;
            
            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                ProspectoLista = ctx.Prospectos;
                if(ProspectoLista.Count() == 0)
                {
                    return RedirectToPage("/Prospecto/Crear");
                }

                VacanteLista = ctx.Vacantes;
                if(VacanteLista.Count() == 0)
                {
                    return RedirectToPage("/Vacante/Crear");
                }

                Entrevista = ctx.Entrevistas.FirstOrDefault(e => e.Id.Equals(Entrevista.Id));
                currentDateTime = $"{ System.DateTime.Now }";
                return Page();
            }
            
            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Entrevistas.FirstOrDefault(e => e.Id.Equals(Entrevista.Id));
                tmp.ProspectoId = Entrevista.ProspectoId;
                tmp.VacanteId = Entrevista.VacanteId;
                tmp.Fecha = Entrevista.Fecha;
                tmp.Notas = Entrevista.Notas;
                tmp.Reclutado = Entrevista.Reclutado;
                ctx.SubmitChanges();
                return RedirectToPage("/Entrevista/Index");
            }
        }
    }
}