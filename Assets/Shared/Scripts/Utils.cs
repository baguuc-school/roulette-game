using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public static class Utils
    {
        public static void Exit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public static T WeightedPick<T>(IEnumerable<T> seq, Random random) where T : IWeightedItem
        {
            T[] pool = seq.OrderBy(i => i.Value).ToArray();
            int max = pool[pool.Length - 1].Value;

            Dictionary<int, T> d = new Dictionary<int, T>();

            for (int i = 0; i < pool.Length; i++)
            {
                int weight = max - pool[i].Value;
                d.Add(weight, pool[i]);
            }

            List<int> keys = d.Keys.ToList();

            double[] weights = new double[pool.Length];
            double totalWeight = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = Math.Pow(keys[i], 2);
                totalWeight += weights[i];
            }

            double randomValue = random.NextDouble() * totalWeight;

            double cumulativeWeight = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                cumulativeWeight += weights[i];
                if (randomValue < cumulativeWeight)
                {
                    return d[keys[i]];
                }
            }

            return d[keys[^1]];
        }

        public interface IWeightedItem
        {
            public int Value { get; set; }
        }

        public static List<T> Shuffle<T>(this Random rng, IEnumerable<T> collection)
        {
            T[] copy = collection.ToArray().Clone() as T[];

            int n = copy.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = copy[n];
                copy[n] = copy[k];
                copy[k] = temp;
            }

            return copy.ToList();
        }
    }
}