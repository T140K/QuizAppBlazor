﻿using System;
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
        //how do i get in videos exactly
        public bool IsTimed { get; set; }
        public int TimeLimit { get; set; } = 0;
        public List<AnswerViewModel> Answers { get; set; }
        //public DateTime Starttime { get; set; }
        //when doing the quiz save this at question start, then when user answers
        //compare user answer dattime to StartTime and if difference is more than 
        //question time make answer even if it was correct

        //do this in the QuizResponse part so that the timings are handeld by the viewmodel with time starting on getting a question and time ending on answering or posting a answer
    }
}
