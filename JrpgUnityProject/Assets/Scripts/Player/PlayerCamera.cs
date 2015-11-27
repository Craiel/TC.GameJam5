using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerCamera : MonoBehaviour
    {
        public float dampTime = 0.15f;
        private Vector3 velocity = Vector3.zero;
        public Transform target;
        //public Camera camera = Camera.main;

        void Update()
        {
            if (target)
            {
                Vector3 point = Camera.main.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
    }
}
