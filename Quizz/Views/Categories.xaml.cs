using Xamarin.Forms;

using Quizz.ViewModel;

namespace Quizz.Views
{
    public partial class Categories : ContentPage
    {
        #region parameters

        private readonly CategoriesViewModel bindingContext;

        #endregion

        public Categories()
        {
            InitializeComponent();
            bindingContext = new CategoriesViewModel();
            BindingContext = bindingContext;

        }

        #region functions
        async void OnCategorySelected(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CategoryLevel(bindingContext.CurrentCategory.Id));
            App.CurrenyCategory = bindingContext.CurrentCategory.Id;
        }
        #endregion

    }
}
