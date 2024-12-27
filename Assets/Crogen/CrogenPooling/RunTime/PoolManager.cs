using System.Collections.Generic;
using UnityEngine;
using Crogen.CrogenPooling;
using System;

public class PoolManager : MonoBehaviour
{
    internal static Dictionary<string, Stack<IPoolingObject>> poolDic = new Dictionary<string, Stack<IPoolingObject>>();
    public List<PoolBaseSO> poolBaseList;
    public List<PoolPair> poolingPairs;
    public static Transform Transform;

	private void Awake()
	{
        poolDic?.Clear();
        Transform = transform;
        PopCore.Init(this, poolBaseList);
        PushCore.Init(this);

		for (int i = 0; i < poolBaseList.Count; ++i)
		{
            if (poolBaseList[i] == null) continue;
            MakeObj(poolBaseList[i]);
		}
    }

	private void MakeObj(PoolBaseSO poolBase)
    {
        PoolPair[] poolPairs = poolBase.pairs.ToArray();

        int currentPairIndex = 0;

		foreach (PoolPair poolPair in poolPairs)
		{
            try
            {
                poolDic.Add(poolPair.poolType, new Stack<IPoolingObject>());
            }
            catch (System.Exception)
            {
                Debug.LogError("Press to \"Generate Enum\"");
                return;
            }
            for (int i = 0; i < poolPair.poolCount; ++i)
            {
                IPoolingObject poolingObject = CreateObject(poolPair, Vector3.zero, Quaternion.identity);
                PoolingObjectInit(poolingObject, poolPair.poolType, transform);
                ++currentPairIndex;
            }
        }
    }

    public static IPoolingObject CreateObject(PoolPair poolPair, Vector3 vec, Quaternion rot)
    {
        GameObject poolObject = Instantiate(poolPair.prefab);
        IPoolingObject poolingObject = poolObject.GetComponent<IPoolingObject>();

        poolingObject.OriginPoolType = poolPair.poolType;
        poolingObject.gameObject = poolObject;

        poolObject.transform.localPosition = vec;
        poolObject.transform.localRotation = rot;
        poolObject.transform.name = poolObject.name.Replace("(Clone)","");

        return poolingObject;
    }

    public static void PoolingObjectInit(IPoolingObject poolObject, string type, Transform parent)
	{
        poolObject.gameObject.transform.SetParent(parent);
        poolObject.gameObject.SetActive(false);
        poolDic[type].Push(poolObject);
    }
}
    