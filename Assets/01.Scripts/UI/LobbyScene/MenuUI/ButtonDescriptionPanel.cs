using TMPro;
using UnityEngine;

public class ButtonDescriptionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    public void Show(string content)
    {
        _descriptionText.text = content;
    }
}