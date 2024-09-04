using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TechTreeGraphView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<TechTreeGraphView, GraphView.UxmlTraits> { }

    private TechTreeSO _tree;

    public TechTreeGraphView()
    {
        Insert(0, new GridBackground());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/UI/SkillTree/Editor/TechTreeGenerator.uss");
        styleSheets.Add(styleSheet);

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    }

    private NodeView FindNodeView(NodeSO node) => GetNodeByGuid(node.guid) as NodeView;

    public void ParpurateView(TechTreeSO tree)
    {
        _tree = tree;
        CreateNodeAndEdge();
    }

    private void CreateNodeAndEdge()
    {
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        //Create Node View
        _tree.nodes.ForEach(n => CreateNodeView(n));

        //Create Edge
        _tree.nodes.ForEach(n =>
        {
            var children = _tree.GetConnectedScripts(n);

            children.ForEach(c =>
            {
                if (c != null)
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    if (parentView.output != null)
                    {
                        Edge edge = parentView.output.ConnectTo(childView.input);
                        AddElement(edge);
                    }
                }
            });
        });
    }

    private void CreateNode(Type type)
    {
        NodeSO node = _tree.CreateNode(type);
        CreateNodeView(node);
    }

    private void CreateNodeView(NodeSO n)
    {
        NodeView nodeView = new NodeView(n);

        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        Debug.Log("¹Ö¤±¤·");
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                    _tree.DeleteScript(nodeView.node);

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;

                    _tree.RemoveNextNode(parentView.node, childView.node);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _tree.AddNextNode(parentView.node, childView.node);
            });
        }

        return graphViewChange;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            NodeSO n = ScriptableObject.CreateInstance<NodeSO>();
            Type type = n.GetType();
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
        }
    }
}
