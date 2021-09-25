// Modelo del prospecto para LINQ

using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System;

namespace WebServer.Models
{
    [Table(Name = "PROSPECTO")]
    public class Prospecto
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public Int32 Id { get; set; }

        [Column(Name="nombre")]
        [Required, StringLength(50)]
        public string Nombre { get; set; }

        [Column(Name="correo")]
        [Required, StringLength(50)]
        public string Correo { get; set; }
        
        [Column(Name="fecha_registro")]
        public DateTime Fecha { get; set; }
    }
}