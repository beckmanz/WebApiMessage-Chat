using WebApiMessage_Chat.Enums;

namespace WebApiMessage_Chat.Models;

public class FriendModel
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public int ReceiverId { get; set; }
    public FriendRequestStatus Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime RespondedAt { get; set; }
}