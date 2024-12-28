using System;
using Crogen.PowerfulInput;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BLAST.MapSystem
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private GameObject[] _mapObjs;
        [SerializeField] private int _seed;
        [field:SerializeField] public int MapSize { get; private set; }
        [SerializeField] private float _minObjSize = 0.1f;
        [SerializeField] private float _maxObjSize = 0.15f;
        [SerializeField] private int _mapObjCount = 100;
        private InputReader _inputReader;
        
        private void Awake()
        {
            _inputReader = GameManager.Instance.InputReader;
        }

        public void SpawnObjects()
        {
            Random.InitState(_seed);

            for (int i = 0; i < _mapObjCount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-MapSize / 2, MapSize / 2), 1, Random.Range(-MapSize / 2, MapSize / 2)) + transform.position;
                Collider[] colliders = Physics.OverlapSphere(pos, 0.5f, LayerMask.GetMask("Map"));

                {
                    GameObject obj = Instantiate(_mapObjs[Random.Range(0, _mapObjs.Length)], pos, Quaternion.identity);
                    obj.transform.SetParent(transform);
                    obj.transform.localScale = Vector3.one * Random.Range(_minObjSize, _maxObjSize);
                    obj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                }
            }
        }



        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Player player)) return;

            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            Vector3 mapPos = transform.position;

            float diffX = playerPos.x - mapPos.x;
            float diffZ = playerPos.z - mapPos.z;

            if (Mathf.Abs(diffX) > Mathf.Abs(diffZ))
            {
                float moveX = diffX > 0 ? MapSize * 3 : -MapSize * 3;
                transform.position += new Vector3(moveX, 0, 0);
            }
            else
            {
                float moveZ = diffZ > 0 ? MapSize * 3 : -MapSize * 3;
                transform.position += new Vector3(0, 0, moveZ);
            }

            SpawnObjects();
        }

    }

}
