using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private TechTree tree;
    public NodeSO nodeSO;

    public int id { get; private set; }

    private void Awake()
    {
        tree = transform.GetComponentInParent<TechTree>();
    }

    private void Update()
    {
        
    }

    public void StartLoad()
    {

    }

    public void EndLoad()
    {

    }

    public void Init(NodeSO nodeSO)
    {
        this.nodeSO = nodeSO;
        id = nodeSO.id;
    }

    #region EventRegion

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.05f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    #endregion
}
