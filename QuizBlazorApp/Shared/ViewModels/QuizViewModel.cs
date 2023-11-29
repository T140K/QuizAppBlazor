namespace QuizBlazorApp.Shared.ViewModels
{
    public class QuizViewModel
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string CreatorName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public int FKQuizGameId { get; set; }
        public string QuestionName { get; set; }
        //how do i get in videos exactly
        public bool IsTimed { get; set; }
        public int TimeLimit { get; set; } = 0;
        public List<AnswerViewModel> Answers { get; set; }
        //public DateTime Starttime { get; set; }
        //when doing the quiz save this at question start, then when user answers
        //compare user answer dattime to StartTime and if difference is more than 
        //question time make answer even if it was correct
    }

    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public int FKQuestionId { get; set; }
        public string AnswerTitle { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
