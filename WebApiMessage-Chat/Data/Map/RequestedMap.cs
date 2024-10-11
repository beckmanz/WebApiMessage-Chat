using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Data.Map;

public class RequestedMap : IEntityTypeConfiguration<RequestModel>
{
    public void Configure(EntityTypeBuilder<RequestModel> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.HasOne<UserModel>()
            .WithMany(u => u.Requests)
            .HasForeignKey(f=> f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(f=> f.RequestedId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}