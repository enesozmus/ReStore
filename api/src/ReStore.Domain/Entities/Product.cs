namespace ReStore.Domain.Entities;

public class Product : BaseEntity
{
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }

        #region Relations

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? ColorId { get; set; }
        public Color Color { get; set; }

        #endregion
}
