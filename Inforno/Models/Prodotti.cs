namespace Inforno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            ArticoliOrdine = new HashSet<ArticoliOrdine>();
        }

        [Key]
        public int IDProdotto { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        public string Foto { get; set; }

        public decimal PrezzoVendita { get; set; }

        public string TempoConsegna { get; set; }

        [Required]
        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticoliOrdine> ArticoliOrdine { get; set; }
    }
}
