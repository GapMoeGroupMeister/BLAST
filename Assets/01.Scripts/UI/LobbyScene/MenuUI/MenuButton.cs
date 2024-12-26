using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    [SerializeField] private string _content;
    [SerializeField] private ButtonDescriptionPanel _descriptionPanel;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionPanel.Show(_content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionPanel.Disable();
    }
}
