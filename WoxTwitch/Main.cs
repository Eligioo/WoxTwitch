using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wox.Plugin;
using Wox.Infrastructure.Storage;

namespace WoxTwitch
{
    public class Main : IPlugin, ISettingProvider, IPluginI18n, ISavable
    {
        private PluginInitContext context { get; set; }
        private API API;

        private readonly Settings _settings;
        private readonly PluginJsonStorage<Settings> _storage;

        public void Init(PluginInitContext context){
            this.context = context;
        }

        public Main()
        {
            _storage = new PluginJsonStorage<Settings>();
            _settings = _storage.Load();
            API = new API(_settings);
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
            else if (query.FirstSearch == "twstream" && query.SecondToEndSearch.Length >= 4)
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
            return "Browse and search streams on Twitch";
        }

        public void Save()
        {
            _storage.Save();
        }
    }
}
