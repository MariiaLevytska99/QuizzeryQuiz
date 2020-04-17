using Xamarin.Forms;

using Quizz.ViewModel;

namespace Quizz.Views
{
    public partial class CategoryLevel : ContentPage
    {
        #region parameters
        private CategoryLevelsViewModel bindingContext;
        private int categoryId { get; set; }
        #endregion

        public CategoryLevel(int categoryId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
            bindingContext = new CategoryLevelsViewModel(categoryId);
            bindingContext.Navigation = Navigation;
            BindingContext = bindingContext;

            MessagingCenter.Subscribe<CategoryLevel>(this, "Refresh Category Page", (sender) => {
                bindingContext = sender.bindingContext;
                BindingContext = bindingContext;
            });


        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;

        }
    }
}
