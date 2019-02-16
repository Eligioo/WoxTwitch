using Newtonsoft.Json;
using System;
using System.IO;

namespace WoxTwitch
{
    internal static class SettingsFile
    {
        private const string Filename = "twitch_settings.json";

        /// <summary>
        /// Reads the settings file into a <see cref="Settings"/> object.
        /// </summary>
        public static Settings Read(string path)
        {
            var location = Path.Combine(path, Filename);

            Settings deserialized;
            try
            {
                if (!File.Exists(location))
                {
                    deserialized = new Settings();
                    Write(path, deserialized);
                }
                else
                {
                    string contents = File.ReadAllText(location);
                    deserialized = JsonConvert.DeserializeObject<Settings>(contents);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to read {Filename}. Reverting to default settings.{Environment.NewLine}" +
                    $"Error: {e.Message}");
                deserialized = new Settings();
            }

            return deserialized;
        }

        /// <summary>
        /// Overwrites the settings file with the provided <see cref="Settings"/> object.
        /// </summary>
        public static void Write(string path, Settings settings)
        {
            try
            {
                string contents = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(Path.Combine(path, Filename), contents);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to save {Filename}.{Environment.NewLine}" +
                    $"Error: {e.Message}");
            }
        }
    }
}
