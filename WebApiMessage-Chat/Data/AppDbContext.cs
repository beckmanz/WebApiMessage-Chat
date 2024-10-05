using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data.Map;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<MessageModel> Messages { get; set; }
    public DbSet<FriendModel> Friends { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new MessageMap());
        modelBuilder.ApplyConfiguration(new FriendMap());
        base.OnModelCreating(modelBuilder);
    }
}