using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;

namespace MainScene
{
    public class StartButtonHandler : MonoBehaviour
    {
        // prywatny stan
        private Core core => Core.GetInstance();
        
        /// <summary>
        /// Handler klikniêcia przycisku Start, wysy³a ¿¹danie POST do endpointu Rewards i uruchamia animacjê losowania nagrody.
        /// </summary>
        public void OnClick()
        {
            if (Context.CurrentItemPool == null || Context.CurrentItemPool.Count < 5)
            {
                return;
            }

            GeneratedReward reward = PostReward();
            StartCoroutine(RollingAnimation(reward));
        }

        /// <summary>
        /// Wywo³uje animacjê losowania nagrody dla wygenerowanej nagrody. (Do u¿ytku z StartCoroutine)
        /// </summary>
        /// <param name="forReward">wygenerowana nagroda na której ruletka ma siê zatrzymaæ</param>
        private IEnumerator RollingAnimation(GeneratedReward forReward)
        {
            // obróæ siê w pe³ni 2 razy (wykonaj <iloœæ wejœæ w currentItemPool> przesuniêæ) dla imitacji losowoœci
            for (int i = 0; i < Context.CurrentItemPool.Count * 2; i++)
            {
                core.RotateRoulette(); 
                yield return new WaitForSeconds(0.1f);
            }

            // dopóki wylosowany element nie jest na œrodku ekranu, przesuwaj elementy
            while (Context.CurrentItemPool[2].id != forReward.itemId)
            { 
                core.RotateRoulette(); 
                yield return new WaitForSeconds(0.1f);
            }
        }

        /// <summary>
        /// Wysy³a ¿¹danie POST do endpointu Rewards w celu wygenerowania nagrody, je¿eli nie mo¿e wys³aæ zapytania, zamyka aplikacjê.
        /// </summary>
        /// <returns>Wygenerowan¹ nagrodê</returns>
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
                    .PostAsync($"{Context.BASE_API_URL}/Rewards", bodyContent)
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

                // nie powinno siê nigdy zdarzyæ, poniewa¿ Utils.Exit() zamyka aplikacjê, to jest tylko dla kompilatora
                throw new Exception("Failed to post reward data.");
            }
        }
    }
}