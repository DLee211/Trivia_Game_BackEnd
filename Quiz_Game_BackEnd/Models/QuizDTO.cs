namespace Quiz_Game_BackEnd.Models;

public class QuizDTO
{
    public int QuizId { get; set; }
    public string Difficulty { get; set; }
    public int GameId { get; set; }
    public ICollection<QuestionDTO> Questions { get; set; }
}