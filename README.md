# Question Sorter C# MVC Application

This is a C# ASP.NET Core MVC implementation of the JavaScript Question Sorter. It processes a series of questions and responses based on JSON data, similar to the original JavaScript implementation.

## Features

- Dynamic question loading based on JSON data
- Support for various question types:
  - Button options
  - Yes/No questions
  - Text input
  - Textarea input
  - Date selection
  - Address input
  - Location selection with map integration
  - Dropdown selections
- Ability to navigate back to previous questions
- Support for restarting the form
- Responsive design

## Requirements

- .NET 9.0 SDK
- Visual Studio 2022 or later (or Visual Studio Code with C# extensions)

## Setup and Run

1. Clone this repository
2. Open the solution in Visual Studio
3. Build the solution
4. Run the application

## Project Structure

- **Models**: Contains data models for questions, options, and user answers
- **Controllers**: Contains the QuestionController that handles form navigation
- **Views**: Contains Razor views for rendering questions and responses
- **Services**: Contains the QuestionService for loading and managing question data
- **wwwroot**: Contains static assets including the JSON data file

## Customization

To customize the questions:

1. Edit the `wwwroot/data/questionData.json` file
2. Follow the existing format for each question type
3. Ensure question IDs and destinations are properly linked

## License

This project is licensed under the MIT License.
