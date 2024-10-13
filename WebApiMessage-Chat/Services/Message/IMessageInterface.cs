using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Message;

public interface IMessageInterface
{
    Task<ResponseModel<List<MessageModel>>> Listar(int userId);
    Task<ResponseModel<MessageModel>> Enviar(int userId, int targetId, string content);
    Task<ResponseModel<MessageModel>> Excluir(int userId, int messageId);
}