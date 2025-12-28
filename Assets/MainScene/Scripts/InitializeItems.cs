using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;

namespace MainScene
{
    public class InitializeItems : MonoBehaviour
    {
        // prywatny stan
        Core core => Core.GetInstance();
        System.Random random => new System.Random();
        
        public void Start()
        {
            if(Context.SelectedRouletteId == null)
            {
                Debug.LogError("No roulette selected!");
                return;
            }

            Roulette roulette = FetchRoulette(Context.SelectedRouletteId ?? 0);
            InitializeItemPool(roulette);
            core.RenderCurrentItemPool();
        }

        /// <summary>
        /// Inicjalizuje CurrentItemPool poprzez generacjê puli nagród ruletki na podstawie jej danych.
        /// </summary>
        /// <param name="roulette">Dane ruletki</param>
        private void InitializeItemPool(Roulette roulette)
        {
            List<Item> roulettePool = new List<Item>();
            int i = roulette.items.Count;
            foreach (Item item in roulette.items.OrderBy(item => item.Value))
            {
                for (int j = 0; j < i; j++)
                {
                    roulettePool.Add(item);
                }

                i--;
            }

            Context.CurrentItemPool = random.Shuffle(roulettePool);
        }

        /// <summary>
        /// Pobiera dane ruletki zidentyfikowanej przez podane ID z API.
        /// </summary>
        /// <param name="id">ID ruletki</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private Roulette FetchRoulette(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync($"{Context.BaseApiUrl}/Roulettes/Details/{id}").Result;

                Roulette record = JsonConvert.DeserializeObject<Roulette>(response);
                return record;
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Request error: {e.Message}");
                Utils.Exit();

                // nie powinno siê tu nigdy dojœæ, to jest tylko dla kompilatora
                throw new System.Exception("Failed to fetch roulette data.");
            }
        }
    }
}