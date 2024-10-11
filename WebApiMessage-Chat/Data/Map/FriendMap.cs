using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data.Map;

public class FriendMap : IEntityTypeConfiguration<FriendModel>
{
    public void Configure(EntityTypeBuilder<FriendModel> builder)
    {
        builder.HasKey(f => f.Id);
        builder.HasOne<UserModel>()
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(f => f.FriendId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}