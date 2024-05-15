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

    public Question GetQuestionById(int id)
    {
        return _context.Questions.FirstOrDefault(q => q.QuestionId == id);
    }

    public Question AddQuestion(Question question)
    {
        _context.Questions.Add(question);
        _context.SaveChanges();
        return question;
    }

    public Question UpdateQuestion(int id, Question question)
    {
        _context.Entry(question).State = EntityState.Modified;
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
}