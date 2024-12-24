using Cinemachine.Utility;
using DG.Tweening;
using GGM.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    #region Use Tweening

    //public NodeSO NodeType;
    //private TechTree _tree;

    //private Stack<Node> _nodes = new Stack<Node>();
    //private Node _curEnableNode;

    //[SerializeField] private RectTransform _inputRect;
    //[SerializeField] private RectTransform _outputRect;
    //[SerializeField] private UILineRenderer _edgePf;
    //[SerializeField] private Image _icon;

    //[SerializeField] private float _enableDuration = 0.5f;
    //[SerializeField] private float _disableDuration = 0.2f;
    //private readonly string _coinLackTxt = "<color=yellow>코인</color>이 부족합니다.";

    //private bool _isNodeEnable = false;         //현재 노드가 뚫려있는지
    //private bool _isNodeActive = false;         //현재 노드를 뚫을준비가 되있는지

    //public RectTransform RectTrm => transform as RectTransform;
    //public bool IsNodeEnable => _isNodeEnable;
    //public bool IsNodeActive => _isNodeActive;

    //private Coroutine _enableCoroutine;
    //private Sequence _seq;
    //private int _requireCoin;


    //private void Awake()
    //{
    //    _icon.sprite = NodeType.icon;
    //}

    //private IEnumerator StartEnableAllNodes()
    //{
    //    if (_isNodeEnable) yield break;

    //    if (_requireCoin > GameDataManager.Instance.Coin)
    //    {
    //        _tree.warningPanel.SetText(_coinLackTxt);
    //        _tree.warningPanel.Open();
    //        yield break;
    //    }

    //    while (_nodes.TryPop(out _curEnableNode))
    //    {
    //        _curEnableNode.StartEnableNode();
    //        yield return new WaitUntil(() => _curEnableNode.IsNodeEnable);
    //    }
    //}

    //private void StopEnableAllNodes()
    //{
    //    if (_enableCoroutine != null) StopCoroutine(_enableCoroutine);
    //    _curEnableNode?.StopEnableNode();
    //    _nodes.Clear();
    //}


    //public void StartEnableNode()
    //{
    //    if (_isNodeEnable) return;

    //    //코인 부족
    //    if (NodeType.requireCoin > GameDataManager.Instance.Coin)
    //    {
    //        _tree.warningPanel.SetText(_coinLackTxt);
    //        _tree.warningPanel.Open();
    //        return;
    //    }

    //    if (_seq != null && _seq.active)
    //        _seq.Kill();

    //    _seq = DOTween.Sequence();

    //    //_seq.Append(_edge.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
    //    //    .Append(_vertex.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
    //    //    .OnComplete(EnableNode);
    //}

    //public void StopEnableNode()
    //{
    //    if (_isNodeEnable) return;

    //    if (_seq != null && _seq.active)
    //        _seq.Kill();

    //    _seq = DOTween.Sequence();
    //}



    //public void EnableNode()
    //{
    //    _isNodeEnable = true;
    //    GameDataManager.Instance.UseCoin(NodeType.requireCoin);

    //    for (int i = 0; i < NodeType.nextNodes.Count; i++)
    //    {
    //        if (_tree.TryGetNode(NodeType.nextNodes[i], out Node prevNode))
    //        {
    //            prevNode.Activenode();
    //        }
    //    }

    //    if (NodeType is PartNodeSO part)
    //        GameDataManager.Instance.EnablePart(part.openPart);
    //    if (NodeType is WeaponNodeSO weapon)
    //        GameDataManager.Instance.EnableWeapon(weapon.weapon);

    //    _tree.Save();
    //}

    //public void Init(TechTree tree, bool enable)
    //{
    //    _tree = tree;
    //    _isNodeEnable = enable;

    //    if (enable)
    //    {
    //        for (int i = 0; i < NodeType.nextNodes.Count; i++)
    //        {
    //            if (_tree.TryGetNode(NodeType.nextNodes[i], out Node prevNode))
    //                prevNode.Activenode();
    //        }
    //    }
    //}

    //private void Activenode()
    //{
    //    _isNodeActive = true;

    //    //여기 원래 안보이다가 보이게 한다든가 그런거 하게 해주셈ㅇㅇ
    //}

    //public void DestroyEdge()
    //{

    //}

    //#region Line

    //private void OnValidate()
    //{
    //    NodeType.nextNodes.ForEach(n =>
    //    {
    //        Node prevNode = _tree.GetNode(n.id);

    //        Vector2 size = RectTrm.size;

    //        Vector3 startPosition = new Vector3(size.x * 0.5f, size.y);
    //        Vector3 relativePosition = transform.InverseTransformPoint(prevNode.transform.position);
    //        Vector3 delta = relativePosition - startPosition + new Vector3(size.x * 0.5f, 0);
    //        Vector3 endPosition = startPosition + delta;

    //        Vector3 middlePosition = delta * 0.5f + startPosition;

    //        UILineRenderer edge = Instantiate(_edgePf, transform);
    //        edge.points = new Vector2[]
    //                {
    //                startPosition,
    //                new Vector2(startPosition.x, middlePosition.y),
    //                new Vector2(endPosition.x, middlePosition.y),
    //                endPosition
    //                };
    //    });
    //}

    //#endregion

    //#region Debug

    //public void SetPosition(Vector2 position) => (transform as RectTransform).anchoredPosition = position;
    //public void SetEdgePosition(Vector2 position)
    //{
    //    if (transform.Find("Edge") != null)
    //        (transform.Find("Edge") as RectTransform).anchoredPosition = position;
    //}
    //public Vector2 GetPosition() => (transform as RectTransform).anchoredPosition;
    //public Vector2 GetEdgePosition()
    //{
    //    if (transform.Find("Edge") != null)
    //        return (transform.Find("Edge") as RectTransform).anchoredPosition;

    //    return Vector2.zero;
    //}


    //#endregion


    //#region InputEvent

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (_isNodeEnable == false)
    //    {
    //        int coin = GameDataManager.Instance.Coin;
    //        _tree.selectNodeEvent?.Invoke(coin, 0);
    //    }
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (_isNodeEnable == false)
    //    {
    //        _requireCoin = 0;
    //        NodeSO curNode = NodeType;

    //        while (curNode != null && !_tree.GetNode(curNode.id)._isNodeEnable)
    //        {
    //            _requireCoin += curNode.requireCoin;
    //            _nodes.Push(_tree.GetNode(curNode.id));

    //            curNode = _tree.treeSO.nodes.Find(NodeType => NodeType.nextNodes.Contains(curNode));
    //        }

    //        int coin = GameDataManager.Instance.Coin;
    //        _tree.selectNodeEvent?.Invoke(coin, _requireCoin);
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (eventData.button != PointerEventData.InputButton.Left) return;
    //    StopEnableAllNodes();
    //    //StopEnableNode();
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (eventData.button != PointerEventData.InputButton.Left) return;
    //    _enableCoroutine = StartCoroutine(StartEnableAllNodes());
    //    //StartEnableNode();
    //}

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button != PointerEventData.InputButton.Right) return;

    //    _tree.tooltipPanel.SetNodeInformation(NodeType);
    //    _tree.tooltipPanel.Open();
    //}

    //#endregion

    #endregion

    [SerializeField] private NodeSO _nodeType;
    [SerializeField] private UILineRenderer _edge;
    private Vector2[] _offsets;
    private bool _isNodeEnable;
    private Stack<Node> _nodes;

    private TechTree _techTree => GetComponentInParent<TechTree>();
    public RectTransform RectTrm => transform as RectTransform;
    public bool IsNodeEnable => _isNodeEnable;
    public NodeSO NodeType => _nodeType;

    private void EnableNode()
    {

    }

    public void SetEdge()
    {
        if (_nodeType.prevNode == null) return;

        Vector2 size = RectTrm.sizeDelta;
        Node prevNode = _techTree.GetNode(_nodeType.prevNode.id);

        _offsets = new Vector2[4]
        {
            //down
            new Vector2(size.x * 0.5f, 0),
            //up
            new Vector2(0, size.y),
            //left
            new Vector2(0, size.y * 0.5f),
            //right
            new Vector2(size.x * 0.5f, size.y * 0.5f)
        };

        Vector3 startPosition = _offsets[0];
        Vector3 relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[1]);
        Vector3 delta = relativePosition - startPosition + new Vector3(size.x * 0.5f, 0);
        Vector3 endPosition = startPosition + delta;

        Vector3 middlePosition = delta * 0.5f + startPosition;

        _edge.points = new Vector2[]
                {
                    startPosition,
                    new Vector2(startPosition.x, middlePosition.y),
                    new Vector2(endPosition.x, middlePosition.y),
                    endPosition
                };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        _techTree.tooltipPanel.SetNodeInformation(_nodeType);
        _techTree.tooltipPanel.Open();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        EnableNode();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsNodeEnable == false)
        {
            int requireCoin = 0;
            NodeSO curNode = _nodeType;

            while (curNode != null && !_techTree.GetNode(curNode.id).IsNodeEnable)
            {
                requireCoin += curNode.requireCoin;
                _nodes.Push(_techTree.GetNode(curNode.id));

                curNode = _techTree.treeSO.nodes.Find(node => node.nextNodes.Contains(curNode));
            }

            int coin = GameDataManager.Instance.Coin;
            _techTree.selectNodeEvent?.Invoke(coin, requireCoin);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void SetNode(NodeSO node)
    => _nodeType = node;
}
