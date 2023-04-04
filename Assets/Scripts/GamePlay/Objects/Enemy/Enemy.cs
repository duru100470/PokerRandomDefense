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
        [SerializeField]
        private int maxHealth;
        public int Health { get; set; }
        public int Damage { get; set; }

        protected virtual void Awake()
        {
            Health = maxHealth;
        }

        protected virtual void Update()
        {
            if (transform.position.x > 15) EnemyFactory.Destroy(this);
        }

        public void GetDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0) EnemyFactory.Destroy(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            collision.GetComponent<Player>().GetDamage(Damage);

            Destroy(gameObject);
        }
    }
}