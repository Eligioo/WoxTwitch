using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Wox.Plugin;

namespace WoxTwitch
{
    public class Main : IPlugin, ISettingProvider, IPluginI18n
    {
        private PluginInitContext context { get; set; }
        private API API;

        private readonly Settings _settings;

        public void Init(PluginInitContext context){
            this.context = context;
        }

        public Main()
        {
            _settings = new Settings();
            API = new API(_settings);
        }

        List<Result> IPlugin.Query(Query query)
        {
            if (query.FirstSearch == this._settings.Twgames)
            {
                return API.TWTOPGAMES(context);
            }
            else if (query.FirstSearch == this._settings.Twtop)
            {
                return API.TWTOPSTREAMS();
            }
            else if (query.FirstSearch == this._settings.Twstream && query.SecondToEndSearch.Length >= 4)
            {
                return API.TWSEARCH(query.SecondToEndSearch);
            }

            return new List<Result>();
        }

        public Control CreateSettingPanel()
        {
            return new TwitchSettings(_settings);
        }

        public string GetTranslatedPluginTitle()
        {
            return "Twitch Browser";
        }

        public string GetTranslatedPluginDescription()
        {
            return "Browse, search and view streams on Twitch";
        }
    }
}
