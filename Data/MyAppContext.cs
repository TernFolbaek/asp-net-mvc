using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC.Models;
namespace ASP.NET_MVC.Data;

public class MyAppContext : DbContext
{
    //this is a constructor
    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemClient>().HasKey(x => new { x.ItemId, x.ClientId });
        modelBuilder.Entity<ItemClient>().HasOne(i => i.Item).WithMany(ic => ic.ItemClients).HasForeignKey(x => x.ItemId);
        modelBuilder.Entity<ItemClient>().HasOne(i => i.Client).WithMany(ic => ic.ItemClients).HasForeignKey(x => x.ClientId);

        modelBuilder.Entity<Item>().HasData(
            new Item {Id=40, Name="microphone", Price=1000, SerialNumberId=1 }
            );
        modelBuilder.Entity<SerialNumber>().HasData(
            new SerialNumber { Id = 10, Name = "MIC150", ItemId = 40 });
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Houses" }
            );
    }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<SerialNumber> SerialNumbers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ItemClient> ItemClients { get; set; }
    
        

}