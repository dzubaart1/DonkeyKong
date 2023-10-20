using DefaultNamespace.Barrels;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseFactory : MonoBehaviour
    {
        [SerializeField] protected GameObject BarrelPrefab;
        public abstract BaseBarrel SpawnBarrel();
    }
}