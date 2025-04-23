using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
			try
			{
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandsDate = await File.ReadAllTextAsync(@"..\Infrastructure\\Persistence\\Data\\DataSead\\brands.json");
                    var ProductBrandsDate =  File.OpenRead(@"..\Infrastructure\\Persistence\\Data\\DataSead\\brands.json");

                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsDate);

                    if (ProductBrands != null && ProductBrands.Any())
                    {
                       await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    //var ProductTypeDate = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSead\types.json");
                    var ProductTypeDate = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSead\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeDate);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                       await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductDate = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSead\products.json");
                    var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductDate);
                    if (Products != null && Products.Any())
                    {
                       await _dbContext.Products.AddRangeAsync(Products);
                    }
                }

               await _dbContext.SaveChangesAsync();
            }
			catch (Exception ex)
			{

                ////

			}
        }
    }
}
