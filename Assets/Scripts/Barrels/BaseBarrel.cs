using System;
using UnityEngine;

namespace DefaultNamespace.Barrels
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseBarrel : MonoBehaviour
    {
        public abstract float GetSpeed();

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                GetComponent<Rigidbody2D>().AddForce(other.transform.right * GetSpeed(), ForceMode2D.Impulse);
            }
        }
    }
}