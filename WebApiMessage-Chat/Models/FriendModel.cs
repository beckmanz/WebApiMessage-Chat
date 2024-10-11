namespace WebApiMessage_Chat.Models;

public class FriendModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FriendId { get; set; }
    public DateTime DateConfirmed { get; set; } = DateTime.Now;
}