using System.Collections.Generic;

namespace WoxTwitch.Objects.SearchStream
{
    public class RootObject
    {
        public int _total { get; set; }
        public Links _links { get; set; }
        public List<Stream> streams { get; set; }
    }

}
