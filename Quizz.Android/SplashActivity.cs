using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace Quizz.Droid
{
    [Activity(Label = "Quizzery", MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme.Splash")]
    public class SplashScreen : Activity, Android.Animation.Animator.IAnimatorListener
    {
        public void OnAnimationCancel(Animator animation)
        {

        }

        public void OnAnimationEnd(Animator animation)
        {

        }

        public void OnAnimationRepeat(Animator animation)
        {

        }

        public void OnAnimationStart(Animator animation)
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.SplashLayout);

            var animation = FindViewById<Com.Airbnb.Lottie.LottieAnimationView>(Resource.Id.animation_view);

            animation.AddAnimatorListener(this);

        }

    }
}