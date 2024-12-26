using TMPro;
using UnityEngine;
namespace LobbyScene.ColorSettings
{

    public class ColorSettingPanel : MonoBehaviour
    {
        [SerializeField] private ColorSettingSlot _colorSlotPrefab;


        [SerializeField] private RectTransform _contentTrm;
        [SerializeField] private RectTransform _addColorButtonTrm;


        public void HandleAddColor()
        {
            Instantiate(_colorSlotPrefab, _contentTrm);
            _addColorButtonTrm.SetAsLastSibling();
        }


    }
}