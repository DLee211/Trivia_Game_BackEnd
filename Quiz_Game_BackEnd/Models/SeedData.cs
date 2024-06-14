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
                new Game { GameType = "Science and Technology", Score = 0 },
                new Game { GameType = "Geography and Landmarks", Score = 0 },
                new Game { GameType = "People and Events", Score = 0 },
                new Game { GameType = "Sports and Recreation", Score = 0 },
                new Game { GameType = "Arts and Culture", Score = 0 }
            };

            foreach (var game in games)
            {
                _context.Games.Add(game);
            }

            _context.SaveChanges();

            var difficulties = new string[] { "Easy", "Medium", "Hard"};

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
                for (int i = 1; i <= 20; i++)
                {
                    var question = new Question
                    {
                        Problem = $"Question {i} for {quiz.Difficulty} difficulty",
                        Answer = $"Answer {i}",
                        QuizId = quiz.QuizId
                    };
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