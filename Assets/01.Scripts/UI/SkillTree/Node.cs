using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public NodeSO node;
    private TechTree _tree;

    private Stack<Node> _nodes = new Stack<Node>();
    private Node _curEnableNode;
    private RectTransform _rectTrm;
    private RectTransform _edgeRectTrm;
    private Image _edge;
    private Image _vertex;
    private Image _icon;

    [SerializeField] private float _enableDuration = 0.5f;
    [SerializeField] private float _disableDuration = 0.2f;
    private string _coinLackTxt = "<color=yellow>코인</color>이 부족합니다.";

    private bool _isNodeEnable = false;         //현재 노드가 뚫려있는지
    private bool _isNodeActive = false;         //현재 노드를 뚫을준비가 되있는지

    public bool IsNodeEnable => _isNodeEnable;
    public bool IsNodeActive => _isNodeActive;

    private Coroutine _enableCoroutine;
    private Sequence _seq;
    private int _requireCoin;


    private void Awake()
    {
        if (node is not StartNodeSO)
            _edge = transform.Find("Edge/EdgeFill").GetComponent<Image>();

        _vertex = transform.Find("Vertex/VertexFill").GetComponent<Image>();
        _icon = transform.Find("Vertex/Icon").GetComponent<Image>();
        _icon.sprite = node.icon;

        _rectTrm = transform as RectTransform;
        _edgeRectTrm = transform.Find("Edge") as RectTransform;
    }



    private IEnumerator StartEnableAllNodes()
    {
        if (_isNodeEnable) yield break;

        if (_requireCoin > GameDataManager.Instance.Coin)
        {
            _tree.warningPanel.SetText(_coinLackTxt);
            _tree.warningPanel.Open();
            yield break;
        }

        while (_nodes.TryPop(out _curEnableNode))
        {
            _curEnableNode.StartEnableNode();
            yield return new WaitUntil(() => _curEnableNode.IsNodeEnable);
        }
    }

    private void StopEnableAllNodes()
    {
        if (_enableCoroutine != null) StopCoroutine(_enableCoroutine);
        _curEnableNode?.StopEnableNode();
        _nodes.Clear();
    }


    public void StartEnableNode()
    {
        if (_isNodeEnable) return;

        //코인 부족
        if (node.requireCoin > GameDataManager.Instance.Coin)
        {
            _tree.warningPanel.SetText(_coinLackTxt);
            _tree.warningPanel.Open();
            return;
        }

        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Append(_edge.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
            .Append(_vertex.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
            .OnComplete(EnableNode);
    }

    public void StopEnableNode()
    {
        if (_isNodeEnable) return;

        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Append(_vertex.DOFillAmount(0, _disableDuration / 2).SetEase(Ease.Linear))
            .Append(_edge.DOFillAmount(0, _disableDuration / 2).SetEase(Ease.Linear));
    }



    public void EnableNode()
    {
        _isNodeEnable = true;
        GameDataManager.Instance.UseCoin(node.requireCoin);

        for (int i = 0; i < node.nextNodes.Count; i++)
        {
            if (_tree.TryGetNode(node.nextNodes[i], out Node nextNode))
            {
                nextNode.Activenode();
            }
        }

        if (node is PartNodeSO part)
            GameDataManager.Instance.EnablePart(part.openPart);
        if (node is WeaponNodeSO weapon)
            GameDataManager.Instance.EnableWeapon(weapon.weapon);

        _tree.Save();
    }

    public void Init(TechTree tree, bool enable)
    {
        _tree = tree;
        _isNodeEnable = enable;

        if (_edge != null)
            _edge.transform.parent.SetParent(tree.edgeParent);

        if (enable)
        {
            for (int i = 0; i < node.nextNodes.Count; i++)
            {
                if (_tree.TryGetNode(node.nextNodes[i], out Node nextNode))
                    nextNode.Activenode();
            }

            if (_vertex != null) _vertex.fillAmount = 1;
            if (_edge != null) _edge.fillAmount = 1;
        }
    }

    private void Activenode()
    {
        _isNodeActive = true;

        //여기 원래 안보이다가 보이게 한다든가 그런거 하게 해주셈ㅇㅇ
    }

    public void DestroyEdge()
    {
        DestroyImmediate(transform.Find("Edge").gameObject);
        _edge = null;
    }


    #region Debug

    public void SetPosition(Vector2 position) => (transform as RectTransform).anchoredPosition = position;
    public void SetEdgePosition(Vector2 position)
    {
        if (transform.Find("Edge") != null)
            (transform.Find("Edge") as RectTransform).anchoredPosition = position;
    }
    public Vector2 GetPosition() => (transform as RectTransform).anchoredPosition;
    public Vector2 GetEdgePosition()
    {
        if (transform.Find("Edge") != null)
            return (transform.Find("Edge") as RectTransform).anchoredPosition;

        return Vector2.zero;
    }

    #endregion


    #region InputEvent

    public void OnPointerExit(PointerEventData eventData)
    {
        _vertex.transform.parent.localScale = Vector3.one;

        if (_isNodeEnable == false)
        {
            int coin = GameDataManager.Instance.Coin;
            _tree.selectNodeEvent?.Invoke(coin, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _vertex.transform.parent.localScale = Vector3.one * 1.05f;

        if (_isNodeEnable == false)
        {
            _requireCoin = 0;
            NodeSO curNode = node;

            while (curNode != null && !_tree.GetNode(curNode.id)._isNodeEnable)
            {
                _requireCoin += curNode.requireCoin;
                _nodes.Push(_tree.GetNode(curNode.id));

                curNode = _tree.treeSO.nodes.Find(node => node.nextNodes.Contains(curNode));
            }

            int coin = GameDataManager.Instance.Coin;
            _tree.selectNodeEvent?.Invoke(coin, _requireCoin);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        StopEnableAllNodes();
        //StopEnableNode();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        _enableCoroutine = StartCoroutine(StartEnableAllNodes());
        //StartEnableNode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        _tree.tooltipPanel.SetNodeInformation(node);
        _tree.tooltipPanel.Open();
    }

    #endregion
}
