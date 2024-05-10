using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Game
{
    public int GameId { get; set; }
    public string GameType { get; set; } 
    public int PlayerId { get; set; }
    public int Score { get; set; }
    public List<Quiz> Quiz { get; set; }
}