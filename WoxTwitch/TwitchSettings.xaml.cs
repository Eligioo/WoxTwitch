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
        private string liveStreamerDirectory;

        public TwitchSettings(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (settings.Launch == Launch.Browser)
            {
                radioLivestreamer.IsEnabled = true;
                radioBrowser.IsEnabled = true;
                radioBrowser.IsChecked = true;
                textLivestreamer.IsEnabled = false;
            }
            else if(settings.Launch == Launch.Livestreamer)
            {
                radioLivestreamer.IsEnabled = true;
                radioLivestreamer.IsChecked = true;
            }
            radioBrowser.Checked += (o, re) =>
            {
                settings.Launch = Launch.Browser;
            };
            radioLivestreamer.Checked += (o, re) =>
            {
                settings.Launch = Launch.Livestreamer;
            };
            textLivestreamer.LostFocus += (o, re) =>
            {
                settings.LiveStreamerLocation = this.liveStreamerDirectory;
            };
            textLivestreamer.Text = settings.LiveStreamerLocation;
        }

        private void RadioBrowserEvent(object sender, RoutedEventArgs e)
        {
            settings.Launch = Launch.Browser;
            textLivestreamer.IsEnabled = false;
        }

        private void RadioLivestreamerEvent(object sender, RoutedEventArgs e)
        {
            settings.Launch = Launch.Livestreamer;
            textLivestreamer.IsEnabled = true;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "livestreamer.exe";
            dlg.Filter = "Livestreamer (livestreamer.exe)|livestreamer.exe";
            Nullable<bool> result = dlg.ShowDialog();
            if (result.Value == true)
            {
                textLivestreamer.Text = dlg.InitialDirectory + dlg.FileName;
                this.liveStreamerDirectory = dlg.InitialDirectory + dlg.FileName;
            }
        }
    }
}
