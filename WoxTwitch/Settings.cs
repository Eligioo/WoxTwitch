using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoxTwitch
{
    public class Settings
    {
        public Launch Launch { get; set; } = Launch.Browser;
        public string LiveStreamerLocation {get; set;} = "";
        public string Twtop { get; set; } = "twtop";
        public string Twgames { get; set; } = "twgames";
        public string Twstream { get; set; } = "twstream";
    }

    public enum Launch
    {
        Browser = 0,
        Livestreamer = 1
    }
}
