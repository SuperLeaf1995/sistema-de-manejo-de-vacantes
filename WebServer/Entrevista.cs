// Modelo de la entrevista para LINQ

using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System;

namespace WebServer.Models
{
    [Table(Name = "ENTREVISTA")]
    public class Entrevista
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public int Id { get; set; }

        [Column(Name="vacante")]
        public int VacanteId { get; set; }

        [Column(Name="prospecto")]
        public int ProspectoId { get; set; }

        [Column(Name="fecha_entrevista")]
        public DateTime Fecha { get; set; }

        [Column(Name="notas")]
        public string Notas { get; set; }

        [Column(Name="reclutado")]
        public bool Reclutado { get; set; }
    }
}