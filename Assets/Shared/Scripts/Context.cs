using Shared.Models;
using System.Collections.Generic;

namespace Shared
{
    public class Context
    {
        // sta³e
        public static string BASE_API_URL = "https://localhost:5000";

        // globalny stan aplikacji
        public static string Username = null;
        public static List<RouletteWithoutItems> RouletteList = new List<RouletteWithoutItems>();
        public static int? SelectedRouletteId = null;
        public static List<Item> CurrentItemPool = new List<Item>();
    }
}