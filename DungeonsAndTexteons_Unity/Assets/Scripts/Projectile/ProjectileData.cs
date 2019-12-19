using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    [System.Serializable]
    public class ProjectileData
    {
        #region Fields

        public int damageMultiplier = 1;
        public float speedMultiplier = 1f;
        public int numberOfProjcetiles = 1;
        public GameObject projectileObject;

        #endregion
    }

}
