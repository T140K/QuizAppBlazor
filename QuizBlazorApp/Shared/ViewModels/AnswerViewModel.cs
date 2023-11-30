using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public int FKQuestionId { get; set; }
        public string AnswerTitle { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
