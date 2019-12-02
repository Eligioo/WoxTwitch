using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WoxTwitch
{
    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        // Some brilliance from: https://stackoverflow.com/a/1316417
        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                // Do not trigger PropertyChanged event if the value is the same
                return;
            }

            field = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Launch Launch
        {
            get => _launch;
            set => SetField(ref _launch, value);
        }
        private Launch _launch = Launch.Browser;

        public string LocalLocation
        {
            get => _localLocation;
            set => SetField(ref _localLocation, value);
        }
        private string _localLocation = "";

        public string Twtop
        {
            get => _twTop;
            set => SetField(ref _twTop, value);
        }
        private string _twTop = "twtop";

        public string Twgames
        {
            get => _twgames;
            set => SetField(ref _twgames, value);
        }
        private string _twgames = "twgames";

        public string Twstream
        {
            get => this._twstream;
            set => SetField(ref _twstream, value);
        }
        private string _twstream = "twstream";

        public string Twchannel
        {
            get => this._twchannel;
            set => SetField(ref _twchannel, value);
        }
        private string _twchannel = "twchannel";

    }

    public enum Launch
    {
        Browser = 0,
        Local = 1
    }
}
