namespace QuizBlazorProject.Server.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int FKQuizGameId { get; set; }
        public string QuestionName { get; set; }
        public bool IsTimed { get; set; } = false;
        public int TimeLimit { get; set; }
        public bool UseMedia { get; set; } = false;
        public string MediaUrl { get; set; } = null;
        public string MediaType { get; set; } = string.Empty;
        public bool IsFreeTextAnswer { get; set; } 
        public QuizGame QuizGame { get; set; }
        public List<QuizQuestionAnswer> Answers { get; set; } // one to many
    }
}
