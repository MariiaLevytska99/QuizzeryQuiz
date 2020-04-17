using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

using Quizz.Services;

namespace Quizz.Views
{
    public partial class LevelEndPopup : PopupPage
    {
        INavigation navigation;

        public LevelEndPopup(int scoreParam, int questionNumberPram, INavigation navigationParam)
        {
            InitializeComponent();
            if(App.SettingsDatabase.GetSettings().Sound)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("level_end.mp3");
            }

            _scoreTxt.Text = scoreParam + "/" + questionNumberPram;
            this.navigation = navigationParam;
        }

        #region appear methods

        private async void OnClose(object sender, EventArgs e)
        {
            CategoryLevel categoryLevel = new CategoryLevel(App.CurrenyCategory);
            MessagingCenter.Send<CategoryLevel>(categoryLevel, "Refresh Category Page");
            navigation.PopAsync(true);
            PopupNavigation.Instance.PopAllAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1.0);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }

        #endregion
    }
}
