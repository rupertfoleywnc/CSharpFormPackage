using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CSharpFormPackage.Models;
using CSharpFormPackage.Services;
namespace CSharpFormPackage.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IActionResult Index()
        {
            try
            {
                var firstQuestion = _questionService.GetQuestionById(0);
                if (firstQuestion == null)
                {
                    return View("Error");
                }

                var viewModel = new QuestionViewModel
                {
                    CurrentQuestion = firstQuestion,
                    QuestionOrder = new List<int> { 0 }
                };

                // Store initial state in session
                TempData["UserAnswers"] = JsonConvert.SerializeObject(new List<UserAnswer>());
                TempData["QuestionOrder"] = JsonConvert.SerializeObject(viewModel.QuestionOrder);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult DisplayQuestion(int questionId, string? userInput = null, string? questionText = null)
        {
            // Retrieve previous answers and question order from TempData
            if (TempData["UserAnswers"] is not string userAnswersJson || TempData["QuestionOrder"] is not string questionOrderJson)
            {
                return RedirectToAction("Index");
            }

            var userAnswers = JsonConvert.DeserializeObject<List<UserAnswer>>(userAnswersJson) ?? new List<UserAnswer>();
            var questionOrder = JsonConvert.DeserializeObject<List<int>>(questionOrderJson) ?? new List<int>();

            // If we have input and question text, save the answer
            if (!string.IsNullOrEmpty(userInput) && !string.IsNullOrEmpty(questionText))
            {
                var answer = new UserAnswer
                {
                    QuestionText = questionText,
                    Answer = userInput
                };

                // Update or add the answer
                var firstOrDefault = userAnswers.FirstOrDefault(a => a.QuestionText == questionText);
                if (firstOrDefault != null)
                {
                    firstOrDefault.Answer = userInput;
                }
                else
                {
                    userAnswers.Add(answer);
                }
            }

            // Get the current question
            var question = _questionService.GetQuestionById(questionId);

            // Update question order if it's a new question
            if (!questionOrder.Contains(questionId))
            {
                questionOrder.Add(questionId);
            }

            // Find any existing answer for this question
            var existingAnswer = userAnswers.FirstOrDefault(a => a.QuestionText == question.QuestionText);

            var viewModel = new QuestionViewModel
            {
                CurrentQuestion = question,
                UserAnswers = userAnswers,
                QuestionOrder = questionOrder,
                PreviousAnswer = existingAnswer?.Answer
            };

            // Store updated state
            TempData["UserAnswers"] = JsonConvert.SerializeObject(userAnswers);
            TempData["QuestionOrder"] = JsonConvert.SerializeObject(questionOrder);

            if (question.Type == "end")
            {
                return View("End", viewModel);
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult GoBack()
        {
            // Retrieve previous answers and question order from TempData
            if (TempData["UserAnswers"] is not string userAnswersJson || TempData["QuestionOrder"] is not string questionOrderJson)
            {
                return RedirectToAction("Index");
            }

            var userAnswers = JsonConvert.DeserializeObject<List<UserAnswer>>(userAnswersJson) ?? new List<UserAnswer>();
            var questionOrder = JsonConvert.DeserializeObject<List<int>>(questionOrderJson) ?? new List<int>();

            // Remove the current question and get the previous one
            if (questionOrder.Count > 1)
            {
                // Remove the current question from order
                questionOrder.RemoveAt(questionOrder.Count - 1);

                // Get the previous question
                int previousQuestionId = questionOrder.Last();
                var previousQuestion = _questionService.GetQuestionById(previousQuestionId);

                // Find the previous answer (if it exists)
                var previousAnswer = userAnswers.FirstOrDefault(a => a.QuestionText == previousQuestion.QuestionText);

                var viewModel = new QuestionViewModel
                {
                    CurrentQuestion = previousQuestion,
                    UserAnswers = userAnswers,
                    QuestionOrder = questionOrder,
                    PreviousAnswer = previousAnswer?.Answer // Set the previous answer
                };

                // Store updated state
                TempData["UserAnswers"] = JsonConvert.SerializeObject(userAnswers);
                TempData["QuestionOrder"] = JsonConvert.SerializeObject(questionOrder);

                return View("Index", viewModel);
            }

            // If we're at the first question, just refresh
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restart()
        {
            // Clear all stored data and start over
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}