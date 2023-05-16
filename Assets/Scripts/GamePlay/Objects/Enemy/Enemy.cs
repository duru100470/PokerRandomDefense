using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Enemy : MonoBehaviour
    {
        [Inject]
        public ObjectPoolingSystem _pools { get; set; }
        [SerializeField]
        private int maxHealth;
        public int Health { get; set; }
        public int Damage { get; set; } = 2;

        protected virtual void Awake()
        {
            Health = maxHealth;
        }

        protected virtual void Update()
        {
            if (transform.position.x > 15) _pools[ObjectList.Enemy].Destroy(this.gameObject);
        }

        public void GetDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0) _pools[ObjectList.Enemy].Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            collision.GetComponent<Player>().GetDamage(Damage);

            _pools[ObjectList.Enemy].Destroy(this.gameObject);
        }
    }
}