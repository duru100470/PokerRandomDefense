using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class Hand
    {
        private readonly GameStats _gameStats;
        private readonly Market _market;
        private readonly Card[] cardArray;
        [SerializeField]
        private int handCount = 7;

        public Card[] CardArray => cardArray;

        public Hand(GameStats gameStats)
        {
            _gameStats = gameStats;

            cardArray = new Card[handCount];
        }

        public void Sell(int index)
        {
            _gameStats.Gold.Value += cardArray[index].Price / 2;
            cardArray[index] = null;
        }
    }
}
