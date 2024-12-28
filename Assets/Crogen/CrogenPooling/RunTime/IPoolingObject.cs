using UnityEngine;

namespace Crogen.CrogenPooling
{
    public interface IPoolingObject
    {
        public string OriginPoolType { get; set; }
        public GameObject gameObject { get; set; }

        public void OnPop();
        public void OnPush();
    }
}