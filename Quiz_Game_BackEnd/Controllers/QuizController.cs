using Microsoft.AspNetCore.Mvc;
using Quiz_Game_BackEnd.Models;
using Quiz_Game_BackEnd.Services;

namespace Quiz_Game_BackEnd.Controllers;
[ApiController]
[Route("[controller]")]
public class QuizController:ControllerBase
{
    private readonly QuizService _quizService;
    
    public QuizController(QuizService quizService)
    {
        _quizService = quizService;
    }
    
    [HttpGet]
    public ActionResult<List<Quiz>> GetAllQuizzes()
    {
        return _quizService.GetAllQuizzes();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Quiz> GetQuizById(int id)
    {
        var quiz = _quizService.GetQuizById(id);
        if (quiz == null)
        {
            return NotFound();
        }
        return quiz;
    }
    
    [HttpPost]
    public ActionResult<Quiz> AddQuiz(Quiz quiz)
    {
        quiz.Questions = null; // Ensure that Questions is null
        var createdQuiz = _quizService.AddQuiz(quiz);
        return CreatedAtAction(nameof(GetQuizById), new { id = createdQuiz.QuizId }, createdQuiz);
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateQuiz(int id, Quiz quiz)
    {
        quiz.QuizId = id;
        var existingQuiz = _quizService.GetQuizById(id);
        if (existingQuiz == null)
        {
            return BadRequest();
        }
        _quizService.UpdateQuiz(quiz);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteQuiz(int id)
    {
        _quizService.DeleteQuiz(id);
        return NoContent();
    }
}