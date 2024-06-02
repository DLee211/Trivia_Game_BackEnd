using Microsoft.EntityFrameworkCore;
using Quiz_Game_BackEnd.Models;

namespace Quiz_Game_BackEnd.Services;

public class QuizService
{
    private readonly ApplicationDbContext _context;
    
    public QuizService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<Quiz> GetAllQuizzes()
    {
        return _context.Quizzes.Include(q => q.Questions).ToList();
    }
    
    public Quiz GetQuizById(int id)
    {
        return _context.Quizzes.FirstOrDefault(q => q.GameId == id);
    }
    
    public Quiz AddQuiz(Quiz quiz)
    {
        Console.WriteLine("AddQuiz called with QuizId: " + quiz.QuizId); // Add logging
        _context.Quizzes.Add(quiz);
        _context.SaveChanges();
        return quiz;
    }
    
    public Quiz UpdateQuiz(Quiz quiz)
    {
        var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == quiz.QuizId);
        if (existingQuiz != null)
        {
            existingQuiz.Difficulty = quiz.Difficulty;
            _context.SaveChanges();
        }
        return quiz;
    }
    
    public void DeleteQuiz(int id)
    {
        var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == id);
        if (quiz != null)
        {
            _context.Quizzes.Remove(quiz);
            _context.SaveChanges();
        }
    }
}