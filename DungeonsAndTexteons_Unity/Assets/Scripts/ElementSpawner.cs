using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public enum SpawnSide
    {
        Top,
        Bot,
        Left,
        Right,
    }

    public class ElementSpawner : Singleton<ElementSpawner>
    {
        #region Fields

        public Camera ortoCamera;
        public Color gizmoColor = Color.blue;
        public float topBound = 10f;
        public float botBound = 10f;
        public float leftBound = 10f;
        public float rightBound = 10f;

        #endregion

        #region Unity Callbacks

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;

            Gizmos.DrawLine(new Vector3(rightBound, botBound, 0), new Vector3(rightBound, topBound, 0));
            Gizmos.DrawLine(new Vector3(rightBound, topBound, 0), new Vector3(leftBound, topBound, 0));
            Gizmos.DrawLine(new Vector3(leftBound, topBound, 0), new Vector3(leftBound, botBound, 0));
            Gizmos.DrawLine(new Vector3(leftBound, botBound, 0), new Vector3(rightBound, botBound, 0));
        }

        private void FixedUpdate()
        {
            if (ortoCamera != null)
            {
                topBound = ortoCamera.orthographicSize + 1f;
                botBound = -(ortoCamera.orthographicSize + 1f);
            }
        }

        #endregion

        #region Methods

        public void SpawnElement(GameObject objectSpawned, float normPosition, SpawnSide spawnSide)
        {
            switch (spawnSide)
            {
                case SpawnSide.Top:
                    objectSpawned.transform.position = new Vector3(Mathf.Lerp(leftBound, rightBound, normPosition), topBound, 0);
                    break;
                case SpawnSide.Bot:
                    objectSpawned.transform.position = new Vector3(Mathf.Lerp(leftBound, rightBound, normPosition), botBound, 0);
                    break;
                case SpawnSide.Right:
                    objectSpawned.transform.position = new Vector3(rightBound, Mathf.Lerp(botBound, topBound, normPosition), 0);
                    break;
                case SpawnSide.Left:
                    objectSpawned.transform.position = new Vector3(leftBound, Mathf.Lerp(botBound, topBound, normPosition), 0);
                    break;
            }
        }

        #endregion
    }

}