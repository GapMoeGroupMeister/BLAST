using System;
using System.Collections;
using Crogen.PowerfulInput;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BLAST.MapSystem
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapObstacle[] _mapObjs;
        [SerializeField] private int _seed;
        [field:SerializeField] public int MapSize { get; private set; }
        [SerializeField] private float _minObjSize = 0.1f;
        [SerializeField] private float _maxObjSize = 0.15f;
        private InputReader _inputReader;
        private NavMeshSurface _nav;
        private NavMeshData _navMeshData;
        private bool _isBaked = false;
        
        private void Awake()
        {
            _inputReader = GameManager.Instance.InputReader;
            _nav = GetComponent<NavMeshSurface>();
            _navMeshData = new NavMeshData();
            _nav.navMeshData = _navMeshData;
        }

        public void SpawnObjects()
        {
            Random.InitState(_seed);

            for (int i = 0; i < 100; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-MapSize / 2, MapSize / 2), 1, Random.Range(-MapSize / 2, MapSize / 2)) + transform.position;
                int randomIdx = Random.Range(0, _mapObjs.Length);
                Vector3 scale = Vector3.one * Random.Range(_minObjSize, _maxObjSize);
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                _mapObjs[randomIdx].Spawn(pos, transform, scale, rotation);
            }
            if (!_isBaked)
            {
                _isBaked = true;
                StartCoroutine(BakeNavMesh());
            }
            else 
            {
                StartCoroutine(UpdateNavMesh());
            }
        }

        private IEnumerator BakeNavMesh()
        {
            yield return null;
            _nav.BuildNavMesh();
        }

        private IEnumerator UpdateNavMesh()
        {
            yield return _nav.UpdateNavMesh(_navMeshData);
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
