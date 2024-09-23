using System;
using Microsoft.EntityFrameworkCore;
using ThirdService.Entities;

namespace ThirdService.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
}