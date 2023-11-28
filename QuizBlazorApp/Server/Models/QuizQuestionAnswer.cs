namespace QuizBlazorProject.Server.Models
{
    public class QuizQuestionAnswer
    {
        public int Id { get; set; }
        public int FKQuestionId { get; set; }
        public string AnswerTitle { get; set; }
        public bool CorrectAnswer { get; set; } = false;
        public QuizQuestion Question { get; set; } //many to one
    }
}
