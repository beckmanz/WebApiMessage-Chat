using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
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

    public async Task<ResponseModel<List<BlockedUserModel>>> Listar(ClaimsPrincipal userClaims)
    {
        ResponseModel<List<BlockedUserModel>> resposta = new ResponseModel<List<BlockedUserModel>>();
        try
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                resposta.Mensagem = "Token inválido, userId inválido.";
                return resposta;
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }

            var blockeds = await _context.Blockeds.Where(b => b.UserId == userId).ToListAsync();
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

    public async Task<ResponseModel<UserModel>> Bloquear(int blockedId, ClaimsPrincipal userClaims)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                resposta.Mensagem = "Token inválido, userId inválido.";
                return resposta;
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == blockedId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja bloquear não foi encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var blocked = await _context.Blockeds.FirstOrDefaultAsync(b => 
                b.UserId == userId && b.BlockedUserId == blockedId);

            if (blocked != null)
            {
                resposta.Mensagem = "Este usuário já é bloqueado por você!!";
                return resposta;
            }
            
            var friend = await _context.Friends.FirstOrDefaultAsync(f => 
                (f.UserId == userId && f.FriendId == blockedId) || 
                (f.UserId == blockedId && f.FriendId == userId));
            
            if (friend == null)
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

    public async Task<ResponseModel<UserModel>> Desbloquear(int blockedId, ClaimsPrincipal userClaims)
    {
        ResponseModel<UserModel> resposta = new ResponseModel<UserModel>();
        try
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                resposta.Mensagem = "Token inválido, userId inválido.";
                return resposta;
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == blockedId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja desbloquear não foi encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            
            var blocked = await _context.Blockeds.FirstOrDefaultAsync(b => 
                b.UserId == userId && b.BlockedUserId == blockedId);

            if (blocked == null)
            {
                resposta.Mensagem = "Este usuário não é bloqueado por você!!";
                return resposta;
            }

            _context.Remove(blocked);
            await _context.SaveChangesAsync();

            resposta.Dados = target;
            resposta.Mensagem = "Usuário desbloqueado com sucesso!!";
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