using System.Collections.Generic;

namespace WoxTwitch.Objects.SearchChannel
{
    public class RootObject
    {
        public int _total { get; set; }
        public Links _links { get; set; }
        public List<Channel> channels { get; set; }
    }
}
