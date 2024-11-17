﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarWars.Infrastructure.Contracts.EntitiesDB
{
    public partial class Planet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string NombrePlaneta { get; set; }
        public int RotacionOrbitalEnDiasAlderaanos { get; set; }
        public int PeriodoOrbitalEnHorasAlderaanas { get; set; }
        [Required]
        [StringLength(50)]
        public string Clima { get; set; }
        [Required]
        [StringLength(50)]
        public string Poblacion { get; set; }
        [Required]
        [StringLength(250)]
        public string Url { get; set; }
    }
}