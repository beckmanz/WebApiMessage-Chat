using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.User;

public interface IUserInterface
{
    Task<ResponseModel<List<UserModel>>> Listar();
}