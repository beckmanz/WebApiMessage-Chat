using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Message;

public interface IMessageInterface
{
    Task<ResponseModel<List<MessageModel>>> Listar(int userId);
}