using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.User;

public interface IUserInterface
{
    Task<ResponseModel<UserModel>> Registrar(string Username, string Email, string Password);
    Task<ResponseModel<UserModel>> Login(string Email, string Password);
    Task<ResponseModel<List<UserModel>>> Listar();
}