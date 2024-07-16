using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Question
{
    public int QuestionId { get; set; }
    public Quiz Quiz { get; set; }
    public int QuizId { get; set; }
    [Required]
    public string Problem { get; set; } 
    [Required]
    public string Answer { get; set; }
}