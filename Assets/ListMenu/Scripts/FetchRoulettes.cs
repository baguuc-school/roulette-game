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
            // pobierz liste ruletek tylko jesli nie byla jeszcze pobrana
            // jesli byla to znaczy ze uzytkownik wrocil do listy z MainScene i zacacheowana lista 
            // nadal moze byc uzyta
            if (Context.RouletteList == null)
            {
                List<RouletteWithoutItems> records = FetchRouletteList();
                Context.RouletteList = records;
            }

            core.RenderRouletteList();
        }

        private List<RouletteWithoutItems> FetchRouletteList()
        {
            try
            {
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync($"{Context.BaseApiUrl}/Roulettes").Result;

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