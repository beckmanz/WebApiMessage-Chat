using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Request;

public interface IRequestInterface
{
    Task<ResponseModel<RequestModel>> Adicionar(int userId, int targetId);
    Task<ResponseModel<RequestModel>> Aceitar(int userId, int amigoId);
    Task<ResponseModel<RequestModel>> Recusar(int userId, int amigoId);
}