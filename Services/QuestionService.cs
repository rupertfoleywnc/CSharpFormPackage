using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using CSharpFormPackage.Models;

namespace CSharpFormPackage.Services
{
    public interface IQuestionService
    {
        Question GetQuestionById(int id);
        List<Question> GetAllQuestions();
        void LoadFormFromFile(string fileName);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly List<Question> _questions;

        public QuestionService(ILogger<QuestionService> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _questions = LoadQuestions();
        }

        public Question GetQuestionById(int id)
        {
            return _questions.FirstOrDefault(q => q.Id == id);
        }

        public List<Question> GetAllQuestions()
        {
            return _questions;
        }

        public void LoadFormFromFile(string fileName)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string jsonFilePath = Path.Combine(webRootPath, "data", fileName);
                
                if (File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    };
                    var questionData = JsonSerializer.Deserialize<QuestionData>(jsonContent, options);
                    _questions.Clear();
                    _questions.AddRange(questionData?.Questions ?? new List<Question>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading form from file");
            }
        }

        private List<Question> LoadQuestions()
        {
            try
            {
                // Use WebHostEnvironment to get the wwwroot path
                string webRootPath = _webHostEnvironment.WebRootPath;
                string jsonFilePath = Path.Combine(webRootPath, "data", "reportItFormFlow.json");

                if (!File.Exists(jsonFilePath))
                {
                    _logger.LogError($"Question data file not found at: {jsonFilePath}");
                    return new List<Question>();
                }

                string jsonContent = File.ReadAllText(jsonFilePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var questionData = JsonSerializer.Deserialize<QuestionData>(jsonContent, options);
                return questionData?.Questions ?? new List<Question>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading question data");
                return new List<Question>();
            }
        }
    }
}
