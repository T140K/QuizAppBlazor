using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class CreateQuizViewModel
    {
        [Required(ErrorMessage = "The quiz needs a title")]
        public string QuizName { get; set; }
        public List<CreateQuizQuestions> Questions { get; set; }

    }

    public class CreateQuizQuestions
    {
        [Required(ErrorMessage = "This question needs a name")]
        public string QuestionName { get; set; }
        public bool IsTimed { get; set; } = false;
        public int TimeLimit { get; set; } = 0;
        public bool UseMedia { get; set; } = false;
        public string MediaUrl { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty;
        public bool IsFreeTextAnswer { get; set; }
        public List<CreateQuestionAnswer> Answers { get; set; }
    }

    public class CreateQuestionAnswer
    {
        [Required(ErrorMessage = "This answer needs a name!")]
        public string AnswerTitle { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
