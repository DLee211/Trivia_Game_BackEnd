using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Game
{
    public int GameId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "GameType cannot be longer than 20 characters.")]
    public string GameType { get; set; } 
    [Required]
    public int Score { get; set; }
}