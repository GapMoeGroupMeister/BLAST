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
        [SerializeField] private InputReader _inputReader;
        
        private void Awake()
        {
            _inputReader = GameManager.Instance.InputReader;
        }

        public void SpawnObjects()
        {
            // 랜덤 시드 설정
            Random.InitState(_seed);

            // 맵 오브젝트 생성 - 오브젝트끼리 겹치지 않게 생성
            for (int i = 0; i < 100; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-MapSize / 2, MapSize / 2), 1, Random.Range(-MapSize / 2, MapSize / 2)) + transform.position;
                Collider[] colliders = Physics.OverlapSphere(pos, 0.5f, LayerMask.GetMask("Map"));

                {
                    GameObject obj = Instantiate(_mapObjs[Random.Range(0, _mapObjs.Length)], pos, Quaternion.identity);
                    obj.transform.SetParent(transform);
                    obj.transform.localScale = Vector3.one * Random.Range(0.1f, 0.15f);
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

            // 이동할 축을 결정
            if (Mathf.Abs(diffX) > Mathf.Abs(diffZ))
            {
                // X축 방향 이동
                float moveX = diffX > 0 ? MapSize * 3 : -MapSize * 3;
                transform.position += new Vector3(moveX, 0, 0);
            }
            else
            {
                // Z축 방향 이동
                float moveZ = diffZ > 0 ? MapSize * 3 : -MapSize * 3;
                transform.position += new Vector3(0, 0, moveZ);
            }

            // 새 위치에서 오브젝트 생성 (옵션)
            SpawnObjects();
        }

    }

}
