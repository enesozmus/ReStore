using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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

                        //#region Categories

                        //if (!context.Categories.Any())
                        //{
                        //        context.Categories.AddRange(new List<Category>()
                        //                {
                        //                        new Category {Name = "Mont", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Ceket", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Kazak", CreatedDate =new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Jean", CreatedDate =new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Pantolon", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Hırka ve Süveter", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Bluz", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Gömlek", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Tişört", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Sweatshirt", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Category {Name = "Diğer", CreatedDate = new DateTime(2022, 04, 16) },
                        //                });
                        //        context.SaveChanges();
                        //}

                        //#endregion


                        //#region Colors

                        //if (!context.Colors.Any())
                        //{
                        //        context.Colors.AddRange(new List<Color>()
                        //                {
                        //                        new Color { Name = "Ekru", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Kırmızı", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Lacivert", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Kahverengi", CreatedDate = new DateTime(2022, 04, 16)},
                        //                        new Color { Name = "Mavi", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        // 6
                        //                        new Color { Name = "Antrasit", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Koyu Gri", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Canlı Turuncu", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Bej Çizgili", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Beyaz", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        // 11
                        //                        new Color { Name = "Gri", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "İndigo Melanj", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Koyu Rodeo", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Optik Beyaz", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Siyah", CreatedDate = new DateTime(2022, 04, 16) },
                                                
                        //                        //16
                        //                        new Color { Name = "Bej", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Bordo", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Açık Mavi", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Orta Rodeo", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "Haki", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        //21
                        //                        new Color { Name = "Mor", CreatedDate = new DateTime(2022, 04, 16) },
                        //                        new Color { Name = "unassigned", CreatedDate = new DateTime(2022, 04, 16) }
                        //                });
                        //        context.SaveChanges();
                        //}

                        //#endregion


                        #region Products

                        if (!context.Products.Any())
                        {
                                context.Products.AddRange(new List<Product>()
                                        {
                                                new Product { PictureUrl ="/images/products/itemA.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Mont", Color = "Ekru", Name = "Beyaz Outdoor Şişme Mont", QuantityInStock = 400,  Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemB.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Mont", Color = "Kırmızı", Name = "Turuncu Outdoor Şişme Mont", QuantityInStock = 400, Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemC.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Mont", Color = "Lacivert", Name = "Siyah Outdoor Şişme Mont", QuantityInStock = 400, Price = 64999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemD.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Mont", Color = "Kahverengi", Name = "Kahverengi Hakiki Deri Mont", QuantityInStock = 400, Price = 269999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemE.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Ceket", Color = "Antrasit", Name = "Bej Blazer Ceket", QuantityInStock = 400, Price = 59999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemF.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Ceket", Color = "Koyu Gri", Name = "Gri Ekoseli Blazer Ceket", QuantityInStock = 400, Price = 49999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemG.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Kazak", Color = "Canlı Turuncu", Name = "Bordo Melanj Triko Kazak", QuantityInStock = 400, Price = 23999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemH.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Kazak", Color = "Bej Çizgili", Name = "Gri Melanj Triko Kazak", QuantityInStock = 400, Price = 23999, CreatedDate = new DateTime(2022, 04, 16) },                                                                                                  
                                                new Product { PictureUrl ="/images/products/itemI.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Pantolon", Color = "Beyaz", Name = "Açık Mavi Jean Pantolon", QuantityInStock = 400, Price = 29999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemJ.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Pantolon", Color = "Antrasit", Name = "Orta Rodeo Jean Pantolon", QuantityInStock = 400, Price = 29999, CreatedDate = new DateTime(2022, 04, 16) },                                                    
                                                new Product { PictureUrl ="/images/products/itemK.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Pantolon", Color = "Gri", Name = "Haki Jogger Pantolon", QuantityInStock = 400, Price = 35999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/itemL.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Pantolon", Color = "İndigo Melanj", Name = "Antrasit Kargo Pantolon", QuantityInStock = 400, Price = 34999, CreatedDate = new DateTime(2022, 04, 16) },                                                                                          
                                                new Product { PictureUrl ="/images/products/item1.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Koyu Rodeo", Name = "product1", QuantityInStock = 400, Price = 12000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item2.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Optik Beyaz", Name = "product2", QuantityInStock = 400, Price = 19999, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item3.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Siyah", Name = "product3", QuantityInStock = 400, Price = 12000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item4.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Bej", Name = "product4", QuantityInStock = 400, Price = 25000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item5.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Bordo", Name = "product5", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item6.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Açık Mavi", Name = "product6", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item7.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Orta Rodeo", Name = "product7", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item8.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Haki", Name = "product8", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item9.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki", Category = "Diğer", Color = "Mor", Name = "product9", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                                new Product { PictureUrl ="/images/products/item10.png", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vel ligula fringilla, mollis nisi et, ullamcorper libero.", Brand = "LC Waikiki",Category = "Diğer", Color = "unassigned", Name = "product10", QuantityInStock = 400, Price = 30000, CreatedDate = new DateTime(2022, 04, 16) },
                                        });
                                context.SaveChanges();
                        }

                        #endregion
                }
        }
}
