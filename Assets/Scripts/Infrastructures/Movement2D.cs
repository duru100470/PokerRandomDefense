using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.Infrastructure
{
    public class Movement2D : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 0.0f;
        [SerializeField]
        private float slowdownRate = 1f;
        [SerializeField]
        private Vector3 moveDirection = Vector3.zero;

        public float defaultmoveSpeed;
        public float angle;

        private void Awake()
        {
            defaultmoveSpeed = moveSpeed;
        }

        public float SlowdownRate => slowdownRate;
        public float MoveSpeed
        {
            set => moveSpeed = Mathf.Max(0, value);
            get => moveSpeed;
        }
        private void Update()
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        public void MoveTo(Vector3 direction)
        {
            moveDirection = direction;
            if (direction.x == 1)
            {
                angle = 0;
            }
            else if (direction.x == -1)
            {
                angle = 180;
            }
            else if (direction.y == 1)
            {
                angle = 90;
            }
            else if (direction.y == -1)
            {
                angle = 270;
            }
            this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}