using System;
using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Market
    {
        private readonly GameStats _gameStats;
        private readonly IDeck _deck;
        private Card[] cardArray;
        private int deckCount = 0;
        [SerializeField]
        private int availableCardCount = 5;
        [SerializeField]
        private int rerollPrice = 2;

        public Card[] CardArray => cardArray;

        public Market(GameStats gameStats, IDeck deck)
        {
            _gameStats = gameStats;
            _deck = deck;

            // Init cards for sale
            cardArray = new Card[availableCardCount];
            for (int i = 0; i < availableCardCount; i++)
            {
                cardArray[i] = _deck.Draw();
            }
        }

        public Card Buy(int index)
        {
            Card card = cardArray[index];
            if (card is null) return null;

            if (_gameStats.Gold < card.Price) throw new NotEnoughGoldException();

            _gameStats.Gold -= card.Price;
            cardArray[index] = null;

            return card;
        }

        public void Reroll()
        {
            if (_gameStats.Gold < rerollPrice) throw new NotEnoughGoldException();

            for (int i = 0; i < 5; i++)
            {
                cardArray[i] = _deck.Draw();
            }
        }

        public class NotEnoughGoldException : Exception { }
    }
}
