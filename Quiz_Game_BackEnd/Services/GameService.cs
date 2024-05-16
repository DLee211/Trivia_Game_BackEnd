using Microsoft.EntityFrameworkCore;
using Quiz_Game_BackEnd.Models;

namespace Quiz_Game_BackEnd.Services;

public class GameService
{
    private readonly ApplicationDbContext _context;
    
    public GameService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<Game> GetAllGames()
    { 
        return _context.Games
            .Include(g => g.Quiz)
            .ThenInclude(q => q.Questions)
            .ToList();
    }

    public Game GetGameById(int id)
    {
        return _context.Games.FirstOrDefault(g => g.GameId == id);
    }
    
    public Game AddGame(Game game)
    {
        _context.Games.Add(game);
        _context.SaveChanges();
        return game;
    }
    
    public Game UpdateGame(Game game)
    {
        var existingGame = _context.Games.FirstOrDefault(g => g.GameId == game.GameId);
        if (existingGame != null)
        {
            existingGame.GameType = game.GameType;
            existingGame.PlayerId = game.PlayerId;
            existingGame.Score = game.Score;
            _context.SaveChanges();
        }
        return game;
    }

    public void DeleteGame(int id)
    {
        var game = _context.Games.FirstOrDefault(g => g.GameId == id);
        if (game != null)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }
}