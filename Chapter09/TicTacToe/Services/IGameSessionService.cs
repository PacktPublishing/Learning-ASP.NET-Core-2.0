using System;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameSessionService
    {
        Task<GameSessionModel> AddTurn(Guid id, UserModel user, int x, int y);
        Task<GameSessionModel> CreateGameSession(Guid invitationId, UserModel invitedBy, UserModel invitedPlayer);
        Task<GameSessionModel> GetGameSession(Guid gameSessionId);
    }
}