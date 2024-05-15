using Microsoft.AspNetCore.Mvc;
using Quiz_Game_BackEnd.Models;
using Quiz_Game_BackEnd.Services;

namespace Quiz_Game_BackEnd.Controllers;
[ApiController]
[Route("[controller]")]

public class GameController:ControllerBase
{
    private readonly GameService _gameService;
    
    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }
    
    [HttpGet]
    public ActionResult<List<Game>> GetAllGames()
    {
        return _gameService.GetAllGames();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Game> GetGameById(int id)
    {
        var game = _gameService.GetGameById(id);
        if (game == null)
        {
            return NotFound();
        }
        return game;
    }
    
    [HttpPost]
    public ActionResult<Game> AddGame(Game game)
    {
        var createdGame = _gameService.AddGame(game);
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.GameId }, createdGame);
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateGame(int id, Game game)
    {
        game.GameId = id;
        var existingGame = _gameService.GetGameById(id);
        if (existingGame == null)
        {
            return BadRequest();
        }
        _gameService.UpdateGame(game);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteGame(int id)
    {
        _gameService.DeleteGame(id);
        return NoContent();
    }
}