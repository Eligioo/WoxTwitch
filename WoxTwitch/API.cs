using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Wox.Plugin;
using System.Windows;

namespace WoxTwitch
{
    public class API
    {
        private List<Result> results = new List<Result>();
        private string client_id = "j5saf8c8u17xkm45dh86lbxnkw00n9j";
        private int Score = 50;
        Settings settings;

        public API(Settings settings)
        {
            this.settings = settings;
        }

        public List<Result> TWTOPGAMES(Wox.Plugin.PluginInitContext context)
        {
            Reset();
            var TopGames = new Objects.TopGame.RootObject();
            string url = URLBuilder("games/top", 10, "");
            using (var webClient = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                var jsontxt = webClient.DownloadString(url);
                TopGames = JsonConvert.DeserializeObject<Objects.TopGame.RootObject>(jsontxt);
            }
            foreach (var item in TopGames.top)
            {
                results.Add(new Result {
                    Title = item.game.name,
                    SubTitle = item.game.popularity.ToString("n0") + " viewers are currently watching!",
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        Utility.ChangeQuery(context, item.game.name, settings);
                        return false;
                    }
                });
            }
            return results;
        }

        public List<Result> TWTOPSTREAMS()
        {
            Reset();
            var TopStreams = new Objects.TopStream.RootObject();
            string url = URLBuilder("streams", 10, "");
            using (var webClient = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                var jsontxt = webClient.DownloadString(url);
                TopStreams = JsonConvert.DeserializeObject<Objects.TopStream.RootObject>(jsontxt);
            }
            foreach (var item in TopStreams.streams)
            {
                results.Add(new Result
                {
                    Title = item.channel.display_name + " - " + item.channel.status,
                    SubTitle = item.channel.game +" - "+ item.viewers.ToString("n0") + " viewers are currently watching!",
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        Launcher(item.channel.url);
                        return true;
                    }
                });
            }
            return results;
        }

        public List<Result> TWSEARCH(string query)
        {
            Reset();
            var SearchStream = new Objects.SearchStream.RootObject();
            string url = URLBuilder("search/streams", 10, query).Replace(" ", "%20");
            using (var webClient = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                var jsontxt = webClient.DownloadString(url);
                SearchStream = JsonConvert.DeserializeObject<Objects.SearchStream.RootObject>(jsontxt);
            }
            foreach (var item in SearchStream.streams)
            {
                results.Add(new Result
                {
                    Title = item.channel.display_name + " - " + item.channel.game,
                    SubTitle = item.viewers.ToString("n0") + " viewers are currently watching!",
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        Launcher(item.channel.url);
                        return true;
                    }
                });
            }
            return results;
        }

        private void Reset()
        {
            this.results.Clear();
            this.Score = 50;
        }

        private void Launcher(string launchParameter)
        {
            if (settings.Launch == Launch.Browser)
            {
                Process.Start(launchParameter);
            }
            else if (settings.Launch == Launch.Local)
            {
                if (File.Exists(settings.LocalLocation))
                {
                    Process.Start(settings.LocalLocation, launchParameter + " best");
                }
                else
                {
                    MessageBox.Show("Please setup your Twitch plugin settings correctly.");
                }
            }
        }

        private string URLBuilder(string between, int limit, string query)
        {
            string prefix = "https://api.twitch.tv/kraken/";
            string suffix = "";
            if (query == "")
            {
                suffix = "?limit=" + limit + "&client_id=" + client_id;
            }
            else
                suffix = "?limit=" + limit + "&client_id=" + client_id + "&q=" + query;
            return prefix + between + suffix;
        }
    }
}