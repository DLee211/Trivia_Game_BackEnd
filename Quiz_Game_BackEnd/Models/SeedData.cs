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
            return;
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

        _context.SaveChanges(); // Ensure the Games are saved first

        var difficulties = new string[] { "Easy", "Medium", "Hard"};

        var quizzes = new List<Quiz>();
        foreach (var game in games)
        {
            foreach (var difficulty in difficulties)
            {
                var quiz = new Quiz { Difficulty = difficulty, GameId = game.GameId };
                quizzes.Add(quiz);
                _context.Quizzes.Add(quiz);
            }
        }

        _context.SaveChanges();

        var questions = new List<Question>();
        foreach (var quiz in quizzes)
        {
            string filePath = Path.Combine("TriviaQuestions", quiz.Game.GameType, $"{quiz.Difficulty}.txt");
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        var question = new Question
                        {
                            Problem = parts[0].Trim(),
                            Answer = parts[1].Trim(),
                            QuizId = quiz.QuizId
                        };
                        questions.Add(question);
                        _context.Questions.Add(question);
                    }
                }
            }
        }

        _context.SaveChanges(); // Ensure the Questions are saved
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