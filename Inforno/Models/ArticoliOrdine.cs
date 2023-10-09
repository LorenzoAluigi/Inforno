namespace Inforno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArticoliOrdine")]
    public partial class ArticoliOrdine
    {
        [Key]
        public int IDArticolo { get; set; }

        public int IDOrdine { get; set; }

        public int IDProdotto { get; set; }

        public int Quantita { get; set; }

        public decimal PrezzoUnitario { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
