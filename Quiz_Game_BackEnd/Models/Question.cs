using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Question
{
    public int QuestionId { get; set; }
    public int QuizId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Question cannot be longer than 100 characters.")]
    public string Text { get; set; } 
    [Required]
    [StringLength(100, ErrorMessage = "Answer cannot be longer than 100 characters.")]
    public string Answer { get; set; } 
}