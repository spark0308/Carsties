using System;
using AuctionService.Data;
using AuctionService.Entities;

namespace AuctionService.IntegrationTests.Util;

public static class DbHelper
{
    public static void InitDbForTests(AuctionDbContext db)
    {
        db.Auctions.AddRange(GetAuctionForTest());
        db.SaveChanges();
    }

    public static void ReinitDbForTests(AuctionDbContext db)
    {
        db.Auctions.RemoveRange(db.Auctions);
        db.SaveChanges();
        InitDbForTests(db);
    }

    private static List<Auction> GetAuctionForTest()
    {
        return new List<Auction>
        {
            // 1 Ford GT
            new Auction
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "GT",
                    Color = "White",
                    Mileage = 50000,
                    Year = 2020,
                    ImageUrl = "https://images.unsplash.com/photo-1575732905607-3d12d01218bf"
                }
            },
            // 2 Bugatti Veyron
            new Auction
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = Status.Live,
                ReservePrice = 90000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Make = "Bugatti",
                    Model = "Veyron",
                    Color = "Black",
                    Mileage = 15035,
                    Year = 2018,
                    ImageUrl = "https://images.unsplash.com/photo-1672658074939-16cd8cc3e414"
                }
            },
            // 3 Ford mustang
            new Auction
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(4),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "Mustang",
                    Color = "Black",
                    Mileage = 65125,
                    Year = 2023,
                    ImageUrl = "https://images.unsplash.com/photo-1678826537247-fc1e60fbda51"
                }
            }
        };
    }
}
