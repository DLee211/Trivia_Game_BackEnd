namespace Quiz_Game_BackEnd.Models;

public class Question
{
    public int QuestionId { get; set; }
    public string Text { get; set; } 
    public string Answer { get; set; } 
    public Quiz Quiz { get; set; } 
}