using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Request;

public class RequestService : IRequestInterface
{
    private readonly AppDbContext _context;

    public RequestService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseModel<List<RequestModel>>> Listar(int userId)
    {
        ResponseModel<List<RequestModel>> resposta = new ResponseModel<List<RequestModel>>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var requests = await _context.Requests
                .Where(r => r.RequestedId == userId || r.RequesterId == userId)
                .ToListAsync();
            if (!requests.Any())
            {
                resposta.Mensagem = "O usuário não possui nenhuma notificação!!";
                return resposta;
            }

            resposta.Dados = requests;
            resposta.Mensagem = "Todas notificações recuperadas com sucesso!!";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<RequestModel>> Aceitar(int userId, int amigoId)
    {
        ResponseModel<RequestModel> resposta = new ResponseModel<RequestModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == amigoId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja aceitar a solicitação não foi encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var request = await _context.Requests
                .FirstOrDefaultAsync(f => (f.RequesterId == amigoId && f.RequestedId == userId));
            
            if (request == null)
            {
                resposta.Mensagem = "Não foi encontrado uma solicitação de amizade do usuários mencionado, verifique os Id e tente novamente!";
                return resposta;
            }

            var newFriend = new FriendModel()
            {
                UserId = user.Id,
                FriendId = target.Id
            };
            
            _context.Remove(request);
            _context.Add(newFriend);
            await _context.SaveChangesAsync();
            
            resposta.Mensagem = "Amizade aceita com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<RequestModel>> Recusar(int userId, int amigoId)
    {
        ResponseModel<RequestModel> resposta = new ResponseModel<RequestModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == amigoId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja recusar a solicitação não foi encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var request = await _context.Requests
                .FirstOrDefaultAsync(f => (f.RequesterId == amigoId && f.RequestedId == userId));
            
            if (request == null)
            {
                resposta.Mensagem = "Não foi encontrado encontrado uma solicitação de amizade do o usuários mencionado, verifique os Id e tente novamente!";
                return resposta;
            }
            
            _context.Remove(request);
            await _context.SaveChangesAsync();
            
            resposta.Mensagem = "Amizade recusada com sucesso!!";
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