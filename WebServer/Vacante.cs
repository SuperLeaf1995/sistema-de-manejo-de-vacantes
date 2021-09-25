// Modelo de la vacante para LINQ

using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System;

namespace WebServer.Models
{
    [Table(Name = "VACANTE")]
    public class Vacante
    {
        [Column(Name="id", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL IDENTITY")]
        public int Id { get; set; }

        [Column(Name="area")]
        [Required, StringLength(50)]
        public string Area { get; set; }

        [Column(Name="sueldo")]
        public decimal Sueldo { get; set; }

        [Column(Name="activo")]
        public bool Activo { get; set; }
    }
}