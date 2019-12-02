using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Wox.Plugin;
using System.Windows;
using System.Net;

namespace WoxTwitch
{
    public class API
    {
        private List<Result> results = new List<Result>();
        private string client_id = "j5saf8c8u17xkm45dh86lbxnkw00n9j";
        private int Score = 50;
        public Settings settings { get; set; }

        public API(Settings settings)
        {
            this.settings = settings;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        //for debugging purposes
        private List<Result> ExceptionResult(string exceptionString)
        {
            return new List<Result>() {new Result
                {
                    Title = "Exception",
                    SubTitle = exceptionString,
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        return false;
                    }
                }
                };
        }

        private string TwitchAPICall(string url)
        {
            try
            {
                using (var webClient = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 })
                {
                    webClient.Headers.Add("Accept", "application/vnd.twitchtv.v5+json");
                    return webClient.DownloadString(url);
                }
            }
            catch (System.Exception e) { return e.ToString(); }
        }

        public List<Result> TWTOPGAMES(Wox.Plugin.PluginInitContext context)
        {
            Reset();
            var TopGames = new Objects.TopGame.RootObject();
            string url = URLBuilder("games/top", 10, "");
            string jsontxt = TwitchAPICall(url);
            try
            {
                TopGames = JsonConvert.DeserializeObject<Objects.TopGame.RootObject>(jsontxt);
            }
            catch { TopGames = null; }
            if (TopGames is null)
                return ExceptionResult(jsontxt);
            foreach (var item in TopGames.top)
            {
                results.Add(new Result
                {
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
            string jsontxt = TwitchAPICall(url);
            try
            {
                TopStreams = JsonConvert.DeserializeObject<Objects.TopStream.RootObject>(jsontxt);
            }
            catch { TopStreams = null; }
            if (TopStreams is null)
                return ExceptionResult(jsontxt);
            foreach (var item in TopStreams.streams)
            {
                results.Add(new Result
                {
                    Title = item.channel.display_name + " - " + item.channel.status,
                    SubTitle = item.channel.game + " - " + item.viewers.ToString("n0") + " viewers are currently watching!",
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

        public List<Result> TWSEARCHCHANNEL(string query)
        {
            Reset();
            var SearchStream = new Objects.SearchChannel.RootObject();
            string url = URLBuilder("search/channels", 10, query).Replace(" ", "%20");
            string jsontxt = TwitchAPICall(url);
            try { SearchStream = JsonConvert.DeserializeObject<Objects.SearchChannel.RootObject>(jsontxt); }
            catch { SearchStream = null; }
            if (SearchStream is null)
                return ExceptionResult(jsontxt);
            foreach (var item in SearchStream.channels)
            {
                results.Add(new Result
                {
                    Title = item.display_name + " - " + item.game,
                    SubTitle = $"{item.status} | {item.followers} followers",
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        Launcher(item.url);
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
            string jsontxt = TwitchAPICall(url);
            SearchStream = JsonConvert.DeserializeObject<Objects.SearchStream.RootObject>(jsontxt);
            try { SearchStream = JsonConvert.DeserializeObject<Objects.SearchStream.RootObject>(jsontxt); }
            catch { SearchStream = null; }
            if (SearchStream is null)
                return ExceptionResult(jsontxt);

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
                suffix = "?limit=" + limit + "&client_id=" + client_id + "&query=" + query;
            return prefix + between + suffix;
        }
    }
}