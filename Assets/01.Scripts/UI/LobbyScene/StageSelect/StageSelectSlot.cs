using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSlot : MonoBehaviour
{
    [SerializeField] private Image _stageColorImage;

    [SerializeField] private StageInfoSO _stageInfo;
    [SerializeField] private Image _image;
    [SerializeField] private Image _lockPanel;
    [SerializeField] private TextMeshProUGUI _stageNameText;
    private Button _button;
    private StageSelectPanel _panel;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleSelectStage);

    }

    private void HandleSelectStage()
    {
        _panel.SelectStage(_stageInfo);
    }

    public void Initialize(StageSelectPanel panel, StageInfoSO info)
    {
        _panel = panel;
        _stageInfo = info;
        _stageColorImage.color = info.stageColor;
        _image.sprite = info.stageImageSprite;
        _stageNameText.text = info.stageName;
       
       _lockPanel.gameObject.SetActive(_stageInfo.isLocked); 
        // 나중에 스테이지 잠금 부분은 다른식으로 진도를 저장해서 구현해야함
        // 현재는 StageInfoSO안에있는 bool값으로 강제 잠금임
    }
}
