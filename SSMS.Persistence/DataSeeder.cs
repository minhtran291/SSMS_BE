using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSMS.Domain.ConfigOptions;
using SSMS.Domain.Constant;
using SSMS.Domain.Entities;
using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Seed;

namespace SSMS.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
            SSMSContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IOptions<SeedSettings> seedOptions)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                foreach (var role in UserRole.ALL)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(role));

                        if (!result.Succeeded)
                        {
                            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }
                }

                // seed category
                var categoryDict = new Dictionary<string, Category>();

                foreach (var name in DataSeederConstant.Categories)
                {
                    var category = await SeedHelpers.GetOrCreateAsync(
                        context.Categories,
                        c => c.CategoryName == name,
                        () => new Category { CategoryName = name }
                     );

                    categoryDict[name] = category;
                }

                // seed brand
                var brandDict = new Dictionary<string, Brand>();

                foreach (var name in DataSeederConstant.Brands)
                {
                    var brand = await SeedHelpers.GetOrCreateAsync(
                        context.Brands,
                        b => b.BrandName == name,
                        () => new Brand { BrandName = name }
                     );
                    brandDict[name] = brand;
                }

                // seed size
                var sizeDict = new Dictionary<int, Size>();

                foreach (var size in DataSeederConstant.Sizes)
                {
                    var entity = await SeedHelpers.GetOrCreateAsync(
                        context.Sizes,
                        s => s.Value == size,
                        () => new Size { Value = size }
                     );
                    sizeDict[size] = entity;
                }

                await context.SaveChangesAsync();

                //seed product
                var productDict = new Dictionary<string, Product>();

                foreach (var seed in DataSeederConstant.ProductSeeds)
                {
                    var product = await SeedHelpers.GetOrCreateAsync(
                        context.Products,
                        p => p.ProductName == seed.Name,
                        () => new Product
                        {
                            ProductName = seed.Name,
                            Description = seed.Description,
                            CategoryId = categoryDict[seed.Category].Id,
                            BrandId = brandDict[seed.Brand].Id,
                        }
                     );
                    productDict[seed.Name] = product;
                }

                await context.SaveChangesAsync();

                // seed price
                var existingPrices = (await context.ProductSizePrices
                    .Select(x => new { x.ProductId, x.SizeId })
                    .ToListAsync())
                    .Select(x => (x.ProductId, x.SizeId))
                    .ToHashSet();

                foreach (var seed in DataSeederConstant.ProductSizePriceSeeds)
                {
                    if (!productDict.TryGetValue(seed.Product, out var product))
                        throw new Exception($"Product not found: {seed.Product}");

                    if (!sizeDict.TryGetValue(seed.Size, out var size))
                        throw new Exception($"Size not found: {seed.Size}");

                    var exists = existingPrices.Contains((product.Id, size.Id));

                    if (!exists)
                    {
                        await context.ProductSizePrices.AddAsync(new ProductSizePrice
                        {
                            ProductId = product.Id,
                            SizeId = size.Id,
                            Price = seed.Price,
                        });

                        existingPrices.Add((product.Id, size.Id));
                    }
                }

                // seed image
                var existingImages = (await context.ProductImages
                        .Select(x => new { x.ProductId, x.Image })
                        .ToListAsync())
                        .Select(x => (x.ProductId, x.Image))
                        .ToHashSet();

                foreach (var seed in DataSeederConstant.ProductImageSeeds)
                {
                    if (!productDict.TryGetValue(seed.Product, out var product))
                        throw new Exception($"Product not found: {seed.Product}");

                    var exists = existingImages.Contains((product.Id, seed.Image));

                    if (!exists)
                    {
                        await context.ProductImages.AddAsync(new ProductImage
                        {
                            ProductId = product.Id,
                            Image = seed.Image,
                        });

                        existingImages.Add((product.Id, seed.Image));
                    }
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction.RollbackAsync();
                throw;
            }

            // seed user
            if (seedOptions?.Value.Admin == null || seedOptions.Value.Customer == null)
            {
                throw new Exception("Seed user configuration is missing.");
            }

            var adminConfig = seedOptions.Value.Admin;

            var customerConfig = seedOptions.Value.Customer;

            // admin
            var existingAdminUserName = await userManager.FindByNameAsync(adminConfig.Username);
            var existingAdminEmail = await userManager.FindByEmailAsync(adminConfig.Email);
            if (existingAdminUserName == null && existingAdminEmail == null)
            {
                var admin = new User
                {
                    Username = adminConfig.Username,
                    Email = adminConfig.Email,
                    FullName = DataSeederConstant.admin.FullName,
                    Avatar = DataSeederConstant.admin.Avatar,
                    Gender = DataSeederConstant.admin.Gender,
                    PhoneNumber = DataSeederConstant.admin.PhoneNumber,
                };

                var result = await userManager.CreateAsync(admin, adminConfig.Password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                var roleResult = await userManager.AddToRoleAsync(admin, UserRole.ADMIN);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }

            // customer
            var existingCustomerUserName = await userManager.FindByNameAsync(customerConfig.Username);
            var existingCustomerEmail = await userManager.FindByEmailAsync(customerConfig.Email);

            if (existingCustomerUserName == null && existingCustomerEmail == null)
            {
                var customer = new User
                {
                    //Id = Guid.NewGuid().ToString(),
                    Username = customerConfig.Username,
                    Email = customerConfig.Email,
                    //NormalizedEmail = "MINHTRAN2912003@GMAIL.COM",
                    //NormalizedUserName = "CUSTOMER",
                    FullName = DataSeederConstant.customer.FullName,
                    Avatar = DataSeederConstant.customer.Avatar,
                    Gender = DataSeederConstant.customer.Gender,
                    PhoneNumber = DataSeederConstant.customer.PhoneNumber,
                    //SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await userManager.CreateAsync(customer, customerConfig.Password);
                //await context.SaveChangesAsync();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                var roleResult = await userManager.AddToRoleAsync(customer, UserRole.CUSTOMER);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
