
    using System;
    using UnityEngine;

    public abstract class PowerBase: MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        public abstract int GetPower();
    }