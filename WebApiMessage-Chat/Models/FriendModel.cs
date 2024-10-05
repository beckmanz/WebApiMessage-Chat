using WebApiMessage_Chat.Enums;

namespace WebApiMessage_Chat.Models;

public class FriendModel
{
    public int Id { get; set; }
    public int RequesterId { get; set; }
    public int AddresseId { get; set; }
    public FriendRequestStatus Status { get; set; }
    public DateTime DateRequested { get; set; } = DateTime.Now;
    public DateTime? DateAccepted { get; set; }
}