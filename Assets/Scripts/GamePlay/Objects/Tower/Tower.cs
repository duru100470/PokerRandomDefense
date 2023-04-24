using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerRandomDefense.Infrastructure;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class Tower : MonoBehaviour
    {
        private readonly ReactData<List<Card>> cards = new ReactData<List<Card>>(new List<Card>());
        private (int, int) rank = (0, 0);
        private float atkSpeed = 1f; // Todo: Inject from GameStats

        public int Damage => (rank.Item1 + 1) * rank.Item2;
        public (int, int) Rank => rank;
        public float AtkSpeed => atkSpeed;
        public ReactData<List<Card>> Cards => cards;

        public void Insert(Card card)
        {
            if (cards.Value.Count >= 5) return;
            cards.Value.Add(card);
            cards.Notify();
            rank = GetRank();
        }

        public bool TryInsert(Card card)
        {
            if (cards.Value.Count >= 5) return false;
            if (cards.Value.FirstOrDefault(c => c.Suit == card.Suit && c.Index == card.Index) != default)
                return false;

            cards.Value.Add(card);
            cards.Notify();
            rank = GetRank();
            return true;
        }

        private (int, int) GetRank()
        {
            if (cards.Value.Count == 0) return (0, 0);

            var isFlush = CheckFlush();
            var pairList = GetPairList();
            var (isStraight, highNumber) = CheckStraight();

            // Straight Flush
            if (isFlush && isStraight) return (8, highNumber);

            // Four of a Kind
            if (pairList.ContainsValue(4))
            {
                var high = pairList.First(kv => kv.Value == 4).Key;
                return (7, high == 0 ? 13 : high);
            }

            // Full House
            if (pairList.ContainsValue(3) && pairList.ContainsValue(2))
                return (6, pairList.First(kv => kv.Value == 3).Key);

            // Flush
            if (isFlush)
            {
                if (cards.Value.FirstOrDefault(c => c.Index == 0) != default)
                    return (5, 13);
                else
                    return (5, highNumber);
            }

            // Straight
            if (isStraight)
                return (4, highNumber);

            // Three of a Kind
            if (pairList.ContainsValue(3))
            {
                var high = pairList.First(kv => kv.Value == 3).Key;
                return (3, high == 0 ? 13 : high);
            }

            // Two Pairs
            if (pairList.Count == 2)
            {
                if (pairList.First().Value == 0) return (2, 13);
                else return (2, pairList.Last(kv => kv.Value == 2).Key);
            }

            // One Pair
            if (pairList.Count == 1)
            {
                var high = pairList.First(kv => kv.Value == 2).Key;
                return (1, high == 0 ? 13 : high);
            }

            // High Card
            if (cards.Value.FirstOrDefault(c => c.Index == 0) != default)
                return (0, 13);
            else
                return (0, highNumber);
        }

        private bool CheckFlush()
        {
            if (cards.Value.Count != 5) return false;

            int suit = (int)cards.Value[0].Suit;
            for (int i = 1; i < 5; i++)
            {
                if (suit != (int)cards.Value[i].Suit) return false;
            }
            return true;
        }

        private (bool, int) CheckStraight()
        {
            var ret = true;
            var orderedList = cards.Value.OrderBy(c => c.Index).ToList();

            if (cards.Value.Count == 5 &&
                orderedList[0].Index == 0 &&
                orderedList[1].Index == 9 &&
                orderedList[2].Index == 10 &&
                orderedList[3].Index == 11 &&
                orderedList[4].Index == 12) return (true, 13);

            int prevIndex = orderedList[0].Index;
            for (int i = 1; i < orderedList.Count; i++)
            {
                if (orderedList[i].Index - prevIndex != 1)
                    ret = false;
                prevIndex = orderedList[i].Index;
            }

            if (cards.Value.Count != 5) return (false, orderedList[orderedList.Count - 1].Index);
            return (ret, orderedList[orderedList.Count - 1].Index);
        }

        private Dictionary<int, int> GetPairList()
        {
            Dictionary<int, int> ret = new Dictionary<int, int>();
            var orderedList = cards.Value.OrderBy(c => c.Index).ToList();

            int prevIndex = orderedList[0].Index;
            for (int i = 1; i < orderedList.Count; i++)
            {
                if (prevIndex == orderedList[i].Index)
                {
                    if (!ret.ContainsKey(prevIndex)) ret[prevIndex] = 2;
                    else ret[prevIndex]++;
                }
                prevIndex = orderedList[i].Index;
            }

            return ret;
        }
    }
}
