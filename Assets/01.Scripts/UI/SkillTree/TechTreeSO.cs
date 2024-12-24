using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "SO/TechTree")]
public class TechTreeSO : ScriptableObject
{
    public List<NodeSO> nodes = new List<NodeSO>();


#if UNITY_EDITOR
    public NodeSO CreateNode(System.Type type)
    {
        NodeSO node = ScriptableObject.CreateInstance(type) as NodeSO;

        node.id = nodes.Count;
        node.name = $"{type.Name}-{node.id}";
        node.guid = GUID.Generate().ToString();
        node.nextNodes = new List<NodeSO>();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteScript(NodeSO node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public List<NodeSO> GetConnectedScripts(NodeSO n) => n.nextNodes.ToList();
    public NodeSO GetPrevNode(NodeSO n) => n.prevNode;

    public void RemoveNextNode(NodeSO parent, NodeSO child)
    {
        //if (child.lastNodes.Contains(parent))
        //    child.lastNodes.Remove(parent);

        parent.nextNodes
            .Where((node) => node.id == child.id)
            .ToList()
            .ForEach((node) =>
            {
                parent.nextNodes.Remove(node);
                node.prevNode = null;
            });
        
        //if (parent.nextNodes.Contains(child) == false)
        //    parent.nextNodes.Remove(child);
    }
    public void AddNextNode(NodeSO parent, NodeSO child)
    {
        //if (child.lastNodes.Contains(parent) == false)
        //    child.lastNodes.Add(parent);


        bool isExsist = parent.nextNodes.Where((node) => node.id == child.id).Count() > 0;
        if (!isExsist)
        {
            parent.nextNodes.Add(child);
            child.prevNode = parent;
        }

        //if (parent.nextNodes.Contains(child) == false)
        //    parent.nextNodes.Add(child);
    }
#endif
    //public NodeSO startTechTree;
}
