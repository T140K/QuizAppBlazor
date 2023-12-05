namespace QuizBlazorProject.Server.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int FKQuizGameId { get; set; }
        public string QuestionName { get; set; }
        public bool IsTimed { get; set; } = false;
        public int TimeLimit { get; set; }
        public string MediaUrl { get; set; } = null;
        public bool IsFreeTextAnswer { get; set; } 
        public QuizGame QuizGame { get; set; }
        public List<QuizQuestionAnswer> Answers { get; set; } // one to many
    }
}
