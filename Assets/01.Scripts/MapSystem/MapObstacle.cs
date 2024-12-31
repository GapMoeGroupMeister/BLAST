using UnityEngine;

namespace BLAST.MapSystem
{
    public class MapObstacle : MonoBehaviour
    {
        [SerializeField] private bool _isMinusHeight = false;
        [SerializeField] private float _minusHeight = 0.5f;
        public void Spawn(Vector3 pos, Transform parent, Vector3 scale, Vector3 rotation)
        {
            var obj =Instantiate(gameObject, pos, Quaternion.identity, parent);
            obj.transform.localScale = scale;
            if (_isMinusHeight)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - _minusHeight, obj.transform.position.z);
            }
            obj.transform.eulerAngles = rotation;
        }
    }
}