using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    private Button _button;
    [SerializeField] private string _content;
    //public UnityEvent<string> OnSeelctEvent;
    [SerializeField] private ButtonDescriptionPanel _descriptionPanel;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionPanel.Show(_content);
    }
}
