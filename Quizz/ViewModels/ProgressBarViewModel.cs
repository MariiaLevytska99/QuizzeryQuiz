using System;
using System.ComponentModel;
using System.Diagnostics;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

using Quizz.Views;

namespace Quizz.ViewModel
{
	public class ProgressBarViewModel : INotifyPropertyChanged
	{
		#region parameters

		private float progress = 0.0f;
		public bool isExpired = true;

		#endregion

		#region get set

		public float Progress
		{
			get { return progress; }
			set
			{
				progress = value;
				OnPropertyChanged("Progress");
			}
		}

		public Color ProgressColor
		{
			get { return Color.FromHex("3498DB"); }
		}

		public Color ProgressBackgroundColor
		{
			get { return Color.FromHex("B4BCBC"); }
		}

        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region methods

        public void OnPropertyChanged(string propertyname)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		public void StartTimer()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			var start = TimeSpan.FromMinutes(1);
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				start -= TimeSpan.FromSeconds(1);
				Progress = (float)(1 - start.TotalSeconds / 60);


				if (start == TimeSpan.Zero && isExpired)
				{
					var _popup = new Popup(false);
					PopupNavigation.Instance.PushAsync(_popup);
					stopwatch.Stop();
				}
				if (!stopwatch.IsRunning)
				{

					return false;

				}
				else
				{

					return true;
				}
			});
		}

        #endregion
    }


}
