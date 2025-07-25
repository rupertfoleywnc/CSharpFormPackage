using System.Collections.Generic;

namespace CSharpFormPackage.Models
{
    public class QuestionViewModel
    {
        public Question CurrentQuestion { get; set; } = new Question();
        public List<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
        public string? UserInput { get; set; }
        public List<int> QuestionOrder { get; set; } = new List<int>();
        public string? PreviousAnswer { get; set; }
    }
}
