using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using crypto_stats.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using TextAlignment = Android.Views.TextAlignment;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(AndroidNavigationRenderer))]

namespace crypto_stats.Android.Renderers
{
    public class AndroidNavigationRenderer : NavigationPageRenderer
    {
        private Toolbar _toolbar;

        public AndroidNavigationRenderer(Context context) : base(context)
        {
        }

        public override void OnViewAdded(View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Toolbar))
            {
                _toolbar = (Toolbar) child;
                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            if (e.Child?.GetType() == typeof(AppCompatTextView))
            {
                var textView = (AppCompatTextView) e.Child;
                textView.Typeface = Typeface.CreateFromAsset(Context?.Assets, "GorditaBold.otf");
                textView.TextAlignment = TextAlignment.Center;
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}
