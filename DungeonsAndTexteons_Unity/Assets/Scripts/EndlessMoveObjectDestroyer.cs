using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    [RequireComponent(typeof(BoxCollider2D))]
    public class EndlessMoveObjectDestroyer : MonoBehaviour
    {
        #region Fields

        public Color gizmoColor = Color.white;
        public BoxCollider2D boxCollider;

        #endregion

        #region Unity Callbacks

        private void OnDrawGizmos()
        {
            if (boxCollider != null)
            {
                Gizmos.color = gizmoColor;

                Gizmos.DrawLine(transform.position + new Vector3(boxCollider.size.x / 2, -boxCollider.size.y / 2),
                                transform.position + new Vector3(boxCollider.size.x / 2, boxCollider.size.y / 2));
                Gizmos.DrawLine(transform.position + new Vector3(boxCollider.size.x / 2, boxCollider.size.y / 2),
                                transform.position + new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2));
                Gizmos.DrawLine(transform.position + new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2),
                                transform.position + new Vector3(-boxCollider.size.x / 2, -boxCollider.size.y / 2));
                Gizmos.DrawLine(transform.position + new Vector3(-boxCollider.size.x / 2, -boxCollider.size.y / 2),
                                transform.position + new Vector3(boxCollider.size.x / 2, -boxCollider.size.y / 2));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var endlessMovementObject = collision.gameObject.GetComponent<EndlessMovement>();
            if (endlessMovementObject != null)
            {
                DestroyObject(endlessMovementObject);
            }

            var projectile = collision.gameObject.GetComponent<Projectile>();
            if(projectile != null)
            {
                DestroyObject(projectile);
            }
        }

        #endregion

        #region Methods

        private void DestroyObject(EndlessMovement endlessMovementObject)
        {
            endlessMovementObject.Destroy();
        }

        private void DestroyObject(Projectile projectile)
        {
            projectile.Destroy();
        }

        #endregion
    }

}
