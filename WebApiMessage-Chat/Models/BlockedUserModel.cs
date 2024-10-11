namespace WebApiMessage_Chat.Models;

public class BlockedUserModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BlockedUserId { get; set; }
    public DateTime DateBlocked { get; set; } = DateTime.Now;
}