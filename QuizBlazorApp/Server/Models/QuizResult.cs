using System.Text.Json.Serialization;

namespace QuizBlazorApp.Server.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int FKQuizId { get; set; }
        public string QuizTaker { get; set; }
        public int TotalAnswers { get; set; }
        public int CorrectAnswers { get; set; }
        [JsonIgnore]
        public List<QuizResultAnswers> ResultAnswers {get; set;}
    }

    public class QuizResultAnswers
    {
        public int Id { get; set; }
        public int QuizResultId {get; set;}
        public int FKQuestionId { get; set; }
        public bool AnswerdCorrect { get; set; }
        public QuizResult QuizResult { get; set; }
    }
}
