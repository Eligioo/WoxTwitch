using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using Wox.Plugin;

namespace WoxTwitch
{
    public class API
    {
        private List<Result> results = new List<Result>();
        private string client_id = "j5saf8c8u17xkm45dh86lbxnkw00n9j";
        private int Score = 50;

        public List<Result> TWTOPGAMES()
        {
            Reset();
            var TopGames = new Objects.TopGame.RootObject();
            string url = URLBuilder("games/top", 10, "");
            using (var webClient = new System.Net.WebClient())
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
                        Process.Start("https://twitch.tv/directory/game/" + item.game.name);
                        return true;
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
            using (var webClient = new System.Net.WebClient())
            {
                var jsontxt = webClient.DownloadString(url);
                TopStreams = JsonConvert.DeserializeObject<Objects.TopStream.RootObject>(jsontxt);
            }
            foreach (var item in TopStreams.streams)
            {
                results.Add(new Result
                {
                    Title = item.channel.display_name +" - "+ item.channel.status,
                    SubTitle = item.channel.game +" - "+ item.viewers.ToString("n0") + " viewers are currently watching!",
                    IcoPath = "Images\\app.png",
                    Score = Score - 1,
                    Action = c =>
                    {
                        Process.Start(item.channel.url);
                        return true;
                    }
                });
            }
            return results;
        }

        public List<Result> TWSEARCHBYGAME(string query)
        {
            Reset();
            var SearchStream = new Objects.SearchStream.RootObject();
            string url = URLBuilder("search/streams", 10, "&q="+query).Replace(" ", "%20");
            using (var webClient = new System.Net.WebClient())
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
                        Process.Start(item.channel.url);
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

        private string URLBuilder(string between, int limit, string query)
        {
            string prefix = "https://api.twitch.tv/kraken/";
            string suffix = "";
            if (query == "")
            {
                suffix = "?limit=" + limit + "&client_id=" + client_id;
            }
            else
                suffix = "?limit=" + limit + "&client_id=" + client_id + query;
            return prefix + between + suffix;
        }
    }
}