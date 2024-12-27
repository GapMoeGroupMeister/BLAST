using Cinemachine.Utility;
using DG.Tweening;
using GGM.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private NodeSO _nodeType;

    [SerializeField] private UILineRenderer _edge;
    [SerializeField] private UILineRenderer _edgeFill;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _vertexFill;
    [SerializeField] private float _enableTime = 0.5f;
    [SerializeField] private Material _lineMaterial;

    private Vector2[] _offsets;
    private bool _isNodeEnable;
    private bool _isNodeActive;
    private int _requireCoin;
    private Node _curEnableNode;

    private Stack<Node> _prevNodes;
    private Stack<Node> _enabledNodes;
    private Coroutine _enableCoroutine;
    private Coroutine _currentEnableRoutine;

    private Coroutine _cancelCoroutine;
    private Coroutine _currentCancelRoutine;

    private bool _tryNodeEnable = false;
    private readonly string CoinLackText = $"코인이 부족합니다ㅠㅠ";

    #region Property

    private TechTree _techTree => GetComponentInParent<TechTree>();
    public RectTransform RectTrm => transform as RectTransform;
    public bool IsNodeEnable => _isNodeEnable;
    public NodeSO NodeType => _nodeType;

    #endregion

    private void Awake()
    {
        _prevNodes = new Stack<Node>();

        _edge.transform.SetParent(_techTree.EdgeParent);
        _edgeFill.transform.SetParent(_techTree.EdgeFillParent);

        _edge.material = Instantiate(_lineMaterial);
        _edgeFill.material = Instantiate(_lineMaterial);
        _edge.SetFillAmount(1);
        _edgeFill.SetFillAmount(0);
    }

    #region EnableNode

    //선택된 노드들을 전부 활성화시켜주는 코루틴
    public IEnumerator StartEnableAllNodes()
    {
        if (_cancelCoroutine != null)
            StopCoroutine(_cancelCoroutine);
        if (_currentCancelRoutine != null)
            StopCoroutine(_currentCancelRoutine);

        GetPrevNodes();
        _enabledNodes = new Stack<Node>();

        //_prevNodes에 활성화 시킬 노드들을 순차적으로 꺼내서 활성화시키기 시도
        while (_prevNodes.TryPop(out _curEnableNode))
        {
            _currentEnableRoutine = StartCoroutine(_curEnableNode.EnableNodeRoutine());
            _enabledNodes.Push(_curEnableNode);
            yield return _currentEnableRoutine;
        }

        //전부 활성화 됬다면, 한번에 데이터상에서 활성화 시켜주기
        //취소했을 때 전부 사라져야하기 때문
        while (_enabledNodes.TryPop(out _curEnableNode))
        {
            _curEnableNode.EnableNode();
        }
    }

    public IEnumerator EnableNodeRoutine()
    {
        float process = 0;

        if (_nodeType is not StartNodeSO)
        {
            _edgeFill.transform.SetAsLastSibling();
            while (process < 1)
            {
                process += Time.deltaTime / (_enableTime / 2);
                _edgeFill.SetFillAmount(process);
                yield return null;
            }
        }

        process = 0;
        while (process < 1)
        {
            process += Time.deltaTime / (_enableTime / 2);
            _vertexFill.fillAmount = process;
            yield return null;
        }
    }

    public void EnableNode()
    {
        //재화 사용
        _isNodeEnable = true;
        GameDataManager.Instance.UseCoin(NodeType.requireCoin);

        //fillAmount 1로 초기화
        _vertexFill.fillAmount = 1;
        _edgeFill.SetFillAmount(1);

        //다음 노드가 활성화 되었다고 알려줌
        for (int i = 0; i < NodeType.nextNodes.Count; i++)
        {
            if (_techTree.TryGetNode(NodeType.nextNodes[i], out Node prevNode))
            {
                prevNode.ActiveNode();
            }
        }

        //파츠 혹은 무기 활성화시켜주기
        if (NodeType is PartNodeSO part)
            GameDataManager.Instance.EnablePart(part.openPart);
        if (NodeType is WeaponNodeSO weapon)
            GameDataManager.Instance.EnableWeapon(weapon.weapon);

        _techTree.Save();
    }

    #endregion

    #region CancelEnableNode

    public IEnumerator CancelEnableNode()
    {
        if (_enableCoroutine != null)
            StopCoroutine(_enableCoroutine);
        if (_currentEnableRoutine != null)
            StopCoroutine(_currentEnableRoutine);

        while (_enabledNodes.TryPop(out _curEnableNode))
        {
            _currentCancelRoutine = StartCoroutine(_curEnableNode.DisableNodeRoutine());
            yield return _currentCancelRoutine;
        }
    }

    public IEnumerator DisableNodeRoutine()
    {
        float process = _vertexFill.fillAmount;

        while (process > 0)
        {
            process -= Time.deltaTime / (_enableTime / 2);
            _vertexFill.fillAmount = process;
            yield return null;
        }

        process = _edgeFill.GetFillAmount();
        if (_nodeType is not StartNodeSO)
        {
            while (process > 0)
            {
                process -= Time.deltaTime / (_enableTime / 2);
                _edgeFill.SetFillAmount(process);
                yield return null;
            }
        }
    }

    #endregion

    private void GetPrevNodes()
    {
        _prevNodes = new Stack<Node>();
        if (IsNodeEnable == false)
        {
            int requireCoin = 0;
            NodeSO curNode = _nodeType;

            while (curNode != null && !_techTree.GetNode(curNode.id).IsNodeEnable)
            {
                requireCoin += curNode.requireCoin;
                _prevNodes.Push(_techTree.GetNode(curNode.id));

                curNode = curNode.prevNode;
            }

            int coin = GameDataManager.Instance.Coin;
            _techTree.selectNodeEvent?.Invoke(coin, requireCoin);
        }
    }

    public void ActiveNode()
    {
        _isNodeActive = true;
    }

    public void SetEdge()
    {
        if (_nodeType.prevNode == null) return;

        Vector2 size = RectTrm.sizeDelta;
        Node prevNode = _techTree.GetNode(_nodeType.prevNode.id);

        _offsets = new Vector2[4]
        {
            //down
            new Vector2(size.x * 0.5f, -10),
            //up
            new Vector2(0, size.y * 0.5f + 10),
            //left
            new Vector2(-10, size.y * 0.5f),
            //right
            new Vector2(size.x * 0.5f + 10, 0)
        };


        Vector3 startPosition = _offsets[0];
        Vector3 relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[1]);
        Vector3 delta = relativePosition - startPosition;

        if (prevNode.RectTrm.anchoredPosition.y < RectTrm.anchoredPosition.y - size.y / 2)
        {
            startPosition = _offsets[0];
            relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[1]);
            delta += new Vector3(size.x * 0.5f, size.y * 0.5f);
        }
        else if (prevNode.RectTrm.anchoredPosition.y > RectTrm.anchoredPosition.y + size.y / 2)
        {
            startPosition = _offsets[1];
            relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[0]);
            delta += new Vector3(size.x * 0.5f, 0);
        }
        else
        {
            if (prevNode.RectTrm.anchoredPosition.x < RectTrm.anchoredPosition.x - size.x / 2)
            {
                startPosition = _offsets[2];
                relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[3]);
                delta += new Vector3(0f, size.y * 0.5f);
            }
            else if (prevNode.RectTrm.anchoredPosition.x > RectTrm.anchoredPosition.x + size.x / 2)
            {
                startPosition = _offsets[3];
                relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[2]);
                delta += new Vector3(0f, size.y * 0.5f);
            }
        }


        Vector3 endPosition = startPosition + delta;
        Vector3 middlePosition = delta * 0.5f + startPosition;

        _edge.points = new Vector2[]
                {
                    endPosition,
                    new Vector2(endPosition.x, middlePosition.y),
                    new Vector2(startPosition.x, middlePosition.y),
                    startPosition
                };

        _edgeFill.points = new Vector2[]
                {
                    endPosition,
                    new Vector2(endPosition.x, middlePosition.y),
                    new Vector2(startPosition.x, middlePosition.y),
                    startPosition
                };
    }

    public void SetNode(NodeSO node)
    {
        _nodeType = node;
        _icon.sprite = node.icon;
    }

    public void Init(bool isEnable)
    {
        if (isEnable)
        {
            _isNodeEnable = true;

            _vertexFill.fillAmount = 1;
            _edgeFill.SetFillAmount(1);

            for (int i = 0; i < NodeType.nextNodes.Count; i++)
            {
                if (_techTree.TryGetNode(NodeType.nextNodes[i], out Node prevNode))
                {
                    prevNode.ActiveNode();
                }
            }

            //파츠 혹은 무기 활성화시켜주기
            if (NodeType is PartNodeSO part)
                GameDataManager.Instance.EnablePart(part.openPart);
            if (NodeType is WeaponNodeSO weapon)
                GameDataManager.Instance.EnableWeapon(weapon.weapon);
        }
    }

     #region InputRegion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        _techTree.tooltipPanel.SetNodeInformation(_nodeType);
        _techTree.tooltipPanel.Open();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _prevNodes = new Stack<Node>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (_requireCoin > GameDataManager.Instance.Coin) return;

        _enableCoroutine = StartCoroutine(StartEnableAllNodes());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (_requireCoin > GameDataManager.Instance.Coin) return;
        
        _cancelCoroutine = StartCoroutine(CancelEnableNode());
    }

    #endregion
}
