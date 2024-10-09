using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Friend;

public interface IFriendInterface
{
    Task<ResponseModel<List<UserModel>>> Amigos(int userId);
    Task<ResponseModel<FriendModel>> Adicionar(int userId, int targetId);
}