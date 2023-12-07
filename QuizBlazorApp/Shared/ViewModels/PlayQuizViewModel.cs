using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class PlayQuizViewModel
    {
        public int FkQuizId { get; set; }
        public List<PlayQuizAnswersViewModel> Answers { get; set; }
    }

    public class PlayQuizAnswersViewModel
    {
        public int FKQuestionId { get; set; }
        public bool IsFreeTextAnswer { get; set; } = false;
        public int SelectedAnswerId { get; set; }
        public string FreeTextAnswer { get; set; }
        public bool QuestionAnswerdInTime { get; set; } = true;
    }
}
