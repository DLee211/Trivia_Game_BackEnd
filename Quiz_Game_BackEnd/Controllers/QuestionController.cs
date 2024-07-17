using Microsoft.AspNetCore.Mvc;
using Quiz_Game_BackEnd.Models;
using Quiz_Game_BackEnd.Services;

namespace Quiz_Game_BackEnd.Controllers;
[ApiController]
[Route("[controller]")]

public class QuestionController:ControllerBase
{
    private readonly QuestionService _questionService;
    
    public QuestionController(QuestionService questionService)
    {
        _questionService = questionService;
    }
    
    [HttpGet]
    public ActionResult<List<Question>> GetAllQuestions()
    {
        return _questionService.GetQuestions();
    }

    [HttpGet("{id}")]
    public ActionResult<List<Question>> GetQuestionById(int id)
    {
        var question = _questionService.GetQuestionByQuizId(id);
        if (question == null)
        {
            return NotFound();
        }
        return question;
    }
    
    [HttpGet("{questionId}/gameId")]
    public ActionResult<int?> GetGameIdByQuestionId(int questionId)
    {
        var gameId = _questionService.GetGameIdByQuestionId(questionId);
        if (gameId == null)
        {
            return NotFound();
        }
        return gameId;
    }
    
    [HttpPost]
    public ActionResult<QuestionAddDto> AddQuestion([FromBody] QuestionAddDto questionDto)
    { 
        _questionService.AddQuestion(questionDto);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateQuestion(int id, [FromBody] QuestionAddDto questionDto)
    {
        var existingQuestion = _questionService.GetQuestionById(id);
        if (existingQuestion == null)
        {
            return NotFound();
        }
        _questionService.UpdateQuestion(id, questionDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteQuestion(int id)
    {
        _questionService.DeleteQuestion(id);
        return NoContent();
    }
}