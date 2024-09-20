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
    }
}
