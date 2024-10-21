using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Token;

public interface ITokenInterface
{
    Task<ResponseModel<LoginResponseModel>> GenerateToken(LoginDto login);
}