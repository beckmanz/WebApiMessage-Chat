﻿using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
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
                .Where(f => f.UserId == userId || f.FriendId == userId)
                .ToListAsync();

            if (userFriends == null || !userFriends.Any())
            {
                resposta.Mensagem = "Nenhum amigo encontrado!";
                return resposta;
            }

            var friendIds = userFriends.Select(f => f.UserId == userId ? f.FriendId : f.UserId).ToList();

            var friends = await _context.Users
                .Where(u => friendIds.Contains(u.Id))
                .ToListAsync();

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

    public async Task<ResponseModel<FriendModel>> Remover(int userId, int amigoId)
    {
        ResponseModel<FriendModel> resposta = new ResponseModel<FriendModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }
            var friend = await _context.Users.FirstOrDefaultAsync(u => u.Id == amigoId);
            if (friend == null)
            {
                resposta.Mensagem = "O usuário que deseja remover da lista de amigos não existe, verifique o Id e tente novamente!";
                return resposta;
            }

            var userFriend = await _context.Friends
                .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == amigoId) || 
                                     (f.FriendId == userId && f.UserId == amigoId));
            if (userFriend == null)
            {
                resposta.Mensagem = "Amigo não encontrado, verifique o Id e tente novamente!";
                return resposta;
            }

            _context.Remove(userFriend);
            await _context.SaveChangesAsync();

            resposta.Dados = userFriend;
            resposta.Mensagem = "Amizade removida com sucesso!!";
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