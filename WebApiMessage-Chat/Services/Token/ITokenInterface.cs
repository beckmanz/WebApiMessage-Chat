using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Token;

public interface ITokenInterface
{
    Task<ResponseModel<LoginResponseModel>> GenerateToken(string Email, string Password);
}