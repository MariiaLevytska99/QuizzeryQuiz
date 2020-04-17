using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Quizz.Services;
using Quizz.ViewModels;

namespace Quizz.ViewModel
{
    public class LevelQuestionsViewModel : BaseViewModel
    {
        #region parameters
        private readonly LevelQuestionsService _levelQuestionService;
        private readonly LevelService _levelService;

        public INavigation Navigation { get; set; }

        public int levelId { get; set; }
        public double _progress;

        private int _score = 0;


        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Question> _questions;
        private Question _currentQuestion;

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region commands

        private ICommand _nextQuestion;
        public ICommand NextQuestion => _nextQuestion ?? (_nextQuestion = new Command(question =>
        {
            var quest = question as Question;
        }));

        public Command ExitLevel
        {

            get
            {
                return new Command(exitLevel);
            }
        }

        public Command EndLevel { get; }

        #endregion

        #region methods

        public async void exitLevel()
        {

            var answer = await Application.Current.MainPage.DisplayAlert("Завершити тестування?", "Всі результати будуть втрачені", "Так", "Ні");
            if (answer)
            {
                Navigation.PopAsync();
                CurrentQuestion.Progress.Progress = 1.1f;
            }

        }

        public async void endLevel()
        {
            _levelService.Post(levelId, Score);
        }

        public void CheckQuestion(Question question)
        {
            var questionIsRight = question.CheckAnswer();
            if (questionIsRight)
                Score++;
        }

        public LevelQuestionsViewModel(int levelIdParam)
        {
            levelId = levelIdParam;
            _levelService = new LevelService();
            _levelQuestionService = new LevelQuestionsService();
            LoadQuestions();
            EndLevel = new Command(endLevel);
        }

        private async Task LoadQuestions()
        {
            var questions = new List<Question>();
            var result = await _levelQuestionService.Get(levelId);
            foreach (var question in result)
            {
                ObservableCollection<Answer> _answers = new ObservableCollection<Answer>();
                foreach (var answer in question.Answers)
                {
                    _answers.Add(new Answer
                    {
                        Id = answer.Id,
                        Text = answer.Text,
                        IsCorrect = answer.IsCorrect

                    });
                }
                questions.Add(new Question
                {
                    Id = question.Id,
                    Text = question.Text,
                    Answers = _answers,
                    Type = (int)question.Type

                });
            }

            Questions = new ObservableCollection<Question>(questions);
            CurrentQuestion = Questions[0];
            CurrentQuestion.Progress.StartTimer();
        }

        #endregion


        public sealed class Question : BaseViewModel
        {

            #region parameters

            public int _id { get; set; }
            public string _text { get; set; }
            private int _type { get; set; }

            public bool isCorrectAnswer { get; set; }

            public ObservableCollection<Answer> _answers { get; set; }

            public ProgressBarViewModel _progress = new ProgressBarViewModel();

            private Answer _selectedAnswer { get; set; }

            public Answer SelectedAnswer
            {
                get { return _selectedAnswer; }
                set
                {
                    _selectedAnswer = value;
                    OnPropertyChanged();
                }
            }

            private string _enteredAnswer { get; set; }

            public string EnteredAnswer
            {
                get { return _enteredAnswer; }
                set
                {
                    _enteredAnswer = value;
                    OnPropertyChanged();
                }
            }

            #endregion

            #region methods 
            public bool CheckAnswer()
            {
                bool isCorrect = false;
                Progress.isExpired = false;
                if (this.Type == 2 || this.Type == 1)
                {
                    foreach (var answer in _answers)
                    {
                        if (answer.IsCorrect && answer.IsSelected)
                        {
                            isCorrect = true;
                            isCorrectAnswer = true;
                            answer.AnswerColor = Color.Green;
                        }
                        else if (answer.IsSelected && !answer.IsCorrect)
                        {
                            answer.AnswerColor = Color.Red;
                        }
                        else if (answer.IsCorrect)
                        {
                            answer.AnswerColor = Color.Green;
                            isCorrect = false;
                            isCorrectAnswer = false;
                        }
                    }
                }

                else
                {
                    foreach (var ans in _answers)
                    {
                        if (ans.Text.Equals(_enteredAnswer))
                        {
                            isCorrect = true;
                            isCorrectAnswer = isCorrect;
                        }
                    }
                }
                return isCorrect;
            }

            public void ChangeAnswer(Answer answer)
            {
                foreach (var ans in _answers)
                {
                    if (ans != answer)
                        ans.IsSelected = false;
                    else
                        ans.IsSelected = true;
                }
            }

            #endregion

            #region get set
            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }

            public string Text
            {
                get => _text;
                set
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }

            public int Type
            {
                get { return _type; }
                set
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }

            public ProgressBarViewModel Progress
            {
                get => _progress;
                set
                {
                    _progress = value;
                    OnPropertyChanged();
                }
            }

            public ObservableCollection<Answer> Answers
            {
                get => _answers;
                set
                {
                    _answers = value;
                    OnPropertyChanged();
                }
            }

            #endregion
        }



        public sealed class Answer : BaseViewModel
        {
            #region parameters

            public int _id { get; set; }
            public string _text { get; set; }
            public bool _isCorrect { get; set; }
            public bool _isSelected { get; set; }
            private Color _answerColor = Color.Gray;

            #endregion

            #region get set

            public Color AnswerColor
            {
                get { return _answerColor; }
                set
                {
                    _answerColor = value;
                    OnPropertyChanged();
                }
            }

            public int Id
            {
                get => _id;
                set
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }

            public string Text
            {
                get => _text;
                set
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }

            public bool IsCorrect
            {
                get => _isCorrect;
                set
                {
                    _isCorrect = value;
                    OnPropertyChanged();
                }
            }

            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }

            #endregion

            #region commands

            public Command AnswerSelected
            {
                get
                {
                    return new Command(answerSelected);
                }
            }

            public async void answerSelected()
            {
                IsSelected = !IsSelected;


            }
            #endregion

        }

    }
}
