using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Friend;

public interface IFriendInterface
{
    Task<ResponseModel<RequestModel>> Adicionar(int userId, int targetId);
    Task<ResponseModel<List<UserModel>>> Amigos(int userId);
    Task<ResponseModel<FriendModel>> Remover(int userId, int amigoId);

}