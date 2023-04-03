using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.Infrastructure;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class EnemyFactory : ObjectPool<Enemy>
    {
        List<Enemy> enemyList = new List<Enemy>();
        public List<Enemy> EnemyList => enemyList;

        public override void Destroy(Enemy obj)
        {
            enemyList.Remove(obj);
            base.Destroy(obj);
        }
    }
}