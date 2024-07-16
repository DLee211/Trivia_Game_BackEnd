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
    public ActionResult<Question> AddQuestion([FromBody] QuestionAddDto questionDto)
    {
        // Find the quiz with the given gameId and difficulty, or create a new one if it doesn't exist
        var quiz = _questionService.GetQuizByGameIdAndDifficulty(questionDto.GameId, questionDto.Difficulty) 
                   ?? _questionService.CreateQuiz(questionDto.GameId, questionDto.Difficulty);

        // Create a new question object from the DTO
        var question = new QuestionAddDto()
        {
            GameId = questionDto.GameId,
            Difficulty = questionDto.Difficulty,
            Problem = questionDto.Problem,
            Answer = questionDto.Answer,
        };

        // Add the new question
        var createdQuestion = _questionService.AddQuestion(question);

        // Return the created question
        return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.QuestionId }, createdQuestion);
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateQuestion(int id, Question question)
    {
        question.QuestionId = id;
        var existingQuestion = _questionService.GetQuestionByQuizId(id);
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