using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Quiz
{
    public int QuizId { get; set; }
    [Required]
    public string Level { get; set; } // New property
    public Game Game { get; set; }
    public List<Question> Questions { get; set; }
}