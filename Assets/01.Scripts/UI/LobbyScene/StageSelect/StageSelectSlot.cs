using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSlot : MonoBehaviour
{
    [SerializeField] private Image _stageColorImage;

    [SerializeField] private StageInfoSO _stageInfo;
    [SerializeField] private Image _image;
    [SerializeField] private Image _lockPanel;
    private Button _button;
    private StageSelectPanel _panel;

    

    public void Initialize(StageSelectPanel panel, StageInfoSO info)
    {
        _panel = panel;
        _stageInfo = info;
        _stageColorImage.color = info.stageColor;
        _image.sprite = info.stageImageSprite;
       
       _lockPanel.gameObject.SetActive(_stageInfo.isLocked); 
        // 나중에 스테이지 잠금 부분은 다른식으로 진도를 저장해서 구현해야함
        // 현재는 StageInfoSO안에있는 bool값으로 강제 잠금임
    }
}
