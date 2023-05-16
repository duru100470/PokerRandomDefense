using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;
using VContainer;

namespace PokerRandomDefense.GamePlay
{
    public class TowerAttack : MonoBehaviour
    {
        [SerializeField]
        Tower tower;
        TowerState curState;
        [Inject]
        public ObjectPoolingSystem _pools;

        private void Awake()
        {
            curState = TowerState.SearchTarget;
            StartCoroutine(Loop());
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                yield return null;
                switch (curState)
                {
                    case TowerState.SearchTarget:
                        if (FindClosestAttackTarget() != null)
                            curState = TowerState.TryAttack;
                        break;
                    case TowerState.TryAttack:
                        var target = FindClosestAttackTarget();
                        if (target == null) break;
                        curState = TowerState.SearchTarget;
                        SpawnProjectile(target);
                        yield return new WaitForSeconds(tower.AtkSpeed);
                        break;
                }
            }
        }

        private void SpawnProjectile(Transform attackTarget)
        {
            var projectile = _pools[ObjectList.Projectile].Instantiate(this.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Init(attackTarget, tower.Damage);
        }

        private Transform FindClosestAttackTarget()
        {
            Transform attackTarget = null;
            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i < _pools[ObjectList.Enemy].ObjList.Count; i++)
            {
                float distance = Vector3.Distance(_pools[ObjectList.Enemy].ObjList[i].transform.position, transform.position);

                if (distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = _pools[ObjectList.Enemy].ObjList[i].transform;
                }
            }

            return attackTarget;
        }

        private enum TowerState
        {
            SearchTarget,
            TryAttack
        }
    }
}