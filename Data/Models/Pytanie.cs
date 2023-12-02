using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Projekt_Quizy.Data.Models
{
    public class Pytanie
    {
        [Key] public int PytanieId { get; set; }


        [Required]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Tresc { get; set; }
        [Required]
        public string PoziomTrudnosci { get; set; }
        [Required]
        public string? Answer_A { get; set; }
        public string? Answer_B { get; set; }
        public string? Answer_C { get; set; }
        public string? Answer_D { get; set; }
        public string? Answer_E { get; set; }
        public string? Answer_F { get; set; }

        public bool? Answer_A_Correct { get; set; }
        public bool? Answer_B_Correct { get; set; }
        public bool? Answer_C_Correct { get; set; }
        public bool? Answer_D_Correct { get; set; }
        public bool? Answer_E_Correct { get; set; }
        public bool? Answer_F_Correct { get; set; }
        public int KategoriaId { get; set; }
        public virtual Kategoria Kategoria { get; set; }


    }
}