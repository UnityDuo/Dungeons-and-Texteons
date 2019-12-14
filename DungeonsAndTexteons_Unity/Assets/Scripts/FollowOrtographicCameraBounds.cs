using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class FollowOrtographicCameraBounds : MonoBehaviour
    {
        #region Fields

        public enum CameraBound
        {
            Front,
            Rear,
            Left,
            Right,
        }

        public BoxCollider2D collToControl;
        public CameraBound sideToFollow;
        public Camera ortoCamera;
        public bool controlSize = true;
        public bool controlOffset = true;

        #endregion

        #region Unity Callbacks

        private void FixedUpdate()
        {
            ReplaceCollider();
        }

        #endregion

        #region Methods

        private void ReplaceCollider()
        {
            float offsetMultiplier = 0;
            float sizeMultiplier = 0;
            switch (sideToFollow)
            {
                case CameraBound.Front:
                    if (controlOffset)
                    {
                        offsetMultiplier = 1f;
                        collToControl.offset = new Vector2(0, offsetMultiplier * ortoCamera.orthographicSize);
                    }
                    if (controlSize)
                    {
                        sizeMultiplier = 1.45f;
                        collToControl.size = new Vector2(sizeMultiplier * ortoCamera.orthographicSize, 1);
                    }
                    break;
                case CameraBound.Rear:
                    if (controlOffset)
                    {
                        offsetMultiplier = -1f;
                        collToControl.offset = new Vector2(0, offsetMultiplier * ortoCamera.orthographicSize);
                    }
                    if (controlSize)
                    {
                        sizeMultiplier = 1.45f;
                        collToControl.size = new Vector2(sizeMultiplier * ortoCamera.orthographicSize, 1);
                    }
                    break;
                case CameraBound.Right:
                    if (controlOffset)
                    {
                        offsetMultiplier = .62f;
                        collToControl.offset = new Vector2(offsetMultiplier * ortoCamera.orthographicSize, 0);
                    }
                    if (controlSize)
                    {
                        sizeMultiplier = 2f;
                        collToControl.size = new Vector2(1, sizeMultiplier * ortoCamera.orthographicSize);
                    }
                    break;
                case CameraBound.Left:
                    if (controlOffset)
                    {
                        offsetMultiplier = .62f;
                        collToControl.offset = new Vector2(offsetMultiplier * ortoCamera.orthographicSize, 0);
                    }
                    if (controlSize)
                    {
                        sizeMultiplier = 2f;
                        collToControl.size = new Vector2(1, sizeMultiplier * ortoCamera.orthographicSize);
                    }
                    break;
            }
        }

        #endregion
    }

}
