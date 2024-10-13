using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApiMessage_Chat.Data;
using WebApiMessage_Chat.Models;

namespace WebApiMessage_Chat.Services.Message;

public class MessageServices : IMessageInterface
{
    private readonly AppDbContext _context;

    public MessageServices(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<List<MessageModel>>> Listar(int userId)
    {
        ResponseModel<List<MessageModel>> resposta = new ResponseModel<List<MessageModel>>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }

            var messages = await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId).ToListAsync();
            if (!messages.Any())
            {
                resposta.Mensagem = "Não foi encontrado nenhuma mensagem do usuário!!";
                return resposta;
            }
            
            resposta.Dados = messages;
            resposta.Mensagem = "Todos as mensagens do usuários foram coletadas com sucesso!!";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<MessageModel>> Enviar(int userId, int targetId, string content)
    {
        ResponseModel<MessageModel> resposta = new ResponseModel<MessageModel>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var target = await _context.Users.FirstOrDefaultAsync(u => u.Id == targetId);
            if (user == null)
            {
                resposta.Mensagem = "Usuário não encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }
            if (target == null)
            {
                resposta.Mensagem = "Usuário que deseja enviar a mensagem não foi encontrado, verifique o Id e tente novamente!!";
                return resposta;
            }

            var friend = await _context.Friends
                .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == targetId) || (f.UserId == targetId && f.FriendId == userId));

            if (friend == null)
            {
                resposta.Mensagem = "Você não pode enviar mensagem para um usuário que não é seu amigo!!";
                return resposta;
            }

            var newMessage = new MessageModel()
            {
                SenderId = userId,
                ReceiverId = targetId,
                Content = content
            };

            _context.Add(newMessage);
            await _context.SaveChangesAsync();
            
            resposta.Dados = newMessage;
            resposta.Mensagem = "Todos as mensagens do usuários foram coletadas com sucesso!!";
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