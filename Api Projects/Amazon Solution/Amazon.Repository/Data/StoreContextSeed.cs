using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazon.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            //Seeding Data For ProductBrands Table to seed just for first time
            if (!context.ProductBrands.Any())
            {
                //Read All Data From Brand Json Fiel
                var brandText = File.ReadAllText("../Amazon.Repository/Data/DataSeed/brands.json");
                //Convert All Text Data to List<ProductBrand> to insert at it
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandText);
                //check if brans is not null or is not Empty
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                        //Add All Brand to ProductBrands Table at DB
                        await context.Set<ProductBrand>().AddAsync(brand);
                    //Save all Changes to Added to DB
                    await context.SaveChangesAsync();
                }
            }
            if (!context.ProductTypes.Any())
            {
                //Read All Data From Types Json Fiel
                var typeText = File.ReadAllText("../Amazon.Repository/Data/DataSeed/types.json");
                //Convert All Text Data to List<ProductType> to insert at it
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeText);
                //check if Types is not null or is not Empty
                if (types?.Count() > 0)
                {
                    foreach (var type in types)
                        //Add All Types to ProductType Table at DB
                        await context.Set<ProductType>().AddAsync(type);
                    //Save all Changes to Added to DB
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {
                //Read All Data From Product Json File
                var productText = File.ReadAllText("../Amazon.Repository/Data/DataSeed/products.json");
                //Convert All Text Data to List<Product> to insert at it
                var products = JsonSerializer.Deserialize<List<Product>>(productText);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        //Add All Product to ProductBrands Table at DB
                        await context.Set<Product>().AddAsync(product);
                    //Save all Changes to Added to DB
                    await context.SaveChangesAsync();
                }
            }
            //Seeding Data For ProductTypes Table to seed just for first time


        }
    }
}
