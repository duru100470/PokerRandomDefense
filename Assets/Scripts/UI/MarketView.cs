using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.UI
{
    public class MarketView : MonoBehaviour
    {
        [Inject]
        private readonly Market _market;
        private CardSlot[] slots;

        private void Start()
        {
            slots = GetComponentsInChildren<CardSlot>();
            _market.CardArray.OnValueChanged += UpdateSlots;
            _market.CardArray.Notify();
        }

        private void UpdateSlots(Card[] prevCards, Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == null)
                {
                    slots[i].SetName("Empty");
                    continue;
                }
                slots[i].SetName(cards[i].Suit.ToString() + "\n" + cards[i].Index.ToString());
            }
        }
    }
}