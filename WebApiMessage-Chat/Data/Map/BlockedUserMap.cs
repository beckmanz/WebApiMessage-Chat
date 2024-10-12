using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data.Map;

public class BlockedUserMap : IEntityTypeConfiguration<BlockedUserModel>
{
    public void Configure(EntityTypeBuilder<BlockedUserModel> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne<UserModel>()
            .WithMany(u => u.Blockeds)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(b => b.BlockedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}