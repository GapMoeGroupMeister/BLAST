using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public NodeSO node;
    private TechTree _tree;

    private Image _edge;
    private Image _vertex;
    private Image _icon;

    [SerializeField] private float _enableDuration = 0.5f;
    [SerializeField] private float _disableDuration = 0.2f;
    private string _coinLackTxt = "<color=yellow>����</color>�� �����մϴ�.";

    private bool _isNodeEnable = false;         //���� ��尡 �շ��ִ���
    private bool _isNodeActive = false;         //���� ��带 �����غ� ���ִ���
    public bool IsNodeEnable => _isNodeEnable;
    public bool IsNodeActive => _isNodeActive;

    private Sequence _seq;

    private void Awake()
    {
        if (node is not StartNodeSO)
            _edge = transform.Find("Edge/EdgeFill").GetComponent<Image>();

        _vertex = transform.Find("Vertex/VertexFill").GetComponent<Image>();
        _icon = transform.Find("Vertex/Icon").GetComponent<Image>();
        _icon.sprite = node.icon;
    }

    public void StartEnableNode()
    {
        if (_isNodeEnable || _isNodeActive == false) return;

        //���� ����
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
        if (_isNodeEnable || IsNodeActive == false) return;

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

        //���� ���� �Ⱥ��̴ٰ� ���̰� �Ѵٵ簡 �׷��� �ϰ� ���ּ�����
    }

    public void DestroyEdge()
    {
        Destroy(_edge.gameObject);
        _edge = null;
    }

    #region InputEvent

    public void OnPointerExit(PointerEventData eventData)
    {
        _vertex.transform.parent.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _vertex.transform.parent.localScale = Vector3.one * 1.05f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        StopEnableNode();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        StartEnableNode();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        _tree.tooltipPanel.SetNodeInformation(node);
        _tree.tooltipPanel.Open();
    }

    #endregion
}
