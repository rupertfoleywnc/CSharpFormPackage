# C# Form Builder Platform

A comprehensive ASP.NET Core MVC application for creating, managing, and deploying dynamic forms with a visual drag-and-drop editor.

## 🚀 Features

### Visual Form Builder
- **Drag-and-drop interface** for creating forms visually
- **Real-time canvas** with question positioning and flow connections
- **Question types supported:**
  - Button/Multiple Choice
  - Yes/No questions
  - Text input
  - Textarea
  - Date picker
  - Address input (street + city)
  - Location picker with interactive map
  - Dropdown selections
  - End pages

### Form Management
- **Form library** with list view of all created forms
- **CRUD operations:** Create, Edit, View, and Delete forms
- **File-based storage** using JSON format
- **Auto-positioning** of questions with intelligent flow layout
- **Connection visualization** showing question flow paths

### Customization & Theming
- **Custom color themes** per form (primary, background, text colors)
- **Email configuration** with custom subject and body templates
- **Responsive design** that works on desktop and mobile

### Form Deployment
- **Public form URLs** for end-users to fill out forms
- **Session management** with back/forward navigation
- **Answer persistence** throughout the form session
- **Custom styling** applied to deployed forms

### Email Integration (Framework Ready)
- Email settings stored with each form
- TODO markers for implementing SMTP email sending
- Form response collection ready for email dispatch

## 🛠️ Technical Stack

- **Framework:** ASP.NET Core 9.0 MVC
- **Frontend:** HTML5, CSS3, JavaScript (Vanilla)
- **Mapping:** Leaflet.js with OpenStreetMap
- **Data Storage:** JSON files
- **Dependencies:** 
  - Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
  - Newtonsoft.Json

## 📁 Project Structure

```
CsharpFormBuilder/
├── Controllers/
│   ├── FormEditorController.cs    # Visual form builder
│   ├── QuestionController.cs      # Form rendering & navigation
│   └── HomeController.cs          # Basic home controller
├── Models/
│   ├── Question.cs               # Question data model
│   ├── QuestionViewModel.cs      # View model for form rendering
│   └── UserAnswer.cs            # User response model
├── Services/
│   └── QuestionService.cs       # Form data management
├── Views/
│   ├── FormEditor/
│   │   ├── Index.cshtml         # Visual form builder interface
│   │   └── List.cshtml          # Form management dashboard
│   ├── Question/
│   │   ├── Index.cshtml         # Form rendering for end-users
│   │   └── End.cshtml           # Form completion page
│   └── Shared/
│       └── _Layout.cshtml       # Main layout template
├── wwwroot/
│   ├── css/site.css            # Application styles
│   ├── data/                   # JSON form storage
│   └── images/                 # Static assets
└── Program.cs                  # Application entry point
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022+ or VS Code with C# extension

### Installation
1. Clone the repository
2. Navigate to the project directory
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. Open browser to `https://localhost:5001`

## 📖 Usage Guide

### Creating Forms
1. Navigate to the home page (form list)
2. Click "Create New Form"
3. Use the visual editor to:
   - Add questions by clicking "Add Question"
   - Drag questions to position them
   - Click questions to edit properties
   - Configure question flow with destination IDs
   - Set form settings (email, colors)
4. Save your form

### Managing Forms
- **View Forms:** Click "View Form" to see the end-user experience
- **Edit Forms:** Click "Edit" to modify existing forms
- **Delete Forms:** Click "Delete" with confirmation
- **Form Settings:** Configure email settings and color themes

### Question Types & Configuration

#### Button/Multiple Choice
- Add multiple options with destination question IDs
- Users click buttons to navigate through the form

#### Text Input
- Single-line text input with optional validation
- Configure as required/optional

#### Address Input
- Two-field input: Street and Town/City
- Combines into single comma-separated value

#### Location Picker
- Interactive map using Leaflet.js
- Click to select coordinates
- Reverse geocoding for address display

#### End Pages
- Terminal questions that complete the form
- Display summary of all user responses

### Theming & Customization
1. Open Form Settings in the editor
2. Configure:
   - **Email Address:** Where form responses are sent
   - **Email Subject:** Custom subject line
   - **Email Body:** Custom message template
   - **Primary Color:** Main theme color
   - **Background Color:** Page background
   - **Text Color:** Main text color

## 🔧 Configuration

### Form Data Structure
Forms are stored as JSON files in `wwwroot/data/` with this structure:
```json
{
  "title": "Form Name",
  "emailTo": "admin@example.com",
  "emailSubject": "New Form Submission",
  "emailBody": "Custom message...",
  "primaryColor": "#007bff",
  "backgroundColor": "#ffffff",
  "textColor": "#333333",
  "questions": [
    {
      "id": 0,
      "questionText": "Question text here",
      "type": "button",
      "required": true,
      "options": [
        {
          "text": "Option 1",
          "destination": 1
        }
      ],
      "helpText": "Optional help text",
      "x": 30,
      "y": 30
    }
  ]
}
```

### Routing Configuration
- **Home:** `/` - Form management dashboard
- **Form Builder:** `/FormEditor/Index` - Visual form editor
- **Form Viewer:** `/Question/Index?form=formname` - Public form access

## 🔮 Future Enhancements (TODOs)

### Email Integration
- [ ] Add SMTP configuration in appsettings.json
- [ ] Implement email service with form response formatting
- [ ] Add email sending on form completion
- [ ] Email validation and error handling

### Advanced Features
- [ ] Form analytics and response tracking
- [ ] Export responses to CSV/Excel
- [ ] Form templates and duplication
- [ ] Advanced validation rules
- [ ] Conditional logic and branching
- [ ] File upload question type
- [ ] Multi-page forms with progress indicators

### Security & Performance
- [ ] User authentication and authorization
- [ ] Form access controls and permissions
- [ ] Database storage option
- [ ] Caching and performance optimization
- [ ] Input sanitization and validation

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For issues, questions, or contributions, please create an issue in the repository or contact the development team.

---

**Built with ❤️ using ASP.NET Core MVC**