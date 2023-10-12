namespace Inforno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordini()
        {
            ArticoliOrdine = new HashSet<ArticoliOrdine>();
        }

        [Key]
        public int IDOrdine { get; set; }

        public int IDUser { get; set; }

        public DateTime DataOrdine { get; set; }

        [Required]
        [StringLength(255)]
        public string IndirizzoSpedizione { get; set; }

        public string Note { get; set; }

        public bool IsEvaso { get; set; }=false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticoliOrdine> ArticoliOrdine { get; set; }

        public virtual Users Users { get; set; }
    }
}
