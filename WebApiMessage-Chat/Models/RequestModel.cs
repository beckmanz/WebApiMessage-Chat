namespace WebApiMessage_Chat.Models;

public class RequestModel
{
    public int Id { get; set; }
    public int RequesterId { get; set; }
    public int RequestedId { get; set; }
    public DateTime DateRequested { get; set; } = DateTime.Now;
}