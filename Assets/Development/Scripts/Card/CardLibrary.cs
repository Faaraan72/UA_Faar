using UnityEngine;
using System.Collections.Generic;

namespace UAFaar.Cards
{
    [CreateAssetMenu(menuName = "UAFaar/Card Library")]
    public class CardLibrary : ScriptableObject
    {
        public List<CardData> Cards;

        // Generate random  Pairs
        public List<CardData> GetRandomPairs(int pairCount)
        {
            var selected = new List<CardData>();

            var shuffled = new List<CardData>(Cards);
            Shuffle(shuffled);

            for (int i = 0; i < pairCount; i++)
            {
                selected.Add(shuffled[i]);
                selected.Add(shuffled[i]);
            }

            Shuffle(selected);
            return selected;
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
