using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace MainScene
{
    public class FetchRoulettes : MonoBehaviour
    {
        public GameObject listButtonPrefab;
        public GameObject canvas;

        public void Start()
        {
            Debug.Log(Shared.Context.Username);
            List<RouletteRecord> records = Fetch();

            int currY = 100;

            foreach (RouletteRecord entry in records)
            {
                GameObject button = Instantiate(listButtonPrefab, transform);
                button.GetComponentInChildren<TMPro.TMP_Text>().text = entry.name;
                button.GetComponent<Button>().onClick.AddListener(() => {
                    Debug.Log($"Roulette id = {entry.Id} clicked!");
                });

                button.transform.localPosition = new Vector3(40, currY, 0);
                button.transform.SetParent(canvas.transform, true);

                currY -= 60;
            }
        }

        private List<RouletteRecord> Fetch()
        {
            try
            {
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync($"{StateManager.GetInstance().BASE_API_URL}/Roulettes").Result;
                Debug.Log($"Response: {response}");

                // Parse JSON
                List<RouletteRecord> records = JsonConvert.DeserializeObject<List<RouletteRecord>>(response);
                return records;
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Request error: {e.Message}");
                return new List<RouletteRecord>();
            }
        }
    }

    [System.Serializable]
    class RouletteRecord
    {
        public int Id;
        public string name;
    }
}