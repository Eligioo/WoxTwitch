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
    }

    public enum Launch
    {
        Browser = 0,
        Livestreamer = 1
    }
}
