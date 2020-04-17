using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

using Quizz.ViewModel;

namespace Quizz.Views
{
    public partial class Level : ContentPage
    {
        #region parameters

        private readonly LevelQuestionsViewModel bindingContext;
        private Popup _popup;
        private LevelEndPopup levelEndPopup;

        #endregion

        public Level(int levelIdParam)
        {
            InitializeComponent();
            bindingContext = new LevelQuestionsViewModel(levelIdParam);
            bindingContext.Navigation = Navigation;
            BindingContext = bindingContext;
            questionNumber.Text = "1/10";
        }

        #region additional functions

        async void nextQuestion()
        {
            if (QuestionsCarousel.Position + 1 != bindingContext.Questions.Count)
            {
                QuestionsCarousel.Position = QuestionsCarousel.Position + 1;
                bindingContext.CurrentQuestion = bindingContext.Questions[QuestionsCarousel.Position];
                questionNumber.Text = (QuestionsCarousel.Position + 1) + "/" + bindingContext.Questions.Count;
                bindingContext.CurrentQuestion.Progress.StartTimer();
            }
            else
            {
                endLevel();
            }

        }

        async void endLevel()
        {
            bindingContext.endLevel();
            await PopupNavigation.Instance.PopAsync();
            levelEndPopup = new LevelEndPopup(bindingContext.Score, bindingContext.Questions.Count, Navigation);
            await PopupNavigation.Instance.PushAsync(levelEndPopup);

        }
        #endregion

        #region button click

        async void answerBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            bindingContext.CheckQuestion(bindingContext.Questions[QuestionsCarousel.Position]);
            _popup = new Popup(bindingContext.Questions[QuestionsCarousel.Position].isCorrectAnswer);
            await PopupNavigation.Instance.PushAsync(_popup);
            nextQuestion();
        }

        void CheckBox_CheckChanged(System.Object sender, System.EventArgs e)
        {
        }
        #endregion

    }
}
