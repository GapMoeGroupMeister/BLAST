using UnityEngine;

namespace BLAST.MapSystem
{
    public class MapObstacle : MonoBehaviour
    {
        public void Spawn(Vector3 pos, Transform parent, Vector3 scale, Vector3 rotation)
        {
            Instantiate(gameObject, pos, Quaternion.identity, parent);
            transform.localScale = scale;
            transform.eulerAngles = rotation;
        }
    }
}