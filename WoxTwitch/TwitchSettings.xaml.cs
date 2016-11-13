using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WoxTwitch
{
    /// <summary>
    /// Interaction logic for TwitchSettings.xaml
    /// </summary>
    public partial class TwitchSettings : UserControl
    {
        private Settings settings;

        public TwitchSettings(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (settings.Launch == Launch.Browser)
            {
                radioLivestreamer.IsEnabled = false;
                radioBrowser.IsEnabled = true;
                radioBrowser.IsChecked = true;
            }
            else
            {
                radioLivestreamer.IsEnabled = true;
                radioLivestreamer.IsChecked = true;
            }
        }
    }
}
