using System;
using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Market : MonoBehaviour
    {
        [Inject]
        private readonly GameStats _GameStats;
        [Inject]
        private readonly Deck _Deck;
        private List<Card> availableCards = new List<Card>();
        private int deckCount = 0;
        [SerializeField]
        private int availableCardCount = 5;
        [SerializeField]
        private int rerollPrice = 2;

        public List<Card> AvailableCards => availableCards;

        public Card BuyCard(int index)
        {
            Card card = availableCards[index];
            availableCards.RemoveAt(index);
            return card;
        }

        public bool Reroll()
        {
            // if (_GameStats.Gold < rerollPrice) return false;
            // _GameStats.Gold -= rerollPrice;

            // availableCards.Clear();

            // for (int i = 0; i < availableCardCount; i++)
            // {
            //     if (_Deck.Count > 0)
            //     {
            //         availableCards.Add(_Deck.Pop());
            //     }
            //     else
            //     {
            //         RefreshDeck();
            //         availableCards.Add(_Deck.Pop());
            //     }
            // }

            return true;
        }
    }
}
