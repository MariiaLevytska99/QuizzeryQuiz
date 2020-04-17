using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

using Quizz.Services;

namespace Quizz.Views
{
    public partial class Popup: PopupPage
    {
        public Popup(bool isSuccess)
        {
            InitializeComponent();
            if (isSuccess)
            {
                AnimationView.Animation = "correct.json";
                if (App.SettingsDatabase.GetSettings().Sound)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("good.mp3");
                }
                
            }
            else
            {
                AnimationView.Animation = "wrong.json";
                if (App.SettingsDatabase.GetSettings().Sound)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("wrong.mp3");
                }
                
            }
        }

        #region appear methods

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
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
