using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Player : MonoBehaviour
    {
        [Inject]
        private readonly GameStats _gameStats;
        [Inject]
        private readonly Market _market;
        private Card[] cardArray;
        private Tower[] towerArray;
        [SerializeField]
        private int handCount = 7;

        public Card[] CardArray => cardArray;

        private void Awake()
        {
            cardArray = new Card[handCount];
            towerArray = Enumerable.Repeat<Tower>(new Tower(), 4).ToArray<Tower>();
        }

        public void SellCard(int index)
        {
            _gameStats.Gold += cardArray[index].Price / 2;
            cardArray[index] = null;
        }

        public void InsertCard(int towerIndex)
        {
            throw new NotImplementedException();
        }

        public void GetDamage(int amount)
        {
            _gameStats.Health -= amount;
        }
    }
}