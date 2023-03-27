using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Enemy : MonoBehaviour
    {
        [Inject]
        public EnemyFactory EnemyFactory { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        protected virtual void Update()
        {
            if (transform.position.x > 15) EnemyFactory.Destroy(this);
        }
    }
}