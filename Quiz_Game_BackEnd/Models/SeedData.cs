using Microsoft.EntityFrameworkCore;

namespace Quiz_Game_BackEnd.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {

        using (var _context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (_context.Games.Any())
            {
                return; // DB has been seeded
            }

            var games = new Game[]
            {
                new Game { GameType = "Test Game 1", PlayerId = 1, Score = 100 },
                new Game { GameType = "Test Game 2", PlayerId = 2, Score = 200 },
                new Game { GameType = "Test Game 3", PlayerId = 3, Score = 300 },
                new Game { GameType = "Test Game 4", PlayerId = 4, Score = 400 },
                new Game { GameType = "Test Game 5", PlayerId = 5, Score = 500 }
            };

            foreach (var game in games)
            {
                _context.Games.Add(game);
            }

            _context.SaveChanges();

            var difficulties = new string[] { "Easy", "Medium", "Hard", "Expert", "Master" };

            foreach (var game in games)
            {
                foreach (var difficulty in difficulties)
                {
                    var quiz = new Quiz { Difficulty = difficulty, GameId = game.GameId };
                    _context.Quizzes.Add(quiz);
                }
            }

            _context.SaveChanges();

            foreach (var quiz in _context.Quizzes)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var question = new Question
                        { Problem = $"Question {i}", Answer = $"Answer {i}", QuizId = quiz.QuizId };
                    _context.Questions.Add(question);
                }
            }

            _context.SaveChanges();
        }
    }

    public static void ClearDatabase(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<ApplicationDbContext>>()))
        {
            context.Questions.RemoveRange(context.Questions);
            context.Quizzes.RemoveRange(context.Quizzes);
            context.Games.RemoveRange(context.Games);
            context.SaveChanges();
        }
    }
}