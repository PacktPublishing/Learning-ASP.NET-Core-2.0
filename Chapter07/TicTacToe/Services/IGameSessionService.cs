﻿using System;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IGameSessionService
    {
        Task<GameSessionModel> AddTurn(Guid id, string email, int x, int y);
        Task<GameSessionModel> CreateGameSession(Guid invitationId, string invitedByEmail, string invitedPlayerEmail);
        Task<GameSessionModel> GetGameSession(Guid gameSessionId);
    }
}