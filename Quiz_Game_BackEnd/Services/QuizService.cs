using Microsoft.AspNetCore.Mvc;
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
        return _context.Quizzes.ToList();
    }
    
    public List<QuizDTO> GetQuizzesByGameId(int gameId)
    {
        var quizzes = _context.Quizzes
            .Include(q => q.Questions) // Ensure Questions are eagerly loaded
            .Where(q => q.GameId == gameId)
            .Select(q => new QuizDTO
            {
                QuizId = q.QuizId,
                Difficulty = q.Difficulty,
                GameId = q.GameId,
                Questions = q.Questions.Select(question => new QuestionDTO
                {
                    QuestionId = question.QuestionId,
                    Problem = question.Problem,
                    Answer = question.Answer
                }).ToList()
            }).ToList();
        return quizzes;
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

    public ActionResult<Quiz> GetQuizById(int id)
    {
        var quiz = _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefault(q => q.QuizId == id);

        return quiz;
    }
}