using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizBlazorApp.Server.Data;
using QuizBlazorApp.Shared.ViewModels;

namespace QuizBlazorApp.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuizController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("userquiz")]
        public async Task<IActionResult> GetQuizzesByUserAsync()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return BadRequest("Can't find user.");
            }
            var userId = _context.Users.Where(u => u.Id == user).FirstOrDefault();
            if (userId == null)
            {
                return BadRequest("Can't find user.");
            }
            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("cant find user.");
            }*/

            var quiz = await _context.QuizGames
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .Where(q => q.CreatorName == userId.Email)
                .ToListAsync();

            if (quiz == null)
            {
                return NotFound();
            }


            var quizViewModels = new QuizViewModelViewModel()
            {
                QuizItems = quiz.Select(quiz => new QuizViewModel
                {
                    QuizId = quiz.Id,
                    QuizName = quiz.QuizName,
                    CreatorName = quiz.CreatorName,
                    Questions = quiz.QuizQuestions.Select(q => new QuestionViewModel
                    {
                        QuestionId = q.Id,
                        FKQuizGameId = q.FKQuizGameId,
                        QuestionName = q.QuestionName,
                        IsTimed = q.IsTimed,
                        TimeLimit = q.TimeLimit,
                        Answers = q.Answers.Select(a => new AnswerViewModel
                        {
                            AnswerId = a.Id,
                            FKQuestionId = a.FKQuestionId,
                            AnswerTitle = a.AnswerTitle,
                            CorrectAnswer = a.CorrectAnswer
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
            

            /*var quizViewModels = new QuizViewModel
            {
                QuizId = quiz.Id,
                QuizName = quiz.QuizName,
                CreatorName = quiz.CreatorName,
                Questions = quiz.QuizQuestions.Select(q =>
                    new QuestionViewModel
                    {
                        QuestionId = q.Id,
                        FKQuizGameId = q.FKQuizGameId,
                        QuestionName = q.QuestionName,
                        IsTimed = q.IsTimed,
                        TimeLimit = q.TimeLimit,
                        Answers = q.Answers.Select(a =>
                            new AnswerViewModel()
                            {
                                AnswerId = a.Id,
                                FKQuestionId = a.FKQuestionId,
                                AnswerTitle = a.AnswerTitle,
                                CorrectAnswer = a.CorrectAnswer
                            }).ToList()
                    }).ToList()
            };*/

            return Ok(quizViewModels);
        }
    }
}
