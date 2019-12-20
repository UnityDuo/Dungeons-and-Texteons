using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace DungeonsAndTexteons
{

    public class Mage : MonoBehaviour
    {
        #region Fields

        public float maxSpeed = 1f;
        public float timerBetweenTargetPosChange = 1f;
        public List<AIPhase> phases;
        public bool makesYouWin = true;

        [NonSerialized] public GameObject target;

        private Vector3 targetPos;
        private int currPhase = 0;
        private float nextAttackTimer = 0;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            StartCoroutine(AICoroutine());
            target = FindObjectOfType<PlayerController>().gameObject;
            if (makesYouWin)
            {
                var damageableWithHpComponent = GetComponent<DamageableWithHp>();
                if (damageableWithHpComponent != null)
                {
                    damageableWithHpComponent.DamageableDestroyed += Death;
                }
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        #endregion

        #region Methods

        private void Death()
        {
            if(makesYouWin)
            {
                GameManager.instance.LevelPassed();
            }
        }

        private void Move()
        {
            Vector3 difference = targetPos - transform.position;
            if (difference.magnitude > .5f)
            {
                transform.position += difference.normalized * maxSpeed * Time.fixedDeltaTime;
            }
        }

        private Vector3 GetTargetPos(Vector2 horizRange, Vector2 vertRange)
        {
            var elementSpawner = ElementSpawner.instance;
            return new Vector3(Mathf.Lerp(elementSpawner.leftBound+.8f, elementSpawner.rightBound-.8f, UnityEngine.Random.Range(horizRange.x, horizRange.y)),
                               Mathf.Lerp(elementSpawner.botBound+2, elementSpawner.topBound-2, UnityEngine.Random.Range(vertRange.x, vertRange.y)),
                               0);
        }

        private void Shoot(AIPattern pattern)
        {
            nextAttackTimer = UnityEngine.Random.Range(phases[currPhase].pattern.attackRate.x, phases[currPhase].pattern.attackRate.y);

            GameObject newProjectile = Instantiate(pattern.projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.up = target.transform.position - newProjectile.transform.position;
        }

        private void SetNextPhase()
        {
            if(currPhase < phases.Count-1)
            {
                currPhase++;
            }
        }   

        private IEnumerator AICoroutine()
        {
            float movementT = timerBetweenTargetPosChange;
            float attackT = 0;
            float phaseT = 0;
            while(true)
            {
                movementT += Time.deltaTime;
                attackT += Time.deltaTime;
                phaseT += Time.deltaTime;

                if(phaseT > phases[currPhase].phaseTimer)
                {
                    SetNextPhase();
                    phaseT = 0;
                }

                if(movementT > timerBetweenTargetPosChange)
                {
                    targetPos = GetTargetPos(phases[currPhase].pattern.movementHorizRange, phases[currPhase].pattern.movementVertRange);
                    movementT = 0;
                }

                if (phases[currPhase].pattern.doesAttack && phases[currPhase].pattern.projectilePrefab != null && attackT > nextAttackTimer)
                {
                    Shoot(phases[currPhase].pattern);
                    attackT = 0;
                }

                yield return null;
            }
        }

        #endregion
    }

    [System.Serializable]
    public class AIPattern
    {
        [Title("Movement")]
        [MinMaxSlider(0f, 1f)] public Vector2 movementHorizRange = new Vector2();
        [MinMaxSlider(0f, 1f)] public Vector2 movementVertRange = new Vector2();
        [Title("Combat")]
        public bool doesAttack = false;
        [MinMaxSlider(0f, 5f)] public Vector2 attackRate = new Vector2();
        public GameObject projectilePrefab;

    }

    [System.Serializable]
    public class AIPhase
    {
        public float phaseTimer = 10f;
        public AIPattern pattern = new AIPattern();
    }

}
