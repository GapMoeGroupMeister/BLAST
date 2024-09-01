using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public TechTreeSO treeSO;
    public Dictionary<NodeSO, Node> nodeDic;

    public Transform edgeParent;
    private string _path;

    private void Awake()
    {
        nodeDic = new Dictionary<NodeSO, Node>();
        _path = Path.Combine(Application.dataPath, "TechTree.json");

        int childCnt = transform.childCount;

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.node.id) continue;

                    nodeDic.Add(node.node, node);
                }
            }
        }

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            if (nodeDic.TryGetValue(treeSO.nodes[i], out Node node))
            {
                node.Init(this, node.node.isStartNode);
            }
        }

        Load();
    }

    public Node GetNode(NodeSO nodeSO) => nodeDic[nodeSO];

    public Node GetNode(int id)
    {
        //일단 기본으로 첫번째 놈 반환

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            if (treeSO.nodes[i].id == id)
            {
                NodeSO nodeSO = treeSO.nodes[i];
                return nodeDic[nodeSO];
            }
        }
        return null;
    }

    public void Save()
    {
        TechTreeSave save = new TechTreeSave();

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            Node node = GetNode(treeSO.nodes[i].id);
            if (node != null)
            {
                save.nodeEnable.Add(node.IsNodeEnable);
            }
        }

        string json = JsonUtility.ToJson(save, true);
        Debug.Log(json);
        File.WriteAllText(_path, json);
    }

    public void Load()
    {
        if (File.Exists(_path) == false) return;

        string json = File.ReadAllText(_path);
        TechTreeSave save = JsonUtility.FromJson<TechTreeSave>(json);

        for (int i = 0; i < save.nodeEnable.Count; i++)
        {
            Node node = GetNode(treeSO.nodes[i].id);
            bool isEnable = save.nodeEnable[i];

            node.Init(this, isEnable);
        }
    }
}


[Serializable]
public class TechTreeSave
{
    public List<bool> nodeEnable = new List<bool>();
}
