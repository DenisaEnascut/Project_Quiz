using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Xml;
using static FSharp.Compiler.Syntax.SynConst;

namespace Proiect_bun
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] comboItens = new[] { "Esay", "Medium", "Hard" };
            comboBox.ItemsSource = comboItens;
        }
        private string xmlFilePath;
        public bool IsUserNameValid { get; internal set; }
        public bool IsDifficultySelected { get; internal set; }
        public static string UserName { get; set; }
        public static string Difficulty { get; set; }
        private Dictionary<string, string> raspunsuriUtilizator = new Dictionary<string, string>();


        public void StartQuiz()
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Please enter a valid username.");
                IsUserNameValid = false;
            }
            else
            {
                IsUserNameValid = true;
            }

            if (comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a difficulty level.");
                IsDifficultySelected = false;
            }
            else
            {
                IsDifficultySelected = true;
            }
            if (IsUserNameValid && IsDifficultySelected)
            {
                Quiz quizWindow = new Quiz(xmlFilePath, comboBox.SelectedIndex + 1);
                quizWindow.ShowDialog();
                // testul s-a finalizat cu succes, deci putem deschide formula finală
                Finish finishForm = new Finish();
                finishForm.UserName = textBox.Text;
                finishForm.Difficulty = comboBox.SelectedItem.ToString();
                finishForm.ShowDialog();
                this.Close();
            }

            else
            {
                MessageBox.Show("Please enter a valid user name and select a difficulty level.");
            }
            
        }
        private void button_Start(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && comboBox.SelectedItem != null)
            {
                //Afisez numele in fomrul final
                MainWindow.UserName = textBox.Text;

                switch (comboBox.SelectedItem.ToString())
                {
                    case "Esay":
                        xmlFilePath = "quiz.xml";
                        break;
                    case "Medium":
                        xmlFilePath = "quiz.xml";
                        break;
                    case "Hard":
                        xmlFilePath = "quiz.xml";
                        break;
                    default:
                        MessageBox.Show("Invalid difficulty level. Please select a valid difficulty level.");
                        return;
                }
                //Afisez dificultatea aleasa in fomrul final
                MainWindow.Difficulty = textBox.Text;
                StartQuiz();
            }
            else
            {
                MessageBox.Show("Please enter a valid user name and select a difficulty level.");
            }
        }

    }
}
