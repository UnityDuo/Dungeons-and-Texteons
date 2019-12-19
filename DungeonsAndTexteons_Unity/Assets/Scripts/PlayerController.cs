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
        public SpriteRenderer spriteRenderer;
        public AnimationCurve blinkBehaviour;

        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackwards = KeyCode.S;
        [SerializeField] private KeyCode moveRight = KeyCode.D;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;

        private float vInput = 0;
        private float hInput = 0;
        private float vSpeed = 0;
        private float hSpeed = 0;
        private Vector2 speed = Vector2.zero;
        private Color baseColor;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            baseColor = spriteRenderer.color;
        }

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

        public void Blink(float time, Color blinkColor)
        {
            StopAllCoroutines();
            StartCoroutine(BlinkCor(time, blinkColor));
        }

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

            //Graphics
            if(speed.x != 0)
            {
                spriteRenderer.gameObject.transform.localScale = new Vector3(Mathf.Lerp(.8f, 1f, 
                                1 - Mathf.InverseLerp(0, maxSpeed * Time.deltaTime, Mathf.Abs(speed.x))), 1, 1);
            }
        }

        private void Move()
        {
            transform.position += (Vector3)speed;
        }

        private IEnumerator BlinkCor(float time, Color blinkColor)
        {
            Color startingColor = baseColor;
            float t = 0;
            float a = 0;
            while(t < time)
            {
                t += Time.deltaTime;
                a = blinkBehaviour.Evaluate(t % 1);
                spriteRenderer.color = Color.Lerp(baseColor, blinkColor, a);
                yield return null;
            }
            spriteRenderer.color = baseColor;
        }

        #endregion
    }

}
