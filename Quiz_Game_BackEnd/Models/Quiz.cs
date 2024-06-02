using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Quiz
{
    public int QuizId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Level Name cannot be longer than 20 characters.")]
    public string Difficulty { get; set; } // New property
    public int GameId { get; set; }
    public List<Question> Questions { get; set; }
}