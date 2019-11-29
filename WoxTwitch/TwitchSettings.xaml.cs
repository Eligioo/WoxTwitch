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
        private string localDirectory;

        public TwitchSettings(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (settings.Launch == Launch.Browser)
            {
                radioLocal.IsEnabled = true;
                radioBrowser.IsEnabled = true;
                radioBrowser.IsChecked = true;
                textLocal.IsEnabled = false;
            }
            else if(settings.Launch == Launch.Local)
            {
                radioLocal.IsEnabled = true;
                radioLocal.IsChecked = true;
            }

            twtopTextBox.Text = settings.Twtop;
            twgamesTextBox.Text = settings.Twgames;
            twstreamTextBox.Text = settings.Twstream;
            twchannelTextBox.Text = settings.Twchannel;

            twtopTextBox.LostFocus += (o, re) =>
            {
                settings.Twtop = twtopTextBox.Text.TrimEnd(' ');
            };

            twstreamTextBox.LostFocus += (o, re) =>
            {
                settings.Twstream = twstreamTextBox.Text.TrimEnd(' ');
            };

            twchannelTextBox.LostFocus += (o, re) =>
            {
                settings.Twchannel = twchannelTextBox.Text.TrimEnd(' ');
            };

            twgamesTextBox.LostFocus += (o, re) =>
            {
                settings.Twgames = twgamesTextBox.Text.TrimEnd(' ');
            };

            radioBrowser.Checked += (o, re) =>
            {
                settings.Launch = Launch.Browser;
            };

            radioLocal.Checked += (o, re) =>
            {
                settings.Launch = Launch.Local;
            };

            textLocal.LostFocus += (o, re) =>
            {
                settings.LocalLocation = this.localDirectory;
            };

            textLocal.Text = settings.LocalLocation;
        }

        private void RadioBrowserEvent(object sender, RoutedEventArgs e)
        {
            settings.Launch = Launch.Browser;
            textLocal.IsEnabled = false;
        }

        private void RadioLocalEvent(object sender, RoutedEventArgs e)
        {
            settings.Launch = Launch.Local;
            textLocal.IsEnabled = true;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "streamlink.exe";
            dlg.Filter = "Streamlink|streamlink.exe|Livestreamer|livestreamer.exe";
            Nullable<bool> result = dlg.ShowDialog();
            if (result.Value == true)
            {
                textLocal.Text = dlg.InitialDirectory + dlg.FileName;
                this.localDirectory = dlg.InitialDirectory + dlg.FileName;
            }
        }
    }
}
