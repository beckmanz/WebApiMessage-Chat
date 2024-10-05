namespace WebApiMessage_Chat.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Usename { get; set; }
    public string Email { get; set; }
    public string PasswordHas { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}