using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PokerRandomDefense.GamePlay.Stats;
using VContainer;

namespace PokerRandomDefense.UI
{
    public class StatsView : MonoBehaviour
    {
        [Inject]
        private readonly GameStats _stats;
        [SerializeField]
        private TextMeshProUGUI health;
        [SerializeField]
        private TextMeshProUGUI wave;
        [SerializeField]
        private TextMeshProUGUI gold;

        private void Start()
        {
            _stats.Health.OnValueChanged += UpdateHealth;
            _stats.Gold.OnValueChanged += UpdateGold;
            _stats.Wave.OnValueChanged += UpdateWave;

            _stats.Health.Notify();
            _stats.Gold.Notify();
            _stats.Wave.Notify();
        }

        private void UpdateHealth(int prev, int next)
        {
            health.text = next.ToString();
        }

        private void UpdateWave(int prev, int next)
        {
            wave.text = next.ToString();
        }

        private void UpdateGold(int prev, int next)
        {
            gold.text = next.ToString();
        }
    }
}