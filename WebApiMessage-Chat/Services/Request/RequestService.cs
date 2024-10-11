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
    
    public async Task<ResponseModel<RequestModel>> Adicionar(int userId, int targetId)
    {
         ResponseModel<RequestModel> resposta = new ResponseModel<RequestModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == targetId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja adicionar não foi encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var friendsTrue = await _context.Requests
                .FirstOrDefaultAsync(f => 
                    (f.RequesterId == userId && f.RequestedId == targetId) || 
                    (f.RequesterId == targetId && f.RequestedId == userId));

            if (friendsTrue != null)
            {
                resposta.Mensagem = "Já existe uma solicitação entre os usuários, você não pode enviar uma nova!!";
                return resposta;
            }

            
            var newRequest = new RequestModel()
            {
                RequesterId = userId,
                RequestedId = targetId,
            };

            await _context.Requests.AddAsync(newRequest);
            await _context.SaveChangesAsync();

            resposta.Dados = newRequest;
            resposta.Mensagem = "Solicitação enviada com sucesso!!";
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
                resposta.Mensagem = "Não foi encontrado encontrado uma solicitação de amizade do o usuários mencionado, verifique os Id e tente novamente!";
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