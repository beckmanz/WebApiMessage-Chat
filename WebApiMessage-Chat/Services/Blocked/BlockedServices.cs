using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Blocked;

public class BlockedServices : IBlockedInterface
{
    private readonly AppDbContext _context;

    public BlockedServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<BlockedUserModel>>> Listar(int userId)
    {
        ResponseModel<List<BlockedUserModel>> resposta = new ResponseModel<List<BlockedUserModel>>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }

            var blockeds = await _context.Blokeds.Where(b => b.UserId == userId).ToListAsync();
            if (!blockeds.Any())
            {
                resposta.Mensagem = "Não foi encontrado nenhum usuário bloqueado!";
                return resposta;
            }

            resposta.Dados = blockeds;
            resposta.Mensagem = "Todos os usuários bloqueados listados com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<UserModel>> Bloquear(int userId, int blockedId)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == blockedId);
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
            
            var blocked = _context.Blokeds.Where(b => 
                b.UserId == userId && b.BlockedUserId == blockedId);

            if (blocked.Any())
            {
                resposta.Mensagem = "Este usuário já é bloqueado por você!!";
                return resposta;
            }
            
            var friend = _context.Friends.Where(f => 
                (f.UserId == userId && f.FriendId == blockedId) || 
                (f.UserId == blockedId && f.FriendId == userId));
            
            if (!friend.Any())
            {
                resposta.Mensagem = "Você não pode bloquear um usuário que não é seu amigo!!";
                return resposta;
            }

            var block = new BlockedUserModel()
            {
                UserId = userId,
                BlockedUserId = blockedId
            };

            _context.Remove(friend);
            _context.Add(block);
            await _context.SaveChangesAsync();

            resposta.Dados = target;
            resposta.Mensagem = "Usuário bloqueado com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public Task<ResponseModel<UserModel>> Desbloquear(int userId, int BlockedId)
    {
        throw new NotImplementedException();
    }
}