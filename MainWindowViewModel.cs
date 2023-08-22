using System.Windows.Input;

namespace Proiect_bun
{
    public class MainWindowViewModel : ViewModelBase
    {
        private TestModel _testModel;
        private string _userName;
        private string _selectedDifficulty;

        public MainWindowViewModel(TestModel testModel)
        {
            _testModel = testModel;
            StartQuizCommand = new RelayCommand(StartQuiz);
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string SelectedDifficulty
        {
            get { return _selectedDifficulty; }
            set
            {
                if (_selectedDifficulty != value)
                {
                    _selectedDifficulty = value;
                    OnPropertyChanged(nameof(SelectedDifficulty));
                }
            }
        }

        public ICommand StartQuizCommand { get; private set; }

        private void StartQuiz(object parameter)
        {
            // setează numele de utilizator
            _testModel.UserName = UserName;

            // setează dificultatea
            _testModel.DifficultyLevel = SelectedDifficulty;

            // începe testul
            _testModel.StartTest();
        }
    }
}
