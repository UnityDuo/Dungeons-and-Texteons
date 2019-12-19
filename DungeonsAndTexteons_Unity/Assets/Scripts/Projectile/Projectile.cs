using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    [RequireComponent(typeof(Damager))]
    public class Projectile : MonoBehaviour
    {
        #region Fields

        public float speed = 1f;
        public Damager damager;

        #endregion

        #region Unity Callbacks

        private void FixedUpdate()
        {
            Move();
        }

        #endregion

        #region Methods

        private void Move()
        {
            transform.position += Vector3.up * speed * Time.fixedDeltaTime;
        }

        #endregion
    }

}
