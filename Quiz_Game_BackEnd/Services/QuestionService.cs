﻿using Microsoft.EntityFrameworkCore;
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

    public QuestionAddDto AddQuestion(QuestionAddDto questionDto)
    {
        var quiz = _context.Quizzes.FirstOrDefault(q => q.GameId == questionDto.GameId && q.Difficulty.ToLower() == questionDto.Difficulty.ToLower())
                   ?? new Quiz { GameId = questionDto.GameId, Difficulty = questionDto.Difficulty };
    
        if (quiz.QuizId == 0)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges(); // Save changes to get the QuizId for the new quiz
        }

        var question = new Question
        {
            Problem = questionDto.Problem,
            Answer = questionDto.Answer,
            QuizId = quiz.QuizId
        };
        
        _context.Questions.Add(question);
        _context.SaveChanges();
        return questionDto;
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

    public QuestionAddDto UpdateQuestion(int id, QuestionAddDto questionDto)
    {
        var existingQuestion = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
        if (existingQuestion != null)
        {
            existingQuestion.Problem = questionDto.Problem;
            existingQuestion.Answer = questionDto.Answer;
            
            // Check if a Quiz with the new difficulty already exists
            var quiz = _context.Quizzes.FirstOrDefault(q => q.GameId == existingQuestion.Quiz.GameId && q.Difficulty.ToLower() == questionDto.Difficulty.ToLower());

            if (quiz == null)
            {
                // If not, create a new Quiz with the new difficulty
                quiz = new Quiz { GameId = existingQuestion.Quiz.GameId, Difficulty = questionDto.Difficulty };
                _context.Quizzes.Add(quiz);
                _context.SaveChanges();
            }

            // Update the question's QuizId to the correct Quiz
            existingQuestion.QuizId = quiz.QuizId;

            _context.SaveChanges();
        }
        return questionDto;
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
    
    public int? GetGameIdByQuestionId(int questionId)
    {
        var question = GetQuestionById(questionId);
        if (question == null)
        {
            // Return null or a default value indicating that the question was not found
            return null;
        }
        return question.Quiz.GameId;
    }
}