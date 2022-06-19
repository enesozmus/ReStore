using System.ComponentModel.DataAnnotations.Schema;

namespace ReStore.Domain.Entities;

[Table("BasketItems")]
public class BasketItem : BaseEntity
{
        public int Quantity { get; set; }


        #region Navigation Properties

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        #endregion

}
