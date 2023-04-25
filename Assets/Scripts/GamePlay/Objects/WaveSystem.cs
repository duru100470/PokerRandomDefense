using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.GamePlay.Stats;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class WaveSystem : MonoBehaviour
    {
        [Inject]
        EnemyFactory enemyFactory;
        [Inject]
        GameStats gameStats;

        public void StartWave()
        {
            StartCoroutine(Loop());
            gameStats.Wave.Value++;
        }

        private IEnumerator Loop()
        {
            // Todo: Wave 정보 파일 csv 읽어와서 각 웨이브에 맞는 enemy 생성하게
            for (int i = 0; i < 20; i++)
            {
                var enemy = enemyFactory.Instantiate(new Vector2(-8.5f, Random.Range(-4, 4)));
                enemyFactory.EnemyList.Add(enemy);
                yield return new WaitForSeconds(1.5f);
            }

            gameStats.Gold.Value += 20;
        }
    }
}