using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class Projectile : MonoBehaviour
    {
        [Inject]
        public ObjectPoolingSystem _pools;
        private Movement2D movement2D;
        private Transform target;
        private int damage;

        public void Init(Transform target, int damage)
        {
            movement2D = GetComponent<Movement2D>();
            this.target = target;
            this.damage = damage;
        }

        private void Update()
        {
            if (target.gameObject.activeInHierarchy)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                movement2D.MoveTo(direction);
            }
            else
            {
                _pools[ObjectList.Projectile].Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;
            var enemy = collision.GetComponent<Enemy>();
            enemy.GetDamage(damage);

            _pools[ObjectList.Projectile].Destroy(this.gameObject);
        }
    }
}