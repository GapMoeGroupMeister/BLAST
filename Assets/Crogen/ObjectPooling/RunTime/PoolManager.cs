using System.Collections.Generic;
using UnityEngine;
using Crogen.ObjectPooling;
using System;

public class PoolManager : MonoBehaviour
{
    internal static Dictionary<PoolType, Stack<IPoolingObject>> poolDic = new Dictionary<PoolType, Stack<IPoolingObject>>();
    public PoolBaseSO poolBase;
    public List<PoolPair> poolingPairs;
    public static Transform Transform;

	private void Awake()
	{
        Transform = transform;
        PopCore.Init(this, poolBase);
        PushCore.Init(this);
        MakeObj();
    }

	private void MakeObj()
    {
        PoolPair[] poolPairs = poolBase.pairs.ToArray();

		foreach (PoolType type in Enum.GetValues(typeof(PoolType)))
		{
            try
            {
                poolDic.Add(type, new Stack<IPoolingObject>());
            }
            catch (System.Exception)
            {
                Debug.LogError("Press to \"Generate Enum\"");
                return;
            }
            for (int i = 0; i < poolPairs[(int)type].poolCount; ++i)
            {
                IPoolingObject poolingObject = CreateObject(poolPairs[(int)type], Vector3.zero, Quaternion.identity);
                PoolingObjectInit(poolingObject, type, transform);
            }
        }
    }

    public static IPoolingObject CreateObject(PoolPair poolPair, Vector3 vec, Quaternion rot)
    {
        GameObject poolObject = Instantiate(poolPair.prefab);
        IPoolingObject poolingObject = poolObject.GetComponent<IPoolingObject>();

        poolingObject.OriginPoolType = (PoolType)Enum.Parse(typeof(PoolType), poolPair.poolType);
        poolingObject.gameObject = poolObject;

        poolObject.transform.localPosition = vec;
        poolObject.transform.localRotation = rot;
        poolObject.transform.name = poolObject.name.Replace("(Clone)","");

        return poolingObject;
    }

    public static void PoolingObjectInit(IPoolingObject poolObject, PoolType type, Transform parent)
	{
        poolObject.gameObject.transform.SetParent(parent);
        poolObject.gameObject.SetActive(false);
        poolDic[type].Push(poolObject);
    }
}
    