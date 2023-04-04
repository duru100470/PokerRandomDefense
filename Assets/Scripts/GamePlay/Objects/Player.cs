using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerRandomDefense.DI;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PokerRandomDefense.GamePlay
{
    public class Player : MonoBehaviour
    {
        [Inject]
        private readonly GameStats _gameStats;
        [Inject]
        private readonly Market _market;
        private LifetimeScope _scope;
        private Card[] cardArray;
        private Tower[] towerArray;
        [SerializeField]
        private int handCount = 7; // Change to inject in GameStats
        [SerializeField]
        private int towerCount = 5; // Change to inject in GameStats
        [SerializeField]
        private GameObject towerPrefab;

        public Card[] CardArray => cardArray;
        public Tower[] TowerArray => towerArray;

        private void Awake()
        {
            cardArray = new Card[handCount];
            towerArray = new Tower[towerCount];

            _scope = LifetimeScope.Find<InGameScope>();
        }

        public void BuyCard(int index)
        {
            int empty = -1;
            for (int i = 0; i < cardArray.Count(); i++)
            {
                if (cardArray[i] == null)
                {
                    empty = i;
                    break;
                }
            }
            if (empty == -1) return;

            var card = _market.Buy(index);
            if (card == null) return;

            cardArray[empty] = card;
        }

        public void SellCard(int index)
        {
            _gameStats.Gold.Value += cardArray[index].Price / 2;
            cardArray[index] = null;
        }

        public void InsertCard(int towerIndex, Card card)
        {
            if (towerArray[towerIndex] == null)
            {
                towerArray[towerIndex] = _scope.Container
                    .Instantiate(towerPrefab).GetComponent<Tower>();
            }

            towerArray[towerIndex].TryInsert(card);
        }

        public void DestroyTower(int index)
        {
            if (towerArray[index] == null) return;

            Destroy(towerArray[index].gameObject);
        }

        public void GetDamage(int amount)
        {
            _gameStats.Health.Value -= amount;
        }

        public void Test()
        {
            InsertCard(0, new Card(Card.CardSuit.Clubs, 0));
        }
    }
}