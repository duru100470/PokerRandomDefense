using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class Card
    {
        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public Suit mSuit { get; set; }
        public int mIndex { get; set; }
        public int mStat { get; set; }

        public Card(Suit suit, int index)
        {
            mSuit = suit;
            mIndex = index;
        }
    }
}