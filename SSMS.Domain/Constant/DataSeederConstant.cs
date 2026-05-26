using SSMS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Constant
{
    public static class DataSeederConstant
    {
        // Categories
        public const string Sneakers = "Giày thể thao";
        public const string RunningShoes = "Giày chạy bộ";

        // Brands
        public const string Adidas = "Adidas";
        public const string Nike = "Nike";

        // Sizes
        public const int Size40 = 40;
        public const int Size41 = 41;

        // Products
        public const string AdidasSneakers = "Giày thể thao Adidas";
        public const string NikeSneakers = "Giày thể thao Nike";
        public const string AdidasRunningShoes = "Giày chạy bộ Adidas";
        public const string NikeRunningShoes = "Giày chạy bộ Nike";

        // Descriptions
        public const string SneakersProductDescription = "Giày thể thao uy tín chất lượng cao.";
        public const string RunningShoesProductDescription = "Giày chạy bộ uy tín chất lượng cao.";

        // Price
        public const decimal Price1 = 100000;
        public const decimal Price2 = 110000;

        // Avatar
        public const string AvatarConst = "/images/AvatarDefault.png";

        public static readonly string[] Categories =
        [
            Sneakers,
            RunningShoes
        ];

        public static readonly string[] Brands =
        [
            Adidas,
            Nike
        ];

        public static readonly int[] Sizes =
        [
            Size40,
            Size41
        ];

        public static readonly ProductSeedModel[] ProductSeeds = 
        [
            new ProductSeedModel
            {
                Name = AdidasSneakers,
                Category = Sneakers,
                Brand = Adidas,
                Description = SneakersProductDescription
            },
            new ProductSeedModel
            {
                Name = NikeSneakers,
                Category = Sneakers,
                Brand = Nike,
                Description = SneakersProductDescription
            },
            new ProductSeedModel
            {
                Name = AdidasRunningShoes,
                Category = RunningShoes,
                Brand = Adidas,
                Description = RunningShoesProductDescription
            },
            new ProductSeedModel
            {
                Name = NikeRunningShoes,
                Category = RunningShoes,
                Brand = Nike,
                Description = RunningShoesProductDescription
            }
        ];

        public static readonly ProductSizePriceSeedModel[] ProductSizePriceSeeds = 
        [
            new ProductSizePriceSeedModel
            {
                Product = AdidasSneakers,
                Size = Size40,
                Price = Price1
            },
            new ProductSizePriceSeedModel
            {
                Product = AdidasSneakers,
                Size = Size41,
                Price = Price2
            },
            new ProductSizePriceSeedModel
            {
                Product = NikeSneakers,
                Size = Size40,
                Price = Price1
            },
            new ProductSizePriceSeedModel
            {
                Product = NikeSneakers,
                Size = Size41,
                Price = Price2
            },
            new ProductSizePriceSeedModel
            {
                Product = AdidasRunningShoes,
                Size = Size40,
                Price = Price1
            },
            new ProductSizePriceSeedModel
            {
                Product = AdidasRunningShoes,
                Size = Size41,
                Price = Price2
            },
            new ProductSizePriceSeedModel
            {
                Product = NikeRunningShoes,
                Size = Size40,
                Price = Price1
            },
            new ProductSizePriceSeedModel
            {
                Product = NikeRunningShoes,
                Size = Size41,
                Price = Price2
            }
        ];

        public static readonly ProductImageSeedModel[] ProductImageSeeds =
        [
            new (){ Product = AdidasSneakers, Image = "SA1"},
            new (){ Product = AdidasSneakers, Image = "SA2"},
            new (){ Product = AdidasSneakers, Image = "SA3"},

            new (){ Product = NikeSneakers, Image = "SN1"},
            new (){ Product = NikeSneakers, Image = "SN2"},
            new (){ Product = NikeSneakers, Image = "SN3"},

            new (){ Product = AdidasRunningShoes, Image = "RSA1"},
            new (){ Product = AdidasRunningShoes, Image = "RSA2"},
            new (){ Product = AdidasRunningShoes, Image = "RSA3"},

            new (){ Product = NikeRunningShoes, Image = "RSN1"},
            new (){ Product = NikeRunningShoes, Image = "RSN2"},
            new (){ Product = NikeRunningShoes, Image = "RSN3"},
        ];

        public static readonly UserSeedModel customer = new ()
        {
            FullName = "A Nguyen",
            Avatar = AvatarConst,
            Gender = Enums.GenderType.Male,
            PhoneNumber = "0123456789",
            //Role = UserRole.CUSTOMER,
        };

        public static readonly UserSeedModel admin = new()
        {
            FullName = "Admin Nguyen",
            Avatar = AvatarConst,
            Gender = Enums.GenderType.Female,
            PhoneNumber = "0123456788",
            //Role = UserRole.ADMIN,
        };
    }
}
