using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Spawners
{
    public class BarrelSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _startPos;
        [SerializeField] private List<BaseFactory> _factories;
        private bool _isSpawn;
        private void Start()
        {
            StartSpawn();
            StartCoroutine(SpawnBarrels());
        }

        public void StartSpawn()
        {
            _isSpawn = true;
        }

        public void StopSpawn()
        {
            _isSpawn = false;
        }

        private IEnumerator SpawnBarrels()
        {
            if (!_isSpawn)
            {
                yield break;
            }

            var chooseFactory = _factories[Random.Range(0, _factories.Count - 1)];
            var barrel = chooseFactory.SpawnBarrel();
            barrel.transform.position = _startPos.position;
            barrel.GetComponent<Rigidbody2D>().AddForce(barrel.transform.right*800f);

            yield return new WaitForSeconds(1f);
            
            StartCoroutine(SpawnBarrels());
        }
        
        
    }
}