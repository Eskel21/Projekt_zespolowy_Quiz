using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Projekt_Quizy.Data.Models
{
    public class Kategoria
    {
        [Key] public int KategoriaId { get; set; }


        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(200)")]
        public string Nazwa { get; set; }
        [Required]
        public int DzialId { get; set; }
        public virtual Dzial Dzial { get; set; }
        public virtual ICollection<Pytanie>? Pytania
        {
            get; set;
        }

    }
}
