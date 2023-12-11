using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Projekt_Quizy.Data.Models
{
    public class Dzial
    {
        [Key] public int DzialId { get; set; }


        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(200)")]
        public string Nazwa { get; set; }

        public int licznik { get; set; }
        public virtual ICollection<Kategoria>? Kategorie
        {
            get; set;
        }

    }
}
