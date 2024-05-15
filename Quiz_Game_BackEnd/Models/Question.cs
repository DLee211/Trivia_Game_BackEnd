using System.ComponentModel.DataAnnotations;

namespace Quiz_Game_BackEnd.Models;

public class Question
{
    public int QuestionId { get; set; }
    [Required]
    public string Text { get; set; } 
    [Required]
    public string Answer { get; set; } 
    public Quiz Quiz { get; set; } 
}