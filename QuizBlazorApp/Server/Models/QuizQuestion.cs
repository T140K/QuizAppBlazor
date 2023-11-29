namespace QuizBlazorProject.Server.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int FKQuizGameId { get; set; }
        public string QuestionName { get; set; }
        //hur ska man support video e frågan
        public bool IsTimed { get; set; } = false;
        public int TimeLimit { get; set; }
        public QuizGame QuizGame { get; set; }
        public List<QuizQuestionAnswer> Answers { get; set; } // one to many
        //public QuizQuestionTime QuizQuestionTime { get; set; }// one to one
    }
}
