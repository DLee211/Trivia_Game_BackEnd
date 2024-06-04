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
        var question = _questionService.GetQuestionById(id);
        if (question == null)
        {
            return NotFound();
        }
        return question;
    }
    
    [HttpPost]
    public ActionResult<Question> AddQuestion(Question question)
    {
        var createdQuestion = _questionService.AddQuestion(question);
        return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.QuestionId }, createdQuestion);
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateQuestion(int id, Question question)
    {
        question.QuestionId = id;
        var existingQuestion = _questionService.GetQuestionById(id);
        if (existingQuestion == null)
        {
            return BadRequest();
        }
        _questionService.UpdateQuestion(id, question);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteQuestion(int id)
    {
        _questionService.DeleteQuestion(id);
        return NoContent();
    }
}