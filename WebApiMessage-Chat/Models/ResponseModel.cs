namespace WebApiMessage_Chat.Models;

public class ResponseModel<T>
{
    public string Mensagem { get; set; } = string.Empty;
    public Boolean Status { get; set; } = true;
    public T? Dados { get; set; }
}