namespace WebApiMessage_Chat.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Usename { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }
    public ICollection<FriendModel> Friends { get; set; }
    public ICollection<MessageModel> MessagesSent { get; set; }
    public ICollection<MessageModel> MessagesReceived { get; set; }
}