using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public NodeSO node;
    public Port input;
    public Port output;
    public TextField nameInput;
    public List<Port> outputs = new List<Port>();
    public List<Button> buttons = new List<Button>();

    public NodeView(NodeSO node)
    {
        this.node = node;

        if (node is PartNodeSO part)
            title = $"Part: {part.openPart.ToString()}";
        else if (node is WeaponNodeSO weapon)
            title = $"Weapon: {weapon.weapon.ToString()}";
        else
            title = "StartNode";

        //title = NodeType.name;
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();

        this.Add(nameInput);
    }

    private void CreateInputPorts()
    {
        if (node.id == 0) return;

        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

        if (input != null)
        {
            input.portName = "To";
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        if (output != null)
        {
            output.portName = "From";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
        Debug.Log("¹Ö¹Ö");
    }
}
