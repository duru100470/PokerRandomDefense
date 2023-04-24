using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerRandomDefense.DI;
using PokerRandomDefense.GamePlay.Stats;
using PokerRandomDefense.Infrastructure;
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
        private ReactData<Card[]> cardArray;
        private Tower[] towerArray;
        [SerializeField]
        private int handCount = 7; // Change to inject in GameStats
        [SerializeField]
        private int towerCount = 5; // Change to inject in GameStats
        [SerializeField]
        private GameObject towerPrefab;
        [SerializeField]
        private Transform[] towerPosition;

        public ReactData<Card[]> CardArray => cardArray;
        public Tower[] TowerArray => towerArray;

        private void Awake()
        {
            cardArray = new ReactData<Card[]>(new Card[handCount]);
            towerArray = new Tower[towerCount];

            _scope = LifetimeScope.Find<InGameScope>();
        }

        public void BuyCard(int index)
        {
            int empty = -1;
            for (int i = 0; i < cardArray.Value.Count(); i++)
            {
                if (cardArray.Value[i] == null)
                {
                    empty = i;
                    break;
                }
            }
            if (empty == -1) return;

            var card = _market.Buy(index);
            if (card == null) return;

            cardArray.Value[empty] = card;
            cardArray.Notify();
        }

        public void SellCard(int index)
        {
            _gameStats.Gold.Value += cardArray.Value[index].Price / 2;
            cardArray.Value[index] = null;
            cardArray.Notify();
        }

        public void InsertCard(int towerIndex, int cardIndex)
        {
            if (towerArray[towerIndex] == null)
            {
                towerArray[towerIndex] = _scope.Container
                    .Instantiate(towerPrefab, towerPosition[towerIndex]).GetComponent<Tower>();
            }

            if (cardArray.Value[cardIndex] == null)
                return;

            if (towerArray[towerIndex].TryInsert(cardArray.Value[cardIndex]))
            {
                cardArray.Value[cardIndex] = null;
                cardArray.Notify();
            }
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
    }
}