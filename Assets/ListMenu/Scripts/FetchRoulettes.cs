using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace ListMenu
{
    public class FetchRoulettes : MonoBehaviour
    {
        // prywatny stan
        private Core core => Core.GetInstance();

        public void Start()
        {
            List<RouletteWithoutItems> records = FetchRouletteList();
            Context.RouletteList = records;

            core.RenderRouletteList();
        }

        private List<RouletteWithoutItems> FetchRouletteList()
        {
            try
            {
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync($"{Shared.Context.BASE_API_URL}/Roulettes").Result;

                List<RouletteWithoutItems> records = JsonConvert.DeserializeObject<List<RouletteWithoutItems>>(response);
                return records;
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Request error: {e.Message}");
                return new List<RouletteWithoutItems>();
            }
        }
    }
}