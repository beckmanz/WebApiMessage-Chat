using System.Security.Claims;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Message;

public interface IMessageInterface
{
    Task<ResponseModel<List<MessageModel>>> Listar(ClaimsPrincipal userClaims);
    Task<ResponseModel<MessageModel>> Enviar(int targetId, string content, ClaimsPrincipal userClaims);
    Task<ResponseModel<MessageModel>> Excluir(int messageId, ClaimsPrincipal userClaims);
}