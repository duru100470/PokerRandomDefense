using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class Deck
    {
        private readonly List<Card> deck = new List<Card>();
        private int deckCount;

        public int Count => deck.Count;

        public void RefreshDeck()
        {
            deck.Clear();

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

        public Card Draw()
        {
            if (deck.Count > 0)
                return deck.Pop();
            else
            {
                RefreshDeck();
                return deck.Pop();
            }
        }

        public void Insert(Card card)
            => deck.Add(card);
    }
}