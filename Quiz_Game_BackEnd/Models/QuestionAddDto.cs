namespace Quiz_Game_BackEnd.Models;

public class QuestionAddDto
{
    public int GameId { get; set; }
    public string Difficulty { get; set; }
    public string Problem { get; set; }
    public string Answer { get; set; }
}