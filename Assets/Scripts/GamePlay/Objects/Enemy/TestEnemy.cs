using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.GamePlay
{
    public class TestEnemy : Enemy
    {
        protected override void Update()
        {
            base.Update();

            transform.position = transform.position + new Vector3(0.01f, 0f, 0f);
        }
    }
}