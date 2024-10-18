using System.Security.Claims;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Request;

public interface IRequestInterface
{
    Task<ResponseModel<List<RequestModel>>> Listar(ClaimsPrincipal userClaims);
    Task<ResponseModel<RequestModel>> Aceitar(int amigoId, ClaimsPrincipal userClaims);
    Task<ResponseModel<RequestModel>> Recusar(int amigoId, ClaimsPrincipal userClaims);
    
}