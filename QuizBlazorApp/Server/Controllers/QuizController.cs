using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizBlazorApp.Server.Data;
using QuizBlazorApp.Server.Models;
using QuizBlazorApp.Shared.ViewModels;
using QuizBlazorProject.Server.Models;

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

            var quiz = await _context.QuizGames
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .Where(q => q.CreatorName == userId.Email)
                .OrderByDescending(q => q.Id)
                .ToListAsync();

            if (quiz == null)
            {
                return NotFound();
            }


            var quizViewModels = quiz.Select(quiz => new QuizViewModel
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
                        UseMedia = q.UseMedia,
                        MediaUrl = q.MediaUrl,
                        MediaType = q.MediaType,
                        IsFreeTextAnswer = q.IsFreeTextAnswer,
                        Answers = q.Answers.Select(a =>
                            new AnswerViewModel
                            {
                                AnswerId = a.Id,
                                FKQuestionId = a.FKQuestionId,
                                AnswerTitle = a.AnswerTitle,
                                CorrectAnswer = a.CorrectAnswer
                            }).ToList()
                    }).ToList()
            }).ToList();

            return Ok(quizViewModels);
        }

        [HttpPost("createquiz")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizViewModel quizViewModel)
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

            var existingQuiz = await _context.QuizGames.FirstOrDefaultAsync(q => q.QuizName.ToLower() == quizViewModel.QuizName.ToLower());
            if (existingQuiz != null)
            {
                return BadRequest("A quiz with this name already exists. Please choose a different name.");
            }

            var quiz = new QuizGame
            {
                QuizName = quizViewModel.QuizName,
                CreatorName = userId.Email, 
                QuizQuestions = quizViewModel.Questions.Select(q => new QuizQuestion
                {
                    QuestionName = q.QuestionName,
                    IsTimed = q.IsTimed,
                    TimeLimit = q.TimeLimit,
                    IsFreeTextAnswer = q.IsFreeTextAnswer,
                    UseMedia = q.UseMedia,
                    MediaUrl = q.MediaUrl,
                    MediaType = q.MediaType,
                    Answers = q.Answers.Select(a => new QuizQuestionAnswer
                    {
                        AnswerTitle = a.AnswerTitle,
                        CorrectAnswer = a.CorrectAnswer
                    }).ToList()
                }).ToList()
            };


            _context.QuizGames.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getquizbytitle/{quizTitle}")]
        public async Task<IActionResult> GetQuizByTitle(string quizTitle)
        {
            if (string.IsNullOrEmpty(quizTitle))
            {
                return BadRequest("No quiz was given");
            }

            var loadedQuiz = await _context.QuizGames
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .FirstOrDefaultAsync(quiz => quiz.QuizName == quizTitle);

            if (loadedQuiz == null) 
            {
                return NotFound($"Didnt find quizz with the title {quizTitle}");
            }

            var quizViewModel = new QuizViewModel
            {
                QuizId = loadedQuiz.Id,
                QuizName = loadedQuiz.QuizName,
                CreatorName = loadedQuiz.CreatorName,
                Questions = loadedQuiz.QuizQuestions.Select(q =>
                    new QuestionViewModel
                    {
                        QuestionId = q.Id,
                        FKQuizGameId = q.FKQuizGameId,
                        QuestionName = q.QuestionName,
                        IsTimed = q.IsTimed,
                        TimeLimit = q.TimeLimit,
                        UseMedia = q.UseMedia,
                        MediaUrl = q.MediaUrl,
                        MediaType = q.MediaType,
                        IsFreeTextAnswer = q.IsFreeTextAnswer,
                        Answers = q.Answers.Select(a =>
                            new AnswerViewModel
                            {
                                AnswerId = a.Id,
                                FKQuestionId = a.FKQuestionId,
                                AnswerTitle = a.AnswerTitle,
                                CorrectAnswer = a.CorrectAnswer
                            }).ToList()
                    }).ToList()
            };

            return Ok(quizViewModel);
        }

        [HttpPost("submitquiz")]
        public async Task<IActionResult> SubmitQuiz([FromBody] PlayQuizViewModel quizMatch)
        {
            if (quizMatch == null)
            {
                return BadRequest("quiz given doesnt exsit");
            }

            var quizCheatSheet = await _context.QuizGames
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Answers)
                .Where(q => q.Id == quizMatch.FkQuizId)
                .FirstOrDefaultAsync();

            if (quizCheatSheet == null)
            {
                return BadRequest("cant find the corresponding quiz");
            }

            var quizResult = new QuizResult();
            quizResult.ResultAnswers = new List<QuizResultAnswers>();

            foreach (var userAnswer in quizMatch.Answers)
            {
                var correctAnswers = quizCheatSheet.QuizQuestions
                    .FirstOrDefault(q => q.Id == userAnswer.FKQuestionId)?
                    .Answers.Where(a => a.CorrectAnswer).ToList();

                var resultAnswer = new QuizResultAnswers
                {
                    FKQuestionId = userAnswer.FKQuestionId,
                    AnswerdCorrect = false
                };
                
                if (userAnswer.QuestionAnswerdInTime)
                {
                    if (userAnswer.IsFreeTextAnswer)
                    {
                        var correctAnswer = correctAnswers?.FirstOrDefault();
                        resultAnswer.AnswerdCorrect = userAnswer.FreeTextAnswer?.Trim().Equals(correctAnswer?.AnswerTitle?.Trim(), StringComparison.OrdinalIgnoreCase) ?? false;
                    }
                    else
                    {
                        resultAnswer.AnswerdCorrect = correctAnswers?.Any(a => a.Id == userAnswer.SelectedAnswerId) ?? false;
                    }
                }

                quizResult.ResultAnswers.Add(resultAnswer);
                quizResult.TotalAnswers++;

                if (resultAnswer.AnswerdCorrect)
                {
                    quizResult.CorrectAnswers++;
                }
            }

            _context.QuizResults.Add(quizResult);
            await _context.SaveChangesAsync();

            var result = new QuizResultViewModel
            {
                QuizCreator = quizCheatSheet.CreatorName,
                QuizTitle = quizCheatSheet.QuizName,
                TotalQuestions = quizResult.TotalAnswers,
                CorrectQuestions = quizResult.CorrectAnswers
            };
            return Ok(result);
        }
    }
}
