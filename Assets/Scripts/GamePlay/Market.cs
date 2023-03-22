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
        private readonly GameStats mGameStats;
        private List<Card> deck = new List<Card>();
        private List<Card> availableCards = new List<Card>();
        private int deckCount = 0;
        [SerializeField]
        private int availableCardCount = 5;
        [SerializeField]
        private int rerollPrice = 2;

        public List<Card> AvailableCards => availableCards;

        private void Awake()
        {
            RefreshDeck();
        }

        public void RefreshDeck()
        {
            foreach (Card.Suit suit in (Card.Suit[]) Enum.GetValues(typeof(Card.Suit)))
            {
                for (int i = 0; i < 13; i++)
                {
                    deck.Add(new Card(suit, i));
                }
            }

            deck.Shuffle();
            deckCount++;
        }

        public Card BuyCard(int index)
        {
            Card card = availableCards[index];
            availableCards.RemoveAt(index);
            return card;
        }

        public bool Reroll()
        {
            if (mGameStats.Gold < rerollPrice) return false;
            mGameStats.Gold -= rerollPrice;

            availableCards.Clear();

            for (int i = 0; i < availableCardCount; i++)
            {
                if (deck.Count > 0)
                {
                    availableCards.Add(deck.Pop());
                }
                else
                {
                    RefreshDeck();
                    availableCards.Add(deck.Pop());
                }
            }

            return true;
        }
    }
}
