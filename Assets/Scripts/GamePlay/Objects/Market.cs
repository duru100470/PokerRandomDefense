using System;
using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay.Stats;
using PokerRandomDefense.Infrastructure;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Market
    {
        private readonly GameStats _gameStats;
        private readonly IDeck _deck;
        [SerializeField]
        private int availableCardCount = 5;
        [SerializeField]
        private int rerollPrice = 2;
        private ReactData<Card[]> cardArray;

        public ReactData<Card[]> CardArray => cardArray;

        public Market(GameStats gameStats, IDeck deck)
        {
            _gameStats = gameStats;
            _deck = deck;

            // Init cards for sale
            cardArray = new ReactData<Card[]>(new Card[availableCardCount]);
            for (int i = 0; i < availableCardCount; i++)
            {
                cardArray.Value[i] = _deck.Draw();
            }
        }

        public Card Buy(int index)
        {
            Card card = cardArray.Value[index];
            if (card is null) return null;

            if (_gameStats.Gold < card.Price) throw new NotEnoughGoldException();

            _gameStats.Gold -= card.Price;
            cardArray.Value[index] = null;

            cardArray.Notify();
            return card;
        }

        public void Reroll()
        {
            if (_gameStats.Gold < rerollPrice) throw new NotEnoughGoldException();

            for (int i = 0; i < 5; i++)
            {
                cardArray.Value[i] = _deck.Draw();
            }

            cardArray.Notify();
        }

        public class NotEnoughGoldException : Exception { }
    }
}
