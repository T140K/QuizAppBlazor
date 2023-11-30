namespace QuizBlazorApp.Shared.ViewModels
{
    public class QuizViewModel
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string CreatorName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
