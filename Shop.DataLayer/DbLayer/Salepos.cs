namespace Shop.DataLayer.DbLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Salepos
    {
        public int SaleposId { get; set; }

        public int SaleId { get; set; }

        public int GoodsId { get; set; }

        public decimal CountGoods { get; set; }

        [Column(TypeName = "money")]
        public decimal Summa { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
