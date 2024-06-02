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
                        return;   // DB has been seeded
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
            
                    var quizzes = new Quiz[]
                    {
                        new Quiz { Difficulty = "Easy", GameId = games[0].GameId },
                        new Quiz { Difficulty = "Medium", GameId = games[1].GameId },
                        new Quiz { Difficulty = "Hard", GameId = games[2].GameId },
                        new Quiz { Difficulty = "Expert", GameId = games[3].GameId },
                        new Quiz { Difficulty = "Master", GameId = games[4].GameId }
                    };
            
                    foreach (var quiz in quizzes)
                    {
                        _context.Quizzes.Add(quiz);
                    }
            
                    _context.SaveChanges();
            
                    var questions = new Question[]
                    {
                        new Question { Text = "Question 1", Answer = "Answer 1", QuizId = quizzes[0].QuizId },
                        new Question { Text = "Question 2", Answer = "Answer 2", QuizId = quizzes[1].QuizId },
                        new Question { Text = "Question 3", Answer = "Answer 3", QuizId = quizzes[2].QuizId },
                        new Question { Text = "Question 4", Answer = "Answer 4", QuizId = quizzes[3].QuizId },
                        new Question { Text = "Question 5", Answer = "Answer 5", QuizId = quizzes[4].QuizId }
                    };
            
                    foreach (var question in questions)
                    {
                        _context.Questions.Add(question);
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