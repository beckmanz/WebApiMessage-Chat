using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data.Map;

public class MessageMap : IEntityTypeConfiguration<MessageModel>
{
    public void Configure(EntityTypeBuilder<MessageModel> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Content)
            .IsRequired();

        builder.Property(m => m.IsRead)
            .HasDefaultValue(false);
        
        builder.HasOne<UserModel>()
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<UserModel>()
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}