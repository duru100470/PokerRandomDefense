using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PokerRandomDefense.UI
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI cardName;

        public void SetName(string name)
        {
            cardName.text = name;
        }
    }
}