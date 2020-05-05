using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expandable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coin_stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected async void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            var expandableView = sender as ExpandableView;
            var header = expandableView?.Children
                .OfType<Grid>()
                .FirstOrDefault();
            var label = header?.Children
                .OfType<Label>()
                .LastOrDefault();

            if (label != null && e.Status == ExpandStatus.Expanding)
            {
                await label.RotateTo(180, 200, Easing.CubicIn);
            }

            if (label != null && e.Status == ExpandStatus.Collapsing)
            {
                await label.RotateTo(0, 200, Easing.CubicIn);
            }
        }
    }
}