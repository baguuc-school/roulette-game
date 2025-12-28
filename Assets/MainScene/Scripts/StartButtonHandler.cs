using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System;
using System.Collections;
using System.Net.Http;
using UnityEngine;

namespace MainScene
{
    public class StartButtonHandler : MonoBehaviour
    {
        // prywatny stan
        private Core core => Core.GetInstance();
        
        /// <summary>
        /// Handler kliknięcia przycisku Start, wysyła żądanie POST do endpointu Rewards i uruchamia animację losowania nagrody.
        /// </summary>
        public void HandleClick()
        {
            if (Context.CurrentItemPool == null || Context.CurrentItemPool.Count < 5)
            {
                return;
            }

            GeneratedReward reward = PostReward();
            StartCoroutine(RollingAnimation(reward));
        }

        /// <summary>
        /// Wywołuje animację losowania nagrody dla wygenerowanej nagrody. (Do użytku z StartCoroutine)
        /// </summary>
        /// <param name="forReward">wygenerowana nagroda na której ruletka ma się zatrzymać</param>
        private IEnumerator RollingAnimation(GeneratedReward forReward)
        {
            core.ClearRewardIndicatorLabel();
            // obróć się w pełni 2 razy (wykonaj <ilość wejść w currentItemPool> przesunięć) dla imitacji losowości
            for (int i = 0; i < Context.CurrentItemPool.Count * 2; i++)
            {
                core.RotateRoulette(); 
                yield return new WaitForSeconds(0.1f);
            }

            // dopóki wylosowany element nie jest na środku ekranu, przesuwaj elementy
            while (Context.CurrentItemPool[2].id != forReward.itemId)
            { 
                core.RotateRoulette(); 
                yield return new WaitForSeconds(0.1f);
            }

            core.SetRewardIndicatorLabel($"Twoja nagroda: {forReward.item.name}!");
        }

        /// <summary>
        /// Wysyła żądanie POST do endpointu Rewards w celu wygenerowania nagrody, jeżeli nie może wysłać zapytania, zamyka aplikację.
        /// </summary>
        /// <returns>Wygenerowaną nagrodę</returns>
        private GeneratedReward PostReward()
        {
            try
            {
                HttpClient client = new HttpClient();

                string bodyJson = JsonConvert.SerializeObject(new
                {
                    Username = Context.Username,
                    RouletteId = Context.SelectedRouletteId
                });
                HttpContent bodyContent = new StringContent(bodyJson);
                bodyContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = client
                    .PostAsync($"{Context.BaseApiUrl}/Rewards", bodyContent)
                    .Result;

                string responseBody = response
                    .Content
                    .ReadAsStringAsync()
                    .Result;
                GeneratedReward reward = JsonConvert.DeserializeObject<GeneratedReward>(responseBody);
                
                return reward;
            }
            catch (Exception e)
            {
                Debug.LogError($"Request error: {e.Message}");
                Utils.Exit();

                // nie powinno się nigdy zdarzyć, ponieważ Utils.Exit() zamyka aplikację, to jest tylko dla kompilatora
                throw new Exception("Failed to post reward data.");
            }
        }
    }
}