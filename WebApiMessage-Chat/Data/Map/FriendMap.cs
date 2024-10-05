using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data.Map;

public class FriendMap : IEntityTypeConfiguration<FriendModel>
{
    public void Configure(EntityTypeBuilder<FriendModel> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Status).IsRequired();
        builder.Property(f => f.DateAccepted).IsRequired(false);
        
        builder.HasOne<UserModel>()
            .WithMany(u => u.Friends)
            .HasForeignKey(f=> f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(f=> f.AddresseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}