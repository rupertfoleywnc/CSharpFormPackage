using Microsoft.AspNetCore.Mvc;

namespace CsharpFormBuilder.Controllers
{
    public class FormEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveForm([FromBody] FormData formData)
        {
            try
            {
                if (formData == null || string.IsNullOrEmpty(formData.title))
                {
                    return Json(new { success = false, error = "Form data or title is missing" });
                }
                
                var fileName = formData.title.Replace(" ", "") + "FormFlow.json";
                var filePath = Path.Combine("wwwroot", "data", fileName);
                
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                
                var json = System.Text.Json.JsonSerializer.Serialize(formData, new System.Text.Json.JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                
                await System.IO.File.WriteAllTextAsync(filePath, json);
                
                return Json(new { success = true, fileName = fileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetFormList()
        {
            try
            {
                var dataPath = Path.Combine("wwwroot", "data");
                if (Directory.Exists(dataPath))
                {
                    var files = Directory.GetFiles(dataPath, "*FormFlow.json")
                        .Select(f => Path.GetFileNameWithoutExtension(f).Replace("FormFlow", ""))
                        .ToList();
                    return Json(new { success = true, files = files });
                }
                return Json(new { success = true, files = new List<string>() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public IActionResult List()
        {
            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data");
            var forms = new List<FormInfo>();
            
            if (Directory.Exists(dataPath))
            {
                var files = Directory.GetFiles(dataPath, "*FormFlow.json");
                foreach (var file in files)
                {
                    try
                    {
                        var json = System.IO.File.ReadAllText(file);
                        var formData = System.Text.Json.JsonSerializer.Deserialize<FormData>(json);
                        forms.Add(new FormInfo 
                        { 
                            Title = formData.title, 
                            FileName = Path.GetFileName(file),
                            QuestionCount = formData.questions?.Count ?? 0
                        });
                    }
                    catch { }
                }
            }
            
            return View(forms);
        }

        [HttpPost]
        public IActionResult DeleteForm(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("List");
            }
        }
    }

    public class FormData
    {
        public string title { get; set; }
        public List<Question> questions { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
        public List<Option> Options { get; set; }
        public string HelpText { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Option
    {
        public string Text { get; set; }
        public int? Destination { get; set; }
    }

    public class FormInfo
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public int QuestionCount { get; set; }
    }
}