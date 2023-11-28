namespace QuizBlazorProject.Server.Models
{
    public class QuizGame
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public string CreatorName { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }// one to many
    }
}
