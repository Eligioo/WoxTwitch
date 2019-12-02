using System.Collections.Generic;

namespace WoxTwitch.Objects.TopStream
{
    public class RootObject
    {
        public List<Stream> streams { get; set; }
        public int _total { get; set; }
        public Links3 _links { get; set; }
    }
}
