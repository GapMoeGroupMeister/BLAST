using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public NodeSO node;
    private TechTree _tree;

    [SerializeField] private Image edge;
    [SerializeField] private Image vertex;

    [SerializeField] private float _enableDuration = 0.5f;
    [SerializeField] private float _disableDuration = 0.2f;

    private bool _isNodeEnable = false;         //현재 노드가 뚫려있는지
    private bool _isNodeActive = false;         //현재 노드를 뚫을준비가 되있는지
    public bool IsNodeEnable => _isNodeEnable;
    public bool IsNodeActive => _isNodeActive;

    private Sequence _seq;

    public void StartEnableNode()
    {
        if (_isNodeEnable || _isNodeActive == false) return;

        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Append(edge.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
            .Append(vertex.DOFillAmount(1, _enableDuration / 2).SetEase(Ease.Linear))
            .OnComplete(EnableNode);
    }

    public void StopEnableNode()
    {
        if (_isNodeEnable || IsNodeActive == false) return;

        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();



        _seq.Append(vertex.DOFillAmount(0, _disableDuration / 2).SetEase(Ease.Linear))
            .Append(edge.DOFillAmount(0, _disableDuration / 2).SetEase(Ease.Linear));
    }

    public void EnableNode()
    {
        _isNodeEnable = true;

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

        if (edge != null)
            edge.transform.parent.SetParent(tree.edgeParent);

        if (enable)
        {
            for (int i = 0; i < node.nextNodes.Count; i++)
            {
                if (_tree.TryGetNode(node.nextNodes[i], out Node nextNode))
                    nextNode.Activenode();
            }

            if (vertex != null) vertex.fillAmount = 1;
            if (edge != null)  edge.fillAmount = 1;
        }
    }

    private void Activenode()
    {
        _isNodeActive = true;

        //여기 원래 안보이다가 보이게 한다든가 그런거 하게 해주셈ㅇㅇ
    }

    #region InputEvent

    public void OnPointerExit(PointerEventData eventData)
    {
        vertex.transform.parent.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        vertex.transform.parent.localScale = Vector3.one * 1.05f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopEnableNode();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartEnableNode();
    }

    #endregion
}
