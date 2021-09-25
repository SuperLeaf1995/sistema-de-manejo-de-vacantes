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

using System.Text;
using System.Text.RegularExpressions;

using WebServer.Database;
using WebServer.Models;

namespace RazorPagesWebServer.Pages.Prospecto
{
    namespace Index
    {
        public class BaseModel : PageModel
        {
            public IEnumerable<WebServer.Models.Prospecto> ProspectoLista { get; set; }

            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                ProspectoLista = ctx.Prospectos;
                return Page();
            }
            
            public IActionResult OnPostDelete(int id)
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Prospectos.FirstOrDefault(e => e.Id.Equals(id));
                ctx.Prospectos.DeleteOnSubmit(tmp);
                ctx.SubmitChanges();
                return RedirectToPage("/Prospecto/Index");
            }
        }
    }

    namespace Crear
    {
        public class BaseModel : PageModel
        {
            [BindProperty]
            public WebServer.Models.Prospecto Prospecto { get; set; }

            // El tiempo de ahora mismo es utilizado automaticamente para nuevos prospectos
            public string currentDateTime;

            public IActionResult OnGet()
            {
                currentDateTime = $"{ System.DateTime.Now }";
                return Page();
            }

            public IActionResult OnPost(string? handler)
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Un regex para checar el email, se conforma a el estandar RFC 5322
                // http://www.ietf.org/rfc/rfc5322.txt
                var re = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\x22(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*\x22)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                if (!re.IsMatch(Prospecto.Correo))
                {
                    ModelState.AddModelError("Prospecto.Correo", "El E-Mail no es valido");
                    return Page();
                }

                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                ctx.Prospectos.InsertOnSubmit(Prospecto);
                ctx.SubmitChanges();
                System.Console.WriteLine("New prospecto {0}", Prospecto.Id);
                return RedirectToPage("/Prospecto/Index");
            }
        }
    }

    namespace Editar
    {
        public class BaseModel : PageModel
        {
            [BindProperty(SupportsGet=true)]
            public WebServer.Models.Prospecto Prospecto { get; set; }

            public string currentDateTime;

            public IActionResult OnGet()
            {
                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                Prospecto = ctx.Prospectos.FirstOrDefault(e => e.Id.Equals(Prospecto.Id));
                currentDateTime = $"{ System.DateTime.Now }";
                return Page();
            }
            
            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var re = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\x22(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*\x22)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                if (!re.IsMatch(Prospecto.Correo))
                {
                    ModelState.AddModelError("Prospecto.Correo", "El E-Mail no es valido");
                    return Page();
                }

                var ctx = new WebServer.Database.MainDataContext(DatabaseManager.GetConnectionString());
                var tmp = ctx.Prospectos.FirstOrDefault(e => e.Id.Equals(Prospecto.Id));
                tmp.Nombre = Prospecto.Nombre;
                tmp.Correo = Prospecto.Correo;
                tmp.Fecha = Prospecto.Fecha;
                ctx.SubmitChanges();
                return RedirectToPage("/Prospecto/Index");
            }
        }
    }
}