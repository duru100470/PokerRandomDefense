using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay;
using TMPro;
using UnityEngine;

namespace PokerRandomDefense.UI
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField]
        private Tower _tower;
        [SerializeField]
        private TextMeshProUGUI cardNames;

        private void Awake()
        {
            _tower.Cards.OnValueChanged += UpdateCards;
            _tower.Cards.Notify();
        }

        private void UpdateCards(List<Card> prevCards, List<Card> cards)
        {
            var str = "";
            foreach (var card in cards)
            {
                str += card.Suit.ToString() + " " + card.Index.ToString() + "\n";
            }
            cardNames.text = str;
        }
    }
}