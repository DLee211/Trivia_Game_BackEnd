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
        return _context.Questions.Include(q => q.Quiz).FirstOrDefault(q => q.QuestionId == id);
    }

    public List<Question> GetQuestionByQuizId(int id)
    {
        return _context.Questions.Where(q => q.QuizId == id).ToList();
    }

    public Question AddQuestion(QuestionAddDto questionDto)
    {
        // Find the quiz with the given gameId and difficulty, or create a new one if it doesn't exist
        var quiz = _context.Quizzes.FirstOrDefault(q => q.GameId == questionDto.GameId && q.Difficulty.ToLower() == questionDto.Difficulty.ToLower())
                   ?? new Quiz { GameId = questionDto.GameId, Difficulty = questionDto.Difficulty };
    
        // If the quiz is new, add it to the context
        if (quiz.QuizId == 0)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges(); // Save changes to get the QuizId for the new quiz
        }

        // Create a new question object from the DTO and associate it with the quiz
        var question = new Question
        {
            Problem = questionDto.Problem,
            Answer = questionDto.Answer,
            QuizId = quiz.QuizId
        };
        
        _context.Questions.Add(question);
        _context.SaveChanges();

        return question;
    }
    
    public Quiz GetQuizByGameIdAndDifficulty(int gameId, string difficulty)
    {
        return _context.Quizzes.FirstOrDefault(q => q.GameId == gameId && q.Difficulty.ToLower() == difficulty.ToLower());
    }

    public Quiz CreateQuiz(int gameId, string difficulty)
    {
        var quiz = new Quiz { GameId = gameId, Difficulty = difficulty };
        _context.Quizzes.Add(quiz);
        _context.SaveChanges();
        return quiz;
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
        var question = GetQuestionById(questionId);
        return question.Quiz.GameId;
    }
}