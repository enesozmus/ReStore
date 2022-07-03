namespace ReStore.Domain.Entities;

public class Basket : BaseEntity
{
     public string BuyerId { get; set; }
     public List<BasketItem> Items { get; set; } = new();

     // for payment
     public string? PaymentIntentId { get; set; }
     public string? ClientSecret { get; set; }

     #region MyRegion

     public void AddItem(Product product, int quantity)
     {
          if (Items.All(item => item.ProductId != product.Id))
          {
               Items.Add(new BasketItem { Product = product, Quantity = quantity });
          }

          var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
          if (existingItem != null) existingItem.Quantity += quantity;
     }

     public void RemoveItem(int productId, int quantity)
     {
          var item = Items.FirstOrDefault(item => item.ProductId == productId);
          if (item == null) return;
          item.Quantity -= quantity;
          if (item.Quantity == 0) Items.Remove(item);
     }

     #endregion

     #region MyRegion

     //             if (!Items.Any(item => item.ProductId == product.Id))
     //            {
     //                      Items.Add(new BasketItem { Product = product, Quantity = quantity });
     //            }

     //public void AddItem(Product product, int quantity)
     //{
     //        var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
     //        if (existingItem != null)
     //        {
     //                existingItem.Quantity += quantity;
     //                return;
     //        }

     //        Items.Add(new BasketItem { Product = product, Quantity = quantity });
     //}

     #endregion
}
