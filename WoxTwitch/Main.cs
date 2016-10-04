using System.Collections.Generic;
using Wox.Plugin;

namespace WoxTwitch
{
    public class Main : IPlugin
    {
        private PluginInitContext context { get; set; }
        private API API = new API();

        public void Init(PluginInitContext context){
            this.context = context;
        }

        List<Result> IPlugin.Query(Query query)
        {
            if (query.FirstSearch == "twgames")
            {
                return API.TWTOPGAMES();
            }
            else if (query.FirstSearch == "twtop")
            {
                return API.TWTOPSTREAMS();
            }
            else if (query.FirstSearch == "twstream" && query.SecondToEndSearch.Length >= 2)
            {
                return API.TWSEARCH(query.SecondToEndSearch);
            }

            return new List<Result>();
        }
    }
}
