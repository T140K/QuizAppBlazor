using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBlazorApp.Shared.ViewModels
{
    public class QuizHistoryViewModel
    {
        public string QuizTaker { get; set; }
        public int TotalAnswers { get; set; }
        public int CorrectAnswers { get; set; }

    }
}
