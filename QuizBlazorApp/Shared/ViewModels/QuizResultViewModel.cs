using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class QuizResultViewModel
    {
        public string QuizTitle { get; set; }
        public string QuizCreator { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectQuestions { get; set; }
        public bool IsFinished { get; set; } = false;
    }
}
