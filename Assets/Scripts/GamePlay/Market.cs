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
        private int deckCount = 0;

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
        }
    }
}
