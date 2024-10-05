using Microsoft.EntityFrameworkCore;

namespace WebApiMessage_Chat.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}