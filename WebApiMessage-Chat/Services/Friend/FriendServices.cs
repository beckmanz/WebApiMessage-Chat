using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Enums;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Friend;

public class FriendServices : IFriendInterface
{
    readonly private AppDbContext _context;

    public FriendServices(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<List<UserModel>>> Amigos(int userId)
    {
        ResponseModel<List<UserModel>> resposta = new ResponseModel<List<UserModel>>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
        
            var userFriends = await _context.Friends
                .Where(f => (f.RequesterId == userId || f.AddresseId == userId) && f.Status == FriendRequestStatus.Accepted)
                .ToListAsync();

            if (userFriends == null || !userFriends.Any())
            {
                resposta.Mensagem = "Nenhum amigo encontrado!";
                return resposta;
            }

            var friendIds = userFriends.Select(f => f.RequesterId == userId ? f.AddresseId : f.RequesterId).ToList();

            var friends = await _context.Users
                .Where(u => friendIds.Contains(u.Id))
                .ToListAsync();

            if (friends == null || !friends.Any())
            {
                resposta.Mensagem = "Nenhum amigo encontrado!";
                return resposta;
            }

            resposta.Dados = friends;
            resposta.Mensagem = "Lista de amigos encontrada!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<FriendModel>> Adicionar(int userId, int targetId)
    {
        ResponseModel<FriendModel> resposta = new ResponseModel<FriendModel>();
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
            
            var friendsTrue = await _context.Friends
                .FirstOrDefaultAsync(f => 
                    (f.RequesterId == userId && f.AddresseId == targetId) || 
                    (f.RequesterId == targetId && f.AddresseId == userId));

            if (friendsTrue != null)
            {
                if (friendsTrue.Status == FriendRequestStatus.Pending)
                {
                    resposta.Mensagem = "Já existe uma solicitação entre os usuários, você não pode enviar uma nova!!";
                    return resposta;
                }
                if (friendsTrue.Status == FriendRequestStatus.Accepted)
                {
                    resposta.Mensagem = "Você já é amigo desse usuário!!";
                    return resposta;
                }
                if (friendsTrue.Status == FriendRequestStatus.Blocked)
                {
                    resposta.Mensagem = "Você não pode adicionar esse usuário pois ele te bloqueou!!";
                    return resposta;
                }
            }

            
            var newFriends = new FriendModel()
            {
                RequesterId = userId,
                AddresseId = targetId,
                Status = FriendRequestStatus.Pending
            };

            await _context.Friends.AddAsync(newFriends);
            await _context.SaveChangesAsync();

            resposta.Dados = newFriends;
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
}