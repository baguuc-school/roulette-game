using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MainScene
{
    public class Core : MonoBehaviour
    {
        // referencje z edytora
        public List<TMP_Text> TextContainers;

        // instancja core (singleton)
        public static Core Instance;

        // prywatny stan
        private System.Random random => new System.Random();

        public static Core GetInstance()
        {
            if (Instance == null)
            {
                Instance = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Core>();
            }

            return Instance;
        }

        /// <summary>
        /// Renderuje przedmioty z CurrentItemPool do kontenerów tekstu.
        /// </summary>
        public void RenderCurrentItemPool()
        {
            if (Context.CurrentItemPool == null || Context.CurrentItemPool.Count < 5)
            {
                Debug.LogError("current item pool has less than 5 items.");
                return;
            }

            int j = 0;
            foreach (TMP_Text textContainer in TextContainers)
            {
                if (Context.CurrentItemPool.Count < j + 1)
                {
                    Debug.LogError("Not enough items in roulette pool!");
                    break;
                }

                Item item = Context.CurrentItemPool[j];
                textContainer.text = item.name;
                j++;
            }
        }

        /// <summary>
        /// Obraca CurrentItemPool o jeden element do przodu.
        /// </summary>
        public void RotateCurrentItemPool()
        {
            if (Shared.Context.CurrentItemPool == null || Shared.Context.CurrentItemPool.Count < 1)
            {
                Debug.LogError("current item pool is empty.");
                return;
            }

            Item first = Shared.Context.CurrentItemPool[0];
            Shared.Context.CurrentItemPool.RemoveAt(0);
            Shared.Context.CurrentItemPool.Add(first);
        }

        /// <summary>
        /// Obraca CurrentItemPool o jeden element do przodu, renderuj¹c zmiany.
        /// </summary>
        public void RotateRoulette()
        {
            RotateCurrentItemPool();
            RenderCurrentItemPool();
        }
    }
}