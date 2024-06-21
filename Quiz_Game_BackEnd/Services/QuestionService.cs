using Microsoft.EntityFrameworkCore;
using Quiz_Game_BackEnd.Models;

namespace Quiz_Game_BackEnd.Services;

public class QuestionService
{
    private readonly ApplicationDbContext _context;

    public QuestionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Question> GetQuestions()
    {
        return _context.Questions.ToList();
    }

    public List<Question> GetQuestionById(int id)
    {
        return _context.Questions.Where(q => q.QuizId == id).ToList();
    }

    public Question AddQuestion(Question question)
    {
        _context.Questions.Add(question);
        _context.SaveChanges();
        return question;
    }

    public Question UpdateQuestion(int id, Question question)
    {
        var existingQuestion = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
        if (existingQuestion != null)
        {
            existingQuestion.Problem = question.Problem;
            existingQuestion.Answer = question.Answer;
            existingQuestion.QuizId = question.QuizId;
            _context.SaveChanges();
        }
        _context.SaveChanges();
        return question;
    }

    public void DeleteQuestion(int id)
    {
        var question = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }
    }
    
    public int GetGameIdByQuestionId(int questionId)
    {
        var question = _context.Questions.Include(q => q.Quiz).FirstOrDefault(q => q.QuestionId == questionId);
        return question.Quiz.GameId;
    }
}