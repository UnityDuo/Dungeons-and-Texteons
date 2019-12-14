using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DungeonsAndTexteons
{

    public class PlayerController : MonoBehaviour
    {
        #region Fields

        public float maxSpeed = 5f;
        public float acceleration = 1f;
        public float deceleration = .5f;

        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackwards = KeyCode.S;
        [SerializeField] private KeyCode moveRight = KeyCode.D;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;

        private float vInput = 0;
        private float hInput = 0;
        private float vSpeed = 0;
        private float hSpeed = 0;
        private Vector2 speed = Vector2.zero;

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            CheckInputs();
        }

        private void FixedUpdate()
        {
            Move();
        }

        #endregion

        #region Methods

        private void CheckInputs()
        {
            //Get Inputs
            vInput = 0;
            hInput = 0;

            if (Input.GetKey(moveForward))
            {
                vInput++;
            }
            if (Input.GetKey(moveBackwards))
            {
                vInput--;
            }
            if (Input.GetKey(moveRight))
            {
                hInput++;
            }
            if (Input.GetKey(moveLeft))
            {
                hInput--;
            }

            //Accelerate
            vSpeed += vInput * acceleration * Time.deltaTime;
            hSpeed += hInput * acceleration * Time.deltaTime;

            //Decelerate
            if (vInput == 0 && vSpeed != 0)
            {
                vSpeed -= Mathf.Sign(vSpeed) * deceleration * Time.deltaTime;
            }
            if (hInput == 0 && hSpeed != 0)
            {
                hSpeed -= Mathf.Sign(hSpeed) * deceleration * Time.deltaTime;
            }

            //Snap speed to Zero
            if(vInput == 0 && vSpeed > -deceleration * Time.deltaTime && vSpeed < deceleration * Time.deltaTime)
            {
                vSpeed = 0;
            }
            if(hInput == 0 && hSpeed > -deceleration * Time.deltaTime && hSpeed < deceleration * Time.deltaTime)
            {
                hSpeed = 0;
            }

            //Clamp Speed
            speed = new Vector2(hSpeed, vSpeed);

            if (speed.magnitude > maxSpeed * Time.deltaTime)
            {
                speed = speed.normalized * maxSpeed * Time.deltaTime;
                vSpeed = speed.y;
                hSpeed = speed.x;
            }
        }

        private void Move()
        {
            transform.position += (Vector3)speed;
        }

        #endregion
    }

}
