using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Game
{
    public int GameId { get; set; }
    [Required]
    public string GameType { get; set; } 
    [Required]
    public int PlayerId { get; set; }
    [Required]
    public int Score { get; set; }
    public List<Quiz> Quiz { get; set; }
}