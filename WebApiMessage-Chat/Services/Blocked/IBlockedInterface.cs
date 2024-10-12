using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Blocked;

public interface IBlockedInterface
{
    Task<ResponseModel<List<BlockedUserModel>>> Listar(int userId);
    Task<ResponseModel<UserModel>> Bloquear(int userId, int blockedId);
    Task<ResponseModel<UserModel>> Desbloquear(int userId, int BlockedId);
}