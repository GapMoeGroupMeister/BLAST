using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Dijkstra
{
    public static int[] GetPathOnTarget(List<Node> tree, (int, int, int)[] nodes, int start, int target)
    {
        Priority_Queue q = new Priority_Queue();
        List<int> visit = new List<int>();

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].Item1 == start)
            {
                q.Enqueue((start, nodes[i].Item2));
            }
            else if (nodes[i].Item2 == start)
            {
                q.Enqueue((start, nodes[i].Item1));
            }
        }

        while (!q.IsEmpty())
        {
            int currentPosition = q.Peek().Item1;
            int currentCost = q.Peek().Item2;

        }

        return visit.ToArray();
    }
}


/// <summary>
/// 일단 지금은 int, int 튜플로만 사용해서 제네릭으로 안해도 도미
/// </summary>
public class Priority_Queue
{
    private List<(int, int)> pQueue = new List<(int, int)>();

    public (int, int) Peek() => pQueue[0];

    public void Enqueue((int, int) value)
    {
        if (pQueue.Count <= 0)
        {
            pQueue.Add(value);
            return;
        }

        for (int i = 0; i < pQueue.Count; i++)
        {
            if (pQueue[i].Item1 > value.Item1)
            {
                pQueue.Insert(i, value);
                return;
            }
            pQueue.Add(value);
        }
    }

    public (int, int) Dequeue()
    {
        var value = pQueue[0];
        pQueue.Remove(value);

        return value;
    }

    public bool IsEmpty() => pQueue.Count <= 0;
}
