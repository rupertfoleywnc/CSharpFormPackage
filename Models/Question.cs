using System.Collections.Generic;

namespace CSharpFormPackage.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<Option> Options { get; set; } = new List<Option>();
        public string? HelpText { get; set; }
        public bool Required { get; set; }
    }

    public class Option
    {
        public string Text { get; set; } = string.Empty;
        public int Destination { get; set; }
    }

    public class QuestionData
    {
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
