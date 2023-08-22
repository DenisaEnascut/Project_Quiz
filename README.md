# Project_Quiz

Introduction
The Quiz Application is a Windows Presentation Foundation (WPF) application designed to provide an interactive quiz experience for users. With dynamic question loading and immediate feedback, users can enhance their knowledge while having fun.

Features
User-Friendly Interface
The application features a clean and intuitive user interface that guides users through each step of the quiz process. The design is aimed at delivering a smooth and engaging experience.

Dynamic Question Loading
Questions are loaded dynamically from an XML file (Quiz.xml) based on the selected difficulty level. This approach allows easy customization of quiz content without modifying the application code.

Immediate Feedback
Users receive instant feedback on their answers. When a user selects an answer, they are notified immediately if it's correct or incorrect, enhancing the learning process.


Performance Summary
Upon completing the quiz, users are presented with a performance summary. This summary includes the number of correctly answered questions, any incorrectly answered questions, and a percentage score.

Getting Started
Prerequisites
  Visual Studio (2019 or later) with .NET Framework support.

Usage
  Launch the application by clicking the "Start" button.
  Enter your username and choose a difficulty level from the dropdown.
  Answer the questions by selecting the appropriate option(s).
  Click the "Next" button to proceed through the quiz.
  After completing the quiz, view your performance summary.
  
Customization
You can customize the quiz content easily:
  Open the Quiz.xml file.
  Modify existing questions or add new <Q> elements with corresponding <R> (response) elements.
  Set the attributes for each question, including text, level, id, and type.
  For multiple-choice questions, indicate the correct answer using the isCorrect attribute.
  
File Structure
The project structure is as follows:
QuizApplication/
├── Quiz.xml              # Contains quiz questions and options
├── MainWindow.xaml.cs   # Main application logic
├── Quiz.xaml.cs          # Quiz window and question display
├── Finish.xaml.cs        # Finish window with user results
├── ...
