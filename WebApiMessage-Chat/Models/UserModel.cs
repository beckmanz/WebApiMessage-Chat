using System.Text.Json.Serialization;

namespace WebApiMessage_Chat.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }
    [JsonIgnore] 
    public ICollection<FriendModel> Friends { get; set; }
    [JsonIgnore] 
    public ICollection<BlockedUserModel> Blockeds { get; set; }
    [JsonIgnore]
    public ICollection<RequestModel> Requests { get; set; }
    [JsonIgnore]
    public ICollection<MessageModel> MessagesSent { get; set; }
    [JsonIgnore]
    public ICollection<MessageModel> MessagesReceived { get; set; }
}