using System.Security.Claims;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Blocked;

public interface IBlockedInterface
{
    Task<ResponseModel<List<BlockedUserModel>>> Listar(ClaimsPrincipal userClaims);
    Task<ResponseModel<UserModel>> Bloquear(int blockedId, ClaimsPrincipal userClaims);
    Task<ResponseModel<UserModel>> Desbloquear(int blockedId, ClaimsPrincipal userClaims);
}