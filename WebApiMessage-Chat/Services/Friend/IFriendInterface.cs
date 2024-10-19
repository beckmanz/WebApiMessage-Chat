using System.Security.Claims;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Friend;

public interface IFriendInterface
{
    Task<ResponseModel<RequestModel>> Adicionar(int targetId, ClaimsPrincipal userClaims);
    Task<ResponseModel<List<UserModel>>> Amigos(ClaimsPrincipal userClaims);
    Task<ResponseModel<FriendModel>> Remover(int amigoId, ClaimsPrincipal userClaims);

}