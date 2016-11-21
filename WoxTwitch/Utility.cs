using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WoxTwitch
{
    public class Utility
    {
        public static void ChangeQuery(Wox.Plugin.PluginInitContext context, string item, Settings settings)
        {
            context.API.ChangeQuery(settings.Twstream + " " + item, true);
        }
    }
}
