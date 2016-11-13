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
        private API API = new API();

        private readonly TwitchSettings _settings;
        private readonly PluginJsonStorage<TwitchSettings> _storage;

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
            return this.context.API.GetTranslation("wox_plugin_folder_plugin_name");
        }

        public string GetTranslatedPluginDescription()
        {
            return context.API.GetTranslation("wox_plugin_folder_plugin_description");
        }

        public void Save()
        {
            _storage.Save();
        }
    }
}
