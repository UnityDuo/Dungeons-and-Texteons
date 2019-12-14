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
                    offsetMultiplier = 1f;
                    sizeMultiplier = 1.45f;
                    collToControl.offset = new Vector2(0, offsetMultiplier * ortoCamera.orthographicSize);
                    collToControl.size = new Vector2(sizeMultiplier * ortoCamera.orthographicSize, 1);
                    break;
                case CameraBound.Rear:
                    offsetMultiplier = -1f;
                    sizeMultiplier = 1.45f;
                    collToControl.offset = new Vector2(0, offsetMultiplier * ortoCamera.orthographicSize);
                    collToControl.size = new Vector2(sizeMultiplier * ortoCamera.orthographicSize, 1);
                    break;
                case CameraBound.Right:
                    offsetMultiplier = .62f;
                    sizeMultiplier = 2f;
                    collToControl.offset = new Vector2(offsetMultiplier * ortoCamera.orthographicSize, 0);
                    collToControl.size = new Vector2(1, sizeMultiplier * ortoCamera.orthographicSize);
                    break;
                case CameraBound.Left:
                    offsetMultiplier = -.62f;
                    sizeMultiplier = 2;
                    collToControl.offset = new Vector2(offsetMultiplier * ortoCamera.orthographicSize, 0);
                    collToControl.size = new Vector2(1, sizeMultiplier * ortoCamera.orthographicSize);
                    break;
            }
        }

        #endregion
    }

}
