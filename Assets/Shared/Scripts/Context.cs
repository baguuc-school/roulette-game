using Shared.Models;
using System.Collections.Generic;

namespace Shared
{
    public class Context
    {
        // globalny stan aplikacji
        public static string Username = null;
        public static string BaseApiUrl = null;
        public static List<RouletteWithoutItems> RouletteList = null;
        public static int? SelectedRouletteId = null;
        public static List<Item> CurrentItemPool = null;
    }
}