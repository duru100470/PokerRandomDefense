using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class WaveSystem : MonoBehaviour
    {
        [Inject]
        EnemyFactory enemyFactory;

        private void Awake()
        {
            StartCoroutine(Loop());
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                var enemy = enemyFactory.Instantiate(new Vector2(-8.5f, Random.Range(-4, 4)));
                enemyFactory.EnemyList.Add(enemy);
            }
        }
    }
}