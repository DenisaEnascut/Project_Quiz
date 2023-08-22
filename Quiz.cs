using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using static FSharp.Compiler.Syntax.SynConst;

namespace Proiect_bun
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>

    public partial class Quiz : Window
    {
        private List<string> selectedAnswers = new List<string>();
        private List<string> selectedQuestions = new List<string>();
        private List<XmlNode> questions = new List<XmlNode>();
        private int difficultyLevel;
        private int questionsDisplayed = 0;
        private bool isSubmitEnabled = false;
        private bool isAnswerSelected = false;
       // private Dictionary<XmlNode, string> userAnswers = new Dictionary<XmlNode, string>();

        public Quiz(string xmlFilePath, int difficultyLevel)
        {
            InitializeComponent();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Get the first 10 questions for the specified difficulty level
            XmlNodeList questionNodes = xmlDoc.SelectNodes($"//Q[@level={difficultyLevel}]");
            int questionsCount = Math.Min(questionNodes.Count, 10);
            for (int i = 0; i < questionsCount; i++)
            {
                questions.Add(questionNodes[i]);
            }
            // Display the first question
            DisplayQuestion(questions.First());

        }

        private void GetRandomQuestions()
        {
            var random = new Random();
            int questionsToDisplay = 5;
            var randomQuestions = new List<XmlNode>();

            while (questionsToDisplay > 0 && selectedQuestions.Count < questions.Count)
            {
                // Select a random question that hasn't been selected yet
                var randomQuestion = questions.OrderBy(q => random.Next()).FirstOrDefault(q => !selectedQuestions.Contains(q.Attributes["text"].Value));
                if (randomQuestion != null)
                {
                    // Add the question to the list of displayed questions
                    selectedQuestions.Add(randomQuestion.Attributes["text"].Value);

                    // Add the question to the list of questions to display
                    randomQuestions.Add(randomQuestion);

                    // Decrease the number of questions to display
                    questionsToDisplay--;
                }
            }

            if (questionsToDisplay == 0)
            {
                isSubmitEnabled = true;
            }

            if (!isAnswerSelected)
            {
                MessageBox.Show("Please select an answer before proceeding to the next question.");
                return;
            }

            // Display each random question
            foreach (var randomQuestion in randomQuestions)
            {
                DisplayQuestion(randomQuestion);
            }

        }
        private void DisplayQuestion(XmlNode questionNode)
        {
            if (questionNode != null)
            {
                // Get the question text
                string questionText = questionNode.Attributes["text"]?.Value;

                // Check if questionText is not null before proceeding
                if (!string.IsNullOrEmpty(questionText))
                {
                    // Clear the question panel of any previous questions
                    questionPanel.Children.Clear();

                    // Create a TextBlock for the question
                    TextBlock questionTextBlock = new TextBlock();
                    questionTextBlock.Text = questionText;
                    questionTextBlock.Margin = new Thickness(0, 0, 0, 10);
                    // Add the TextBlock to the StackPanel
                    questionPanel.Children.Add(questionTextBlock);

                    // Get the answer options
                    List<string> answerOptions = new List<string>();
                    XmlNodeList answerNodes = questionNode.SelectNodes("R");
                    foreach (XmlNode answerNode in answerNodes)
                    {
                        string answerText = answerNode.Attributes["text"]?.Value;
                        if (!string.IsNullOrEmpty(answerText))
                        {
                            answerOptions.Add(answerText);
                        }
                    }

                    // Create a RadioButton for each answer option
                    if (questionNode.Attributes["type"]?.Value == "multiple")
                    {
                        foreach (string answerOption in answerOptions)
                        {
                            CheckBox answerCheckBox = new CheckBox();
                            answerCheckBox.Content = answerOption;
                            answerCheckBox.Margin = new Thickness(0, 0, 0, 10);

                            // Add the CheckBox to the StackPanel
                            questionPanel.Children.Add(answerCheckBox);
                            answerCheckBox.Checked += AnswerCheckBox_Checked;
                        }
                    }
                    // Create a RadioButton for each answer option, if the question allows only one answer
                    else if (questionNode.Attributes["type"]?.Value == "single")
                    {
                        foreach (string answerOption in answerOptions)
                        {
                            RadioButton answerRadioButton = new RadioButton();
                            answerRadioButton.Content = answerOption;
                            answerRadioButton.Margin = new Thickness(0, 0, 0, 10);

                            // Add the RadioButton to the StackPanel
                            questionPanel.Children.Add(answerRadioButton);
                            answerRadioButton.Checked += AnswerRadioButton_Checked;
                        }
                    }
                    isAnswerSelected = false;

                }
            }
        }
        private void AnswerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isAnswerSelected = true;
            // Cast sender to a RadioButton
            RadioButton answerRadioButton = sender as RadioButton;

            // Get the selected answer text
            string selectedAnswer = answerRadioButton.Content.ToString();

            // Clear the selected answers list and add the current selected answer
            selectedAnswers.Clear();
            selectedAnswers.Add(selectedAnswer);
        }
        private void AnswerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Cast sender to a CheckBox
            CheckBox answerCheckBox = sender as CheckBox;

            // Get the selected answer text
            string selectedAnswer = answerCheckBox.Content.ToString();

            // Check if the CheckBox is checked
            if (answerCheckBox.IsChecked == true)
            {
                // Add the selected answer to the list of answers if it's not already there
                if (!selectedAnswers.Contains(selectedAnswer))
                {
                    selectedAnswers.Add(selectedAnswer);
                }
            }
            else
            {
                // Remove the unselected answer from the list of answers if it's there
                if (selectedAnswers.Contains(selectedAnswer))
                {
                    selectedAnswers.Remove(selectedAnswer);
                }
            }

        }

        private void DisplayNextQuestion()
        {
            // Verificați dacă s-au afișat deja cele 5 întrebări
            if (questionsDisplayed >= 5)
            {
                // Navigați la formularul final și închideți fereastra curentă
                Finish finishForm = new Finish();
                // finishForm.Show();
                this.Close();
            }
            else
            {
                // Afișați următoarea întrebare
                XmlNode nextQuestion = questions[questionsDisplayed % questions.Count];
                DisplayQuestion(nextQuestion);

                // Incrementați numărul de întrebări afișate
                questionsDisplayed++;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
          
            // Verificăm dacă s-au răspuns toate întrebările
            if (questionsDisplayed >= questions.Count)
            {
                MessageBox.Show("You have answered all the questions. Click 'Finish' to see your results.");
                return;
            }

            // Verificăm dacă s-a selectat cel puțin un răspuns pentru întrebările cu mai multe răspunsuri
            XmlNode nextQuestion = questions[questionsDisplayed % questions.Count];
            bool isMultipleAnswer = nextQuestion.Attributes["type"].Value == "multiple";
            if (isMultipleAnswer && selectedAnswers.Count == 0)
            {
                MessageBox.Show("Please select at least one answer before proceeding to the next question.");
                return;
            }
            // Afișăm următoarea întrebare
            DisplayNextQuestion();
            // Afișăm mesajul corespunzător dacă s-a evaluat ultima întrebare
            if (questionsDisplayed == 5)
            {
                nextButton.Content = "FINISH";
            }
        }


    }
}