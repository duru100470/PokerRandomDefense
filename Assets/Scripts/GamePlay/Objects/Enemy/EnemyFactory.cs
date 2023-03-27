using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class EnemyFactory : ObjectPool<Enemy>
    {
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var enemy = Instantiate();

                enemy.transform.position = Vector3.zero;
            }
        }
    }
}