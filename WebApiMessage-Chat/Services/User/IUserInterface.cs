using System.Security.Claims;
using WebApiMessage_Chat.Dto.User;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.User;
public interface IUserInterface
{
    Task<ResponseModel<UserModel>> Registrar(RegisterDto register);
    Task<ResponseModel<List<UserModel>>> Listar();
    Task<ResponseModel<UserModel>> Buscar(int UserId);
    Task<ResponseModel<UserModel>> Editar(EditarDto editarDto, ClaimsPrincipal userClaims);
    Task<ResponseModel<UserModel>> Excluir(ClaimsPrincipal userClaims);
}