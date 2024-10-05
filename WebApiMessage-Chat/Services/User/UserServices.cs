using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.User;

public class UserServices : IUserInterface
{
    private readonly AppDbContext _context;

    public UserServices( AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<List<UserModel>>> Listar()
    {
        ResponseModel<List<UserModel>> resposta = new ResponseModel<List<UserModel>>();
        try
        {
            var users = await _context.Users.ToListAsync();

            resposta.Dados = users;
            resposta.Mensagem = "Todos os usuários foram coletados!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}