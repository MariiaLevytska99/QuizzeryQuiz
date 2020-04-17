using Android.Media;
using Xamarin.Forms;

using Quizz.Droid;
using Quizz.Services;

[assembly: Dependency(typeof(AudioService))]
namespace Quizz.Droid
{
    public class AudioService : IAudio
	{
		public AudioService()
		{
		}

		public void PlayAudioFile(string fileName)
		{
			var player = new MediaPlayer();
			var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
			player.Prepared += (s, e) =>
			{
				player.Start();
			};
			player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
			player.Prepare();
		}
	}
}