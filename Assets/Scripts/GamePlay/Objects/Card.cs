using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class Card
    {
        public enum CardSuit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public CardSuit Suit { get; set; } = default;
        public int Index { get; set; } = 0;
        public int Stat { get; set; } = 0;
        public int Price { get; set; } = 0;

        public Card(CardSuit suit, int index)
        {
            Suit = suit;
            Index = index;
        }

        public Card(CardSuit suit, int index, int price) : this(suit, index)
            => Price = price;

        public Card(CardSuit suit, int index, int price, int stat) : this(suit, index, price)
            => Stat = stat;
    }
}