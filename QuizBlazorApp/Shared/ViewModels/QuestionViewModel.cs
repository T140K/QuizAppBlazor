using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public int FKQuizGameId { get; set; }
        [Required(ErrorMessage = "This question needs a name")]
        public string QuestionName { get; set; }
        public bool IsFreeTextAnswer { get; set; }
        public bool IsTimed { get; set; }
        public int TimeLimit { get; set; } = 0;
        public bool UseMedia { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
