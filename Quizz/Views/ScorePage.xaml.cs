using Quizz.ViewModel;
using Xamarin.Forms;

namespace Quizz.Views
{
    public partial class ScorePage : ContentPage
    {
        private UsersViewModel UsersViewModel;

        public ScorePage()
        {

            InitializeComponent();
            UsersViewModel = new UsersViewModel();
            BindingContext = UsersViewModel;
        }

        #region appear methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new UsersViewModel();
        }

        #endregion
    }
}
