using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.Infrastructure.SeedData;

public class DbInitializer
{
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                        var context = serviceScope.ServiceProvider.GetService<ReStoreContext>();

                        context.Database.Migrate();

                        #region Categories

                        if (!context.Categories.Any())
                        {
                                context.Categories.AddRange(new List<Category>()
                                        {
                                                new Category {Name = "Mont", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Ceket", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Kazak", CreatedDate =new DateTime(2022, 04, 16) },
                                                new Category {Name = "Jean", CreatedDate =new DateTime(2022, 04, 16) },
                                                new Category {Name = "Pantolon", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Hırka ve Süveter", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Bluz", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Gömlek", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Tişört", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Sweatshirt", CreatedDate = new DateTime(2022, 04, 16) },
                                                new Category {Name = "Diğer", CreatedDate = new DateTime(2022, 04, 16) },
                                        });
                                context.SaveChanges();
                        }

                        #endregion

                        #region Products

                        if (!context.Products.Any())
                        {
                                context.Products.AddRange(new List<Product>()
                                        {
                                                new Product { CategoryId = 1, PictureUrl ="/images/products/itemA.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Beyaz Outdoor Şişme Mont", QuantityInStock = 400,  Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 1, PictureUrl ="/images/products/itemB.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Turuncu Outdoor Şişme Mont", QuantityInStock = 400, Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 1, PictureUrl ="/images/products/itemC.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Siyah Outdoor Şişme Mont", QuantityInStock = 400, Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 1, PictureUrl ="/images/products/itemD.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Siyah Hakiki Deri Mont", QuantityInStock = 400, Price = 269999, CreatedDate = new DateTime(2022, 04, 16) },
                                               
                                                new Product { CategoryId = 2, PictureUrl ="/images/products/itemE.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Bej Blazer Ceket", QuantityInStock = 400, Price = 59999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 2, PictureUrl ="/images/products/itemF.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Gri Ekoseli Blazer Ceket", QuantityInStock = 400, Price = 49999, CreatedDate = new DateTime(2022, 04, 16) },
                                               
                                                new Product { CategoryId = 3, PictureUrl ="/images/products/itemG.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Bordo Melanj Triko Kazak", QuantityInStock = 400, Price = 23999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 3, PictureUrl ="/images/products/itemH.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Gri Melanj Triko Kazak", QuantityInStock = 400, Price = 23999, CreatedDate = new DateTime(2022, 04, 16) },

                                                new Product { CategoryId = 4, PictureUrl ="/images/products/itemI.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Açık Mavi Jean Pantolon", QuantityInStock = 400, Price = 29999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 4, PictureUrl ="/images/products/itemJ.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Orta Rodeo Jean Pantolon", QuantityInStock = 400, Price = 29999, CreatedDate = new DateTime(2022, 04, 16) },

                                                new Product { CategoryId = 5, PictureUrl ="/images/products/itemK.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Haki Jogger Pantolon", QuantityInStock = 400, Price = 35999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 5, PictureUrl ="/images/products/itemL.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "Antrasit Kargo Pantolon", QuantityInStock = 400, Price = 34999, CreatedDate = new DateTime(2022, 04, 16) },
                                                                                                                                              
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item1.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product1", QuantityInStock = 400, Price = 12000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item2.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product2", QuantityInStock = 400, Price = 19999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item3.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product3", QuantityInStock = 400, Price = 12000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item4.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product4", QuantityInStock = 400, Price = 25000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item5.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product5", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item6.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product6", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item7.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product7", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item8.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product8", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item9.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product9", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { CategoryId = 11, PictureUrl ="/images/products/item10.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Name = "product10", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                        });
                                context.SaveChanges();
                        }

                        #endregion
                }
        }
}
